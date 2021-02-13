using Blace.Editor.Editing;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Components
{
    public class EditorBase : ComponentBase
    {
        [Parameter] public List<BaseEditorFile> Files { get; set; }
        [Parameter] public Func<Task<bool>> AskConfirmation { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        public string Id { get => $"editor-{GetHashCode()}"; }
        public BaseEditorFile SelectedFile { get; set; }
        public AceEditor AceEditor { get; set; }
        public bool SettingsHidden { get; set; } = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                AceEditor = new AceEditor(Id, JS, ValueChanged, ShortcutSavePressed);
                await AceEditor.Load();

                foreach (var file in Files)
                    await OpenFile(file);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public async Task SelectFile(BaseEditorFile file)
        {
            SelectedFile = file;
            await JS.InvokeVoidAsync("ScrollIntoView", $"editor-header-tab-{file.GetHashCode()}", "editor-header");
            await AceEditor.SetValue(file.Content);
            StateHasChanged();
        }

        public async Task OpenFile(BaseEditorFile file)
        {
            file.Content = await file.Load();
            await SelectFile(file);
        }

        public async Task CloseFile(BaseEditorFile file)
        {
            var save = false;
            if (AskConfirmation is object)
                save = await AskConfirmation.Invoke();
            if (save)
                await file.Save();
            Files.Remove(file);
            StateHasChanged();
        }

        public async Task SelectNextFile()
        {
            await SelectFile(+1);
        }

        public async Task SelectPreviousFile()
        {
            await SelectFile(-1);
        }

        public async Task ToggleSettings()
        {
            SettingsHidden = !SettingsHidden;
        }

        private async Task SelectFile(int delta)
        {
            var indexOfFile = Files.IndexOf(SelectedFile);
            indexOfFile += delta;

            if (indexOfFile <= 0)
                await SelectFile(Files.FirstOrDefault());
            else if (indexOfFile >= Files.Count)
                await SelectFile(Files.LastOrDefault());
            else
                await SelectFile(Files[indexOfFile]);
        }

        private void ValueChanged(string value)
        {
            if (SelectedFile is object)
            {
                SelectedFile.Content = value;
                StateHasChanged();
            }
        }

        private async Task ShortcutSavePressed()
        {
            if (SelectedFile is object)
            {
                await SelectedFile.Save();
                StateHasChanged();
            }
        }
    }
}
