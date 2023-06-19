using Blace.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.TestEditor.Testing
{
    public class TestEditorFile : EditorFile
    {
        public TestEditorFile(string name) : base(name)
        {
            Name = name;
        }

        protected override Task<string> LoadContent()
        {
            return Task.FromResult("Dit is een test");
        }

        protected override Task<bool> SaveContent()
        {
            return Task.FromResult(true);
        }
    }
}
