using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClearCanvas.Desktop.Actions;
using ClearCanvas.Desktop;
using WIA;
using System.Runtime.InteropServices;
using System.IO;
using ClearCanvas.ImageViewer.Graphics;
using ClearCanvas.ImageViewer.StudyManagement;

namespace ClearCanvas.ImageViewer.Explorer.Twain.View.WinForms
{
    public partial class TwainExplorerControl : UserControl
    {
        private const int MARGIN = 5;

        private TwainExplorerComponent _component;

        public TwainExplorerControl(TwainExplorerComponent component)
        {
            _component = component;

            InitializeComponent();
            InitializeIcons();
            InitializeGallery();
        }

        private static void InitializeImageList(ImageList imageList, string sizeString)
        {
            Type type = typeof(TwainExplorerControl);
            var resourceResolver = new ActionResourceResolver(type);

            string[] icons = { "Scan" };
            foreach (string iconName in icons)
            {
                var resourceName = string.Format("{0}.Icons.{1}Tool{2}.png", type.Namespace, iconName, sizeString);
                using (var ioStream = resourceResolver.OpenResource(resourceName))
                {
                    if (ioStream == null)
                        continue;
                    imageList.Images.Add(iconName, Image.FromStream(ioStream));
                }
            }
        }

        private ImageList GetImageList(IconSize iconSize)
        {
            if (iconSize == IconSize.Small)
                return _smallIconImageList;

            if (iconSize == IconSize.Medium)
                return _mediumIconImageList;

            return _largeIconImageList;
        }

        private void InitializeIcons()
        {
            InitializeImageList(_largeIconImageList, "Large");
            InitializeImageList(_mediumIconImageList, "Medium");
            InitializeImageList(_smallIconImageList, "Small");

            _toolStrip.ImageList = GetImageList(Settings.Default.ToolbarIconSize);

            _btnScan.ImageKey = @"Scan";
        }

        private void InitializeGallery()
        {
            _galleryView.DataSource = _component.DataSource;
            _galleryView.MultiSelect = false;
            _galleryView.DragReorder = true;
            _galleryView.SelectionChanged += new EventHandler(_galleryView_SelectionChanged);
        }

        void _galleryView_SelectionChanged(object sender, EventArgs e)
        {
            ISelection selection = _galleryView.Selection;
            if (selection.Item != null)
            {
                ScanItem selectedItem = (ScanItem)selection.Item;
                ColorPresentationImage cpi = (ColorPresentationImage)selectedItem.Image;
                _pictureBox.Image = cpi.DrawToBitmap(cpi.ImageGraphic.Columns, cpi.ImageGraphic.Rows);
            }
            else
            {
                _pictureBox.Image = null;
            }
        }

        private abstract class SSFormatID
        {
            public const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
            public const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
        }

        private void _btnScan_Click(object sender, EventArgs e)
        {
            ImageFile scannedImage = null;
            try
            {
                var wiaDlg = new WIA.CommonDialog();
                scannedImage = wiaDlg.ShowAcquireImage(
                    WiaDeviceType.ScannerDeviceType,
                    WiaImageIntent.UnspecifiedIntent,
                    WiaImageBias.MaximizeQuality,
                    SSFormatID.wiaFormatJPEG,
                    true,
                    true,
                    false);
            }
            catch (COMException ex)
            {
                MessageBox.Show("Scan failed with code: " + ex.ErrorCode);
            }

            if (scannedImage != null)
            {
                var imageBytes = (byte[])scannedImage.FileData.get_BinaryData();
                using (var ms = new MemoryStream(imageBytes))
                {
                    // Start wait cursor
                    Cursor.Current = Cursors.WaitCursor;

                    Image img = Image.FromStream(ms);
                    var bmp = new Bitmap(img);  // Convert to bitmap

                    var presentImage = new ColorPresentationImage(img.Height, img.Width);
                    ColorImageGraphic colorImage = presentImage.ImageGraphic;
                    try
                    {
                        colorImage.PixelData.ForEachPixel(
                            delegate(int i, int x, int y, int pixelIndex)
                            {
                                colorImage.PixelData.SetPixel(x, y, bmp.GetPixel(x, y));
                            });
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    Image thumbnail = IconCreator.CreatePresentationImageIcon(presentImage, _galleryView.DisplayRectangle);
                    _component.DataSource.Add(new ScanItem(presentImage,
                        thumbnail, @"Scanned Image", @"Sample", _galleryView.DisplayRectangle));

                    // Restor cursor
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void TwainExplorerControl_Resize(object sender, EventArgs e)
        {
            int toolStripHeight = _toolStrip.Height;

            _galleryView.Location = new Point(0, toolStripHeight);
            _galleryView.Width = this.Width;

            int pbx = toolStripHeight + _galleryView.Height + MARGIN;
            _pictureBox.Location = new Point(0, pbx);
            _pictureBox.Width = this.Width / 2 - MARGIN / 2;
            _pictureBox.Height = this.Height - pbx;
        }
    }
}
