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
        [Parameter] public bool ShowSettings { get; set; }
        [Parameter] public string CssClass { get; set; }
        [Parameter] public string SettingsCssClass { get; set; }
        [Parameter] public string SettingCssStyle { get; set; }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool IsClosed { get; set; }
        public bool ShowSettingsPanel { get; set; }
        public Theme Theme { get; set; }
        public Syntax Syntax { get; set; }

        public async Task Open(T file, EditorOptions options = null)
        {
            Theme = options?.Theme ?? Theme.Chrome;
            Syntax = options?.Syntax ?? Syntax.Text;

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
                await FileChanged.InvokeAsync(false);

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

        public async Task ThemeChanged(Theme theme)
        {
            Theme = theme;
            await _editor?.SetTheme(Theme);
        }

        public async Task SyntaxChanged(Syntax syntax)
        {
            Syntax = syntax;
            await _editor?.SetSyntax(Syntax);
        }

        protected async Task ToggleSettingsPanel(bool? value = null)
        {
            ShowSettingsPanel = value.HasValue ? value.Value : !ShowSettingsPanel;
            await InvokeAsync(StateHasChanged);
        }
    }
}
