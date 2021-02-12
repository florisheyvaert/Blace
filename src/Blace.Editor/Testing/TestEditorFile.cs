using Blace.Editor.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Testing
{
    public class TestEditorFile : IEditorFile
    {
        public string Name { get; set; }
        public string Content { get; set; }

        public TestEditorFile(string name)
        {
            Name = name;
        }

        public Task<string> LoadContent()
        {
            return Task.FromResult("Dit is een test");
        }

        public Task<bool> Save()
        {
            return Task.FromResult(true);
        }
    }
}
