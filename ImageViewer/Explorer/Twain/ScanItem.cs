using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ClearCanvas.Desktop;
using ClearCanvas.Common;
using System.Threading;

namespace ClearCanvas.ImageViewer.Explorer.Twain
{
    public interface IScanItem
    {
        // Original scan image
        IPresentationImage Image { get; }

        Rectangle DisplayRectangle { get; }

        bool IsLocked { get; }

        void Lock();

        void Unlock();
    }

    public class ScanItem : IScanItem, IGalleryItem
    {
        private IPresentationImage _image;
        private Image _thumbnail;  // Maybe no need
        private string _name;
        private string _description;
        private Rectangle _displayRectangle;
        private int _lockCount;

        public ScanItem(IPresentationImage image, Image thumbnail, string name, string description, Rectangle displayRectangle)
        {
            Platform.CheckForNullReference(image, "image");
            Platform.CheckForNullReference(thumbnail, "thumbnail");

            _image = image;
            _thumbnail = thumbnail;
            _name = name ?? string.Empty;
            _description = description ?? string.Empty;
            _displayRectangle = displayRectangle;
        }

        public IPresentationImage Image
        {
            get { return _image; }
        }

        public Image Thumbnail
        {
            get { return _thumbnail; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Description
        {
            get { return _description; }
        }

        public Rectangle DisplayRectangle
        {
            get { return _displayRectangle; }
        }

        public void Lock()
        {
            Interlocked.Increment(ref _lockCount);
        }

        public void Unlock()
        {
            Interlocked.Decrement(ref _lockCount);
        }

        public bool IsLocked
        {
            get { return Thread.VolatileRead(ref _lockCount) != 0; }
        }

        public void Dispose()
        {
            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }
            if (_thumbnail != null)
            {
                _thumbnail.Dispose();
                _thumbnail = null;
            }
        }

        #region IGalleryItem implementation

        object IGalleryItem.Image
        {
            get { return Thumbnail; }
        }

        string IGalleryItem.Name
        {
            get { return Name; }
            set { throw new InvalidOperationException("Rename thumbnail item is not supported"); }
        }

        string IGalleryItem.Description
        {
            get { return Description; }
        }

        object IGalleryItem.Item
        {
            get { return Image; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        #endregion
    }
}
