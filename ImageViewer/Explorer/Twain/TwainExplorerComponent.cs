using System;
using ClearCanvas.Desktop;
using ClearCanvas.Common;
using ClearCanvas.Desktop.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.ImageViewer.Explorer.Twain
{
    public interface ITwainExplorerToolContext : IToolContext
    {
        string SeletectedTwain { get; }
    }

    [ExtensionPoint()]
    public sealed class TwainExplorerToolExtensionPoint : ExtensionPoint<ITool>
    {
    }

    [ExtensionPoint]
    public sealed class TwainExplorerComponentViewExtensionPoint : ExtensionPoint<IApplicationComponentView>
    {
    }

    [AssociateView(typeof(TwainExplorerComponentViewExtensionPoint))]
    public class TwainExplorerComponent : ApplicationComponent
    {
        protected class TwainExplorerToolContext : ToolContext, ITwainExplorerToolContext
        {
            private TwainExplorerComponent _component;

            public TwainExplorerToolContext(TwainExplorerComponent component)
            {
                _component = component;
            }

            #region ITwainExplorerToolContext Members

            public string SeletectedTwain
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }

        private readonly string _galleryToolbarSite;
        private ToolSet _toolSet;  // TODO(hieupm37):  ???
        private ActionModelRoot _galleryToolbarModel;
        private ActionModelRoot _editorToolbarModel;
        private ButtonAction _galleryDeleteButtonAction;
        private ButtonAction _galleryDeleteAllButtonAction;
        private IResourceResolver _resolver;

        private ScanItemList _items;
        
        public TwainExplorerComponent(string galleryToolbarSite)
        {
            Platform.CheckForEmptyString(galleryToolbarSite, "galleryToolbarSite");

            _galleryToolbarSite = galleryToolbarSite;

            _items = new ScanItemList(new BindingList<IScanItem>());
        }

        public BindingList<IScanItem> DataSource
        {
            get { return _items.BindingList; }
            private set
            {
                Platform.CheckForNullReference(value, "value");

                CheckForLockedItems();
                if (_items != null)
                    _items.BindingList.ListChanged -= OnBindingListChanged;

                _items = new ScanItemList(value);
                _items.BindingList.ListChanged += OnBindingListChanged;
            }
        }

        public ActionModelRoot GalleryToolbarModel
        {
            get { return _galleryToolbarModel; }
        }

        public ActionModelRoot EditorToolbarModel
        {
            get { return _editorToolbarModel; }
        }

        protected ToolSet ToolSet
        {
            get { return _toolSet; }
            set { _toolSet = value; }
        }


        private void OnBindingListChanged(object sender, ListChangedEventArgs e)
        {
            // TODO(hieupm37)
        }

        private void CheckForLockedItems()
        {
            //if (_items.Any(item => item.IsLocked))
            //    throw new InvalidOperationException("At least one item is currently locked.");
        }

        protected virtual IActionSet CreateToolActions()
        {
            if (_toolSet == null)
                _toolSet = new ToolSet(new TwainExplorerToolExtensionPoint(), new TwainExplorerToolContext(this));
            return new ActionSet(_toolSet.Actions);
        }

        protected virtual string GalleryToolActionsNamespace
        {
            get { return typeof(TwainExplorerComponent).FullName; }
        }

        private IEnumerable<IAction> GetGalleryActions()
        {
            CreateGalleryActions();
            UpdateGalleryActionEnablement();

            yield return _galleryDeleteButtonAction;
            yield return _galleryDeleteAllButtonAction;
        }

        private void CreateGalleryActions()
        {
            _galleryDeleteButtonAction = CreateToolbarAction(
                "delete",
                String.Format("{0}/GalleryToolbarDeleteItem", _galleryToolbarSite), 
                SR.GalleryTooltipDeleteItem, 
                CreateGalleryDeleteIconSet(), 
                DeleteSelectedGalleryItem);
            _galleryDeleteAllButtonAction = CreateToolbarAction(
                "deleteAll",
                String.Format("{0}/GalleryToolbarDeleteAllItems", _galleryToolbarSite),
                SR.GalleryTooltipDeleteAllItems,
                CreateGalleryDeleteAllIconSet(),
                DeleteAllGalleryItems);
        }

        private static IconSet CreateGalleryDeleteIconSet()
        {
            return new IconSet("Icons.GalleryDeleteItemToolSmall.png",
                               "Icons.GalleryDeleteItemToolSmall.png",
                               "Icons.GalleryDeleteItemToolSmall.png");
        }

        private static IconSet CreateGalleryDeleteAllIconSet()
        {
            return new IconSet("Icons.GalleryDeleteAllItemsToolSmall.png",
                               "Icons.GalleryDeleteAllItemsToolSmall.png",
                               "Icons.GalleryDeleteAllItemsToolSmall.png");
        }

        private void UpdateGalleryActionEnablement()
        {
            _galleryDeleteButtonAction.Enabled = 
        }

        private ButtonAction CreateToolbarAction(string id, string path, string tooltip, IconSet iconSet, ClickHandlerDelegate clickHandler)
        {
            id = String.Format("{0}:{1}", typeof(TwainExplorerComponent).FullName, id);
            ButtonAction action = new ButtonAction(id, new ActionPath(path, _resolver), ClickActionFlags.None, _resolver);
            action.IconSet = iconSet;
            action.Tooltip = tooltip;
            action.Label = action.Path.LastSegment.LocalizedText;
            action.SetClickHandler(clickHandler);
            return action;
        }

        public override void Start()
        {
            base.Start();

            _resolver = new ApplicationThemeResourceResolver(GetType(), true);
            ActionSet galleryToolActions = new ActionSet(GetGalleryActions());
            IActionSet toolActions = CreateToolActions();
            IActionSet galleryAllActions = toolActions != null ? toolActions.Union(galleryToolActions) : galleryToolActions;

            _galleryToolbarModel = ActionModelRoot.CreateModel(GalleryToolActionsNamespace, _galleryToolbarSite, galleryAllActions);
            //IActionSet galleryAllActions = 

            //_galleryToolbarModel = ActionModelRoot.CreateModel(
        }

        public override void Stop()
        {
            base.Stop();
            ToolSet.Dispose();
            ToolSet = null;
        }

        public void DeleteSelectedGalleryItem()
        {
            // TODO(hieupm37)
        }

        public void DeleteAllGalleryItems()
        {
            // TODO(hieupm37)
        }
    }
}
