using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Editing
{
    public class AceEditor
    {
        private readonly string _id;
        private readonly IJSRuntime _js;
        private static Action<string> _valueChanged;

        public string Theme { get; private set; }
        public string Mode { get; private set; }
        public string Value { get; private set; }

        public AceEditor(string id, IJSRuntime jSRuntime, Action<string> valueChanged)
        {
            _id = id;
            _js = jSRuntime;
            _valueChanged = valueChanged;
        }

        public async Task Load()
        {
            Theme = "ace/theme/monokai";
            Mode = "ace/mode/python";
            await _js.InvokeVoidAsync("ace_load", _id, Theme, Mode);
        }

        public async Task SetTheme(string theme)
        {
            await _js.InvokeVoidAsync("ace_set_theme", _id, theme);
            Theme = theme;
        }

        public async Task SetMode(string mode)
        {
            await _js.InvokeVoidAsync("ace_set_mode", _id, mode);
            Mode = mode;
        }

        public async Task SetValue(string value)
        {
            var text = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
            await _js.InvokeVoidAsync("ace_set_value", _id, text);
            Value = text;
        }

        [JSInvokable]
        public static void AceEditorValueChanged(string value)
        {
            _valueChanged.Invoke(value);
        }
    }
}
