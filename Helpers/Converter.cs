using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Helpers
{
    public class Converter
    {
        public static Bitmap ByteToImage(byte[] byteArray)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(byteArray, 0, byteArray.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap bm = new Bitmap(mStream);
                return bm;
            }
        }
        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
