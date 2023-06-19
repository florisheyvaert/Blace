using Blace.Editing;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Components
{
    public partial class Editor<T> where T : BaseEditorFile
    {
        private AceEditor _editor;
        private T _file;

        [Inject] public IJSRuntime JS { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsClosed { get; set; }

        public async Task Open(T file, EditorOptions options = null)
        {
            _file = file;
            _editor = new AceEditor(JS, Id, options);

            IsClosed = false;

            await Reload();
        }

        public async Task Reload()
        {
            var content = await _file.Load();
            await _editor.SetValue(content);
        }

        public async Task Save(bool close = false)
        {
            await _file.Save();
            await InvokeAsync(StateHasChanged);

            if (close)
            {
                await Close();
            }
        }

        public async Task Close()
        {
            await _editor.SetValue(string.Empty);
            IsClosed = true;
            _file = null;
            _editor = null;

            await InvokeAsync(StateHasChanged);
        }


        //[Parameter] public List<BaseEditorFile> Files { get; set; }
        //[Parameter] public Func<BaseEditorFile,Task<bool>> SaveFileOnClose { get; set; }

        //protected string Id { get => $"editor-{GetHashCode()}"; }
        //public BaseEditorFile SelectedFile { get; set; }
        //protected AceEditor AceEditor { get; set; }
        //protected bool SettingsHidden { get; set; } = true;
        //protected bool LoadingContent { get; set; } = false;

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        AceEditor = new AceEditor(Id, JS, ValueChanged, ShortcutSavePressed);
        //        await AceEditor.Load();

        //        foreach (var file in Files)
        //            await OpenFile(file);
        //    }

        //    await base.OnAfterRenderAsync(firstRender);
        //}

        //protected override async Task OnInitializedAsync()
        //{
        //    await base.OnInitializedAsync();
        //}

        //public async Task SelectFile(BaseEditorFile file)
        //{
        //    SelectedFile = file;

        //    if (SelectedFile is object)
        //    {
        //        var isLastFile = Files.LastOrDefault() == file;
        //        await JS.InvokeVoidAsync("ScrollIntoView", $"editor-header-tab-{file.GetHashCode()}", "editor-header", isLastFile);
        //        await AceEditor.SetValue(file.Content);
        //    }
        //    else
        //    {
        //        await AceEditor.SetValue(string.Empty);
        //    }

        //    StateHasChanged();
        //}

        //public async Task OpenFile(BaseEditorFile file)
        //{
        //    LoadingContent = true;
        //    StateHasChanged();

        //    var existingFile = Files.FirstOrDefault(x => x == file);
        //    if (existingFile is null)
        //    {
        //        existingFile = file;
        //        Files.Add(existingFile);
        //        existingFile.Content = await file.Load();
        //    }
        //    await SelectFile(existingFile);

        //    LoadingContent = false;
        //    StateHasChanged();
        //}

        //public async Task CloseFile(BaseEditorFile file)
        //{
        //    var save = false;
        //    if (SaveFileOnClose is object)
        //        save = await SaveFileOnClose.Invoke(file);
        //    if (save)
        //        await file.Save();

        //    var nextFileIndex = Files.IndexOf(SelectedFile) + 1;
        //    Files.Remove(file);
        //    await SelectFile(nextFileIndex);

        //    StateHasChanged();
        //}

        //public async Task SelectNextFile()
        //{
        //    var indexOfFile = Files.IndexOf(SelectedFile) + 1;
        //    await SelectFile(indexOfFile);
        //}

        //public async Task SelectPreviousFile()
        //{
        //    var indexOfFile = Files.IndexOf(SelectedFile) - 1;
        //    await SelectFile(indexOfFile);
        //}

        //public async Task ToggleSettings()
        //{
        //    await Task.Delay(0);
        //    SettingsHidden = !SettingsHidden;
        //}

        //private async Task SelectFile(int index)
        //{
        //    if (Files.Count == 0)
        //        await SelectFile(null);
        //    if (index <= 0)
        //        await SelectFile(Files.FirstOrDefault());
        //    else if (index >= Files.Count)
        //        await SelectFile(Files.LastOrDefault());
        //    else
        //        await SelectFile(Files[index]);
        //}

        //private void ValueChanged(string value)
        //{
        //    if (SelectedFile is object)
        //    {
        //        SelectedFile.Content = value;
        //        StateHasChanged();
        //    }
        //}

        //private async Task ShortcutSavePressed()
        //{
        //    if (SelectedFile is object)
        //    {
        //        await SelectedFile.Save();
        //        StateHasChanged();
        //    }
        //}
    }
}
