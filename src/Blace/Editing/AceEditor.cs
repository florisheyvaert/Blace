using Blace.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editing
{
    public class AceEditor
    {
        private readonly IJSRuntime _js;
        private readonly EditorOptions _options;

        public string Id { get; private set; }
        public string Theme { get; private set; }
        public string Mode { get; private set; }
        public string Value { get; private set; }

        public AceEditor(IJSRuntime js, string id, EditorOptions options)
        {
            _js = js;
            _options = options ?? new();
            Id = id;

            LoadOptions();
        }

        public async Task SetValue(string value)
        {
            await _js.InvokeVoidAsync("ace_load", Id, Theme, Mode);

            var text = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
            await _js.InvokeVoidAsync("ace_set_value", Id, text);

            Value = text;
        }

        public async Task SetTheme(string theme)
        {
            await _js.InvokeVoidAsync("ace_set_theme", Id, theme);
            Theme = theme;
        }

        public async Task SetMode(string mode)
        {
            await _js.InvokeVoidAsync("ace_set_mode", Id, mode);
            Mode = mode;
        }

        private void LoadOptions()
        {
            Mode = !string.IsNullOrWhiteSpace(_options.Syntax) ? _options.Syntax : "mode-text";
            Theme = !string.IsNullOrWhiteSpace(_options.Theme) ? _options.Theme : "theme-monokai";
        }

        //[JSInvokable]
        //public static void AceEditorValueChanged(string value)
        //{
        //    _valueChanged.Invoke(value);
        //}

        //[JSInvokable]
        //public static async Task AceEditorSave()
        //{
        //    await _shortcutSavePressed.Invoke();
        //}
    }
}
