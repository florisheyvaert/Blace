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

        [Parameter] public List<IEditorFile> Files { get; set; }
        [Inject] public IJSRuntime JS { get; set; }

        public string Id { get => $"editor-{GetHashCode()}"; }
        public IEditorFile SelectedFile { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _aceEditor = new AceEditor(Id, JS);
                await _aceEditor.Load();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task NewFile(int index = 0)
        {

        }

        public async Task SelectFile(IEditorFile file)
        {
            SelectedFile = file;
            await JS.InvokeVoidAsync("ScrollIntoView", $"editor-header-tab-{file.GetHashCode()}");
            StateHasChanged();
        }

        public async Task OpenFile(IEditorFile file)
        {
            file.Content = await file.LoadContent();
            await SelectFile(file);
        }

        public async Task CloseFile(IEditorFile file)
        {

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
    }
}
