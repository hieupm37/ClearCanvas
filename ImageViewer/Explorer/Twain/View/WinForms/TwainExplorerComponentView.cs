using System.Windows.Forms;
using ClearCanvas.Common;
using ClearCanvas.Desktop.View.WinForms;
using ClearCanvas.Desktop;

namespace ClearCanvas.ImageViewer.Explorer.Twain.View.WinForms
{
    [ExtensionOf(typeof(TwainExplorerComponentViewExtensionPoint))]
    public class TwainExplorerComponentView : WinFormsView, IApplicationComponentView
    {
        private Control _control;
        private TwainExplorerComponent _component;

        public TwainExplorerComponentView()
        {
        }

        public override object GuiElement
        {
            get 
            {
                if (_control == null)
                {
                    _control = new TwainExplorerControl(_component);
                }

                return _control;
            }
        }


        #region IApplicationComponentView Members

        public void SetComponent(IApplicationComponent component)
        {
            _component = component as TwainExplorerComponent;
        }

        #endregion
    }
}
