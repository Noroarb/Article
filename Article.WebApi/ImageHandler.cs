using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace Article.WebApi
{
    public class ImageHandler : IHttpHandler

    {
        public void ProcessRequest(HttpContext context)

        {
            try
            {
                //Get Height and width from Query string

                int Width = 0, Height = 0;
                try
                {
                    Width = Int32.Parse(context.Request.QueryString["width"]);
                }
                catch
                { }

                try
                {
                    Height = Int32.Parse(context.Request.QueryString["height"]);
                }
                catch
                { }

                // int Height = Int32.Parse(context.Request.QueryString["height"]);


                //Get Picture from Server

                //  string abc = context.Server.MapPath(context.Request.FilePath.ToString());

                var path = context.Server.MapPath(context.Request.FilePath.ToString().Replace("kiwi.ashx", ""));

                Image img = Image.FromFile(path);
                if (Width == 0)
                    Width = (int)(((double)img.Height / img.Width) * Height);
                else if (Height == 0)
                    Height = (int)(((double)img.Height / img.Width) * Width);
                if (Height == 0 && Width == 0)
                {
                    Width = img.Width;
                    Height = img.Height;
                }

                Image _img = new Bitmap(Width, Height);
                //long len = new System.IO.FileInfo(context.Server.MapPath(context.Request.FilePath.ToString().Replace("kiwi.ashx", ""))).Length;

                Graphics graphics = Graphics.FromImage(_img);

                var extension = Path.GetExtension(path);

                //Resize picture according to size

                // graphics.DrawImage(img, 0, 0, Width, Height);
                Rectangle rect = new Rectangle(0, 0, Width, Height);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.DrawImage(img, rect);
                //graphics.DrawImageUnscaled()


                graphics.Dispose();



                //Create outpur sream

                MemoryStream str = new MemoryStream();

                _img = _img.GetThumbnailImage(Width, Height, null, IntPtr.Zero);
                img.Dispose();
                switch (extension/*context.Request.CurrentExecutionFilePathExtension*/)
                {
                    case ".jpg": _img.Save(str, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".png": _img.Save(str, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: _img.Save(str, System.Drawing.Imaging.ImageFormat.Png); break;
                }


                _img.Dispose();

                str.WriteTo(context.Response.OutputStream);

                //len = str.Length; 

                str.Dispose();

                str.Close();

                //Set response type

                context.Response.ContentType = context.Request.CurrentExecutionFilePathExtension;

                context.Response.End();
            }
            catch(Exception e) { }

        }

        public bool IsReusable

        {

            get

            {

                return false;

            }

        }
    }
}