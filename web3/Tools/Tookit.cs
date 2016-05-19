using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Drawing;

namespace web3.Tools
{
    public class Tookit
    {
        /// <summary>
        /// 生成MD5
        /// </summary>
        /// <param name="strPwd"></param>
        /// <returns></returns>
        public static string md5(string strPwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);//将字符编码为一个字节序列 
            byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public static string getimageRandom()
        {
            Random ro = new Random(unchecked((int)DateTime.Now.Ticks));
            String imageRandom = ro.Next().ToString();
            return imageRandom;
        }

		/**//// <summary> 
			/// 生成缩略图 
			/// </summary> 
			/// <param name="originalImagePath">源图路径（物理路径）</param> 
			/// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
			/// <param name="width">缩略图宽度</param> 
			/// <param name="height">缩略图高度</param> 
			/// <param name="mode">生成缩略图的方式</param>     
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
		{
			Image originalImage = Image.FromFile(originalImagePath);

			int towidth = width;
			int toheight = height;

			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;

			switch (mode)
			{
				case "HW"://指定高宽缩放（可能变形）                 
					break;
				case "W"://指定宽，高按比例                     
					toheight = originalImage.Height * width / originalImage.Width;
					break;
				case "H"://指定高，宽按比例 
					towidth = originalImage.Width * height / originalImage.Height;
					break;
				case "Cut"://指定高宽裁减（不变形）                 
					if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
					{
						oh = originalImage.Height;
						ow = originalImage.Height * towidth / toheight;
						y = 0;
						x = (originalImage.Width - ow) / 2;
					}
					else
					{
						ow = originalImage.Width;
						oh = originalImage.Width * height / towidth;
						x = 0;
						y = (originalImage.Height - oh) / 2;
					}
					break;
				default:
					break;
			}

			//新建一个bmp图片 
			Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

			//新建一个画板 
			Graphics g = System.Drawing.Graphics.FromImage(bitmap);

			//设置高质量插值法 
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

			//设置高质量,低速度呈现平滑程度 
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//清空画布并以透明背景色填充 
			g.Clear(Color.Transparent);

			//在指定位置并且按指定大小绘制原图片的指定部分 
			g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
				new Rectangle(x, y, ow, oh),
				GraphicsUnit.Pixel);

			try
			{
				//以jpg格式保存缩略图 
				bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch (System.Exception e)
			{
				throw e;
			}
			finally
			{
				originalImage.Dispose();
				bitmap.Dispose();
				g.Dispose();
			}
		}
	}
}