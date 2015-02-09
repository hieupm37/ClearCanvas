using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ClearCanvas.Common.Configuration;

namespace ClearCanvas.ImageViewer.Explorer.Twain.View.WinForms
{
    [SettingsGroupDescription("Stores settings for the TWAIN Explorer plugin.")]
    [SettingsProvider(typeof(StandardSettingsProvider))]
    internal sealed partial class Settings
    {
        public Settings()
        {
        }
    }
}
