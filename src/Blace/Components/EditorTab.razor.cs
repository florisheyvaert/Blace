using Blace.Editing;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Components
{
    public class EditorTabBase : ComponentBase
    {
        [Parameter] public EditorFile File { get; set; }
        [Parameter] public EditorFile SelectedFile { get; set; }
        [Parameter] public EventCallback<EditorFile> CloseFileClicked { get; set; }
        [Parameter] public EventCallback<EditorFile> OpenFileClicked { get; set; }
    }
}
