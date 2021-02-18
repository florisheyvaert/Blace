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
        [Parameter] public BaseEditorFile File { get; set; }
        [Parameter] public BaseEditorFile SelectedFile { get; set; }
        [Parameter] public EventCallback<BaseEditorFile> CloseFileClicked { get; set; }
        [Parameter] public EventCallback<BaseEditorFile> OpenFileClicked { get; set; }
    }
}
