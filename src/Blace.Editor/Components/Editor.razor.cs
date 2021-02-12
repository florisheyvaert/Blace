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
        private AceEditor _aceEditor;

        [Parameter] public List<BaseEditorFile> Files { get; set; }
        [Parameter] public Func<Task<bool>> AskConfirmation { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        public string Id { get => $"editor-{GetHashCode()}"; }
        public BaseEditorFile SelectedFile { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _aceEditor = new AceEditor(Id, JS, ValueChanged);
                await _aceEditor.Load();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task SelectFile(BaseEditorFile file)
        {
            SelectedFile = file;
            await JS.InvokeVoidAsync("ScrollIntoView", $"editor-header-tab-{file.GetHashCode()}");
            await _aceEditor.SetValue(file.Content);
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
    }
}
