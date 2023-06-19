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
    public partial class Editor<T> where T : EditorFile
    {
        private AceEditor _editor;
        private T _file;

        [Inject] public IJSRuntime JS { get; set; }

        [Parameter] public EventCallback<bool> FileChanged { get; set; }
        [Parameter] public string CssClass { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsClosed { get; set; }

        public async Task Open(T file, EditorOptions options = null)
        {
            _file = file;
            _editor = new AceEditor(JS, Id, options, FileChange, Save);

            IsClosed = false;

            await Reload();
        }

        public async Task Reload()
        {
            if (!IsClosed)
            {
                var content = await _file.Load();
                await _editor.SetValue(content);
            }

            await InvokeAsync(StateHasChanged);
        }

        public async Task Save(bool close = false)
        {
            if (!IsClosed)
            {
                await _file.Save();
                await InvokeAsync(StateHasChanged);

                if (close)
                {
                    await Close();
                }
            }
        }

        public async Task Close()
        {
            if (_editor is object)
                await _editor.SetValue(string.Empty);

            IsClosed = true;
            _file = null;
            _editor = null;

            await InvokeAsync(StateHasChanged);
        }

        public async Task FileChange(string content)
        {
            var isChanged = _file.Content != content;
            _file.Content = content;
            await FileChanged.InvokeAsync(isChanged);
        }
    }
}
