using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace School.Shared.CustomModels
{
    public class Thumbnails
    {
        #region File Upload Coding

        public byte[] pstFileR(HttpPostedFile passfile)
        {
            BinaryReader a = new BinaryReader(passfile.InputStream);
            return a.ReadBytes(passfile.ContentLength);
        }

        // Upload Thumbnail
        public static String UploadImage(Stream stream, String savePath, String strFileName, String prefix, Int32 height, Int32 width, bool isSame)
        {
            String fileName = String.Empty;

            try
            {
                if (!isSame)
                    strFileName = Guid.NewGuid() + strFileName;

                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

                int srcWidth = image.Width;
                int srcHeight = image.Height;
                int intWidth = image.Width;
                int intHeight = image.Height;

                if (intWidth != width)
                    intWidth = width;

                if (intHeight != height)
                    intHeight = height;

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(intWidth, intHeight);
                System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bmp);

                gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, intWidth, intHeight);

                gr.DrawImage(image, rectDestination, 0, 0, srcWidth, srcHeight, System.Drawing.GraphicsUnit.Pixel);

                fileName = strFileName;

                strFileName = String.IsNullOrEmpty(prefix) ? strFileName : prefix + strFileName;

                savePath = savePath + strFileName;

                bmp.Save(savePath);

                bmp.Dispose();

                image.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileName;
        }

        // Upload Icons
        public static String UploadIcons(String savePath, String strFileName, int i)
        {
            String fileName = String.Empty;

            try
            {

                strFileName = Guid.NewGuid() + strFileName;

                String path = String.Empty;

                path = savePath + strFileName;
                HttpContext.Current.Request.Files[i].SaveAs(path);
                return strFileName;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return fileName;
        }

        #endregion

    }
}
