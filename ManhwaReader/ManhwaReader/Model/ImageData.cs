using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhwaReader.Model
{
    public class ImageData
    {
        private Image _image;
        private Action<ImageData> _imgLoaded;

        public FileInfo File { get; }
        public Image Image
        {
            get
            {
                return _image;
            }
        }

        public ImageData (string filePath, Action<ImageData> imageLoaded = null)
        {
            File = new FileInfo(filePath);
            _imgLoaded = imageLoaded;
        }

        public ImageData(FileInfo fileInfo, Action<ImageData> imageLoaded = null)
        {
            File = fileInfo;
            _imgLoaded = imageLoaded;
        }

        public void LoadImageAsync ()
        {
            if (_image == null)
            {
                Task.Run(() =>
                {
                    _image = Image.FromFile(File.FullName);
                    _imgLoaded(this);
                });
            }
        }
        
        public void Dispose ()
        {
            if (_image != null)
            {
                _image.Dispose();
                _image = null;
            }
        }
    }
}
