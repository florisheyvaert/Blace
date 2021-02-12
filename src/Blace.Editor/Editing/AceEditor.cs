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

        public AceEditor(string id, IJSRuntime jSRuntime)
        {
            _id = id;
            _js = jSRuntime;
        }

        public async Task Load()
        {
            await _js.InvokeVoidAsync("ace_load", _id, "ace/theme/monokai", "ace/mode/python");
        }

        public async Task SetTheme(string theme)
        {
            await _js.InvokeVoidAsync("ace_set_theme", _id, theme);
        }

        public async Task SetMode(string mode)
        {
            await _js.InvokeVoidAsync("ace_set_mode", _id, mode);
        }

        public async Task SetValue(string value)
        {
            await _js.InvokeVoidAsync("ace_set_value", _id, value);
        }
    }
}
