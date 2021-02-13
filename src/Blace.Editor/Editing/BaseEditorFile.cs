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

        public abstract Task<bool> SaveContent();
        public abstract Task<string> LoadContent();

        public async Task<string> Load()
        {
            Content = await LoadContent();
            _originalContent = Content;
            return Content;
        }

        public async Task<bool> Save()
        {
            var success = await SaveContent();
            _originalContent = Content;
            return success;
        }
    }
}
