using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Editing
{
    public interface IEditorFile
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public Task<bool> Save();
        public Task<string> LoadContent();
    }
}
