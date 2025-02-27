using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editing
{
    public class EditorOptions
    {
        public Theme Theme { get; set; }
        public Syntax Syntax { get; set; }
        public int MinLines { get; set; } = 10;
        public int MaxLines { get; set; }
        public bool ReadOnly { get; set; }
    }
}
