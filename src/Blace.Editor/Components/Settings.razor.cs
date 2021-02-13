using Blace.Editor.Editing;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blace.Editor.Components
{
    public class SettingsBase : ComponentBase
    {
        [Parameter] public bool Hidden { get; set; }
        [Parameter] public AceEditor Editor { get; set; }

        public AceRepository AceRepository { get; set; } = new AceRepository();
        public List<string> Themes { get; private set; }
        public List<string> Modes { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Themes = await AceRepository.GetThemes();
            Modes = await AceRepository.GetModes();
            await base.OnInitializedAsync();
        }
    }
}
