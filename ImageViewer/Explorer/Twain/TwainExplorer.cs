using ClearCanvas.Common;
using ClearCanvas.Desktop.Explorer;
using ClearCanvas.Desktop;

namespace ClearCanvas.ImageViewer.Explorer.Twain
{
    [ExtensionOf(typeof(HealthcareArtifactExplorerExtensionPoint))]
    public class TwainExplorer : IHealthcareArtifactExplorer
    {
        TwainExplorerComponent _component;

        public TwainExplorer()
        {
        }


        #region IHealthcareArtifactExplorer Members

        public string Name
        {
            get { return SR.Twain; }
        }

        public bool IsAvailable
        {
            // TODO(hieupm37): add permission to TWAIN
            get { return true; }
        }

        public IApplicationComponent Component
        {
            get
            {
                if (_component == null && IsAvailable)
                    _component = new TwainExplorerComponent();

                return _component;
            }
        }

        #endregion
    }
}
