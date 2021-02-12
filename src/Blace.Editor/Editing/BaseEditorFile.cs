using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Editing
{
    public abstract class BaseEditorFile
    {
        private string _originalContent;

        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsChanged { get => _originalContent != Content; }

        public abstract Task<bool> Save();
        public abstract Task<string> LoadContent();

        public async Task<string> Load()
        {
            Content = await Load();
            _originalContent = Content;
            return Content;
        }
    }
}
