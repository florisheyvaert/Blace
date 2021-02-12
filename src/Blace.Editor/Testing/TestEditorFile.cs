using Blace.Editor.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Testing
{
    public class TestEditorFile : BaseEditorFile
    {
        public TestEditorFile(string name)
        {
            Name = name;
        }

        public override Task<string> LoadContent()
        {
            return Task.FromResult("Dit is een test");
        }

        public override Task<bool> Save()
        {
            return Task.FromResult(true);
        }
    }
}
