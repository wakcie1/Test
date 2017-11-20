using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO; 
using System.Text; 
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    public class UDownHelperController : Controller
    {
        #region  Files's upload
        /// <summary>
        /// Upload Files
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns> 
        public static string UpLoadFile(HttpPostedFileBase file, out string error)
        {
            StringBuilder strbuild = new StringBuilder();
            string FileName;
            string savePath = string.Empty;
             error = string.Empty;

            if (file == null || file.ContentLength <= 0)
            {

                error = "文件不能为空";
                return savePath;
            }
            else
            {
                string fileName = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = Path.GetExtension(fileName);//获取上传文件的扩展名
                string NoFileName = Path.GetFileNameWithoutExtension(fileName);//获取无扩展名的文件名  
                //int Maxsize = 4000 * 1024;
                int Maxsize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFileSize"]);
                string FileType = ConfigurationManager.AppSettings["FileType"];

                FileName = NoFileName + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return savePath;
                }
                if (filesize >= Maxsize*1024)
                {
                    error = "上传文件超过4M，不能上传";
                    return savePath;
                } 
                string path =  ConfigurationManager.AppSettings["FilePath"];
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
                return savePath;
            }
        }
        #endregion

        #region Image's upload
      

        /// <summary>  
        /// .NET MVC模式下的图片上传  
        /// </summary>  
        /// <param name="aImage">要上传的图片</param>  
        /// <param name="ImageName">上传成功后的路径</param>  
        /// <param name="SaveThumb">是否保存</param>  
        /// <returns>返回null表示上传成功</returns>  
        public static object SaveImage(HttpPostedFileBase aImage, ref string ImageName, bool SaveThumb)
        {
            object result = new object();
            string ExtName = Path.GetExtension(aImage.FileName).ToLower();
            if (ExtName == ".jpg" || ExtName == ".jpeg" || ExtName == ".png" || ExtName == ".gif")
            {
                int fMaxImageSize = Convert.ToInt32(ConfigurationManager.AppSettings["MaxImageSize"]);//单位 kb  
                if (aImage.ContentLength > fMaxImageSize * 1024)
                {
                    result = new { Result = false, Msg = "图片超过限定大小！" };
                }
                else
                {
                    //ImageName = Guid.NewGuid().ToString() + ExtName;//图片名称  
                    ImageName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ExtName;//图片名称  
                    string UploadFolder = AppDomain.CurrentDomain.BaseDirectory + "Upload/Images/";
                    if (!Directory.Exists(UploadFolder))
                    {
                        Directory.CreateDirectory(UploadFolder);//若不存图片文件夹则创建  
                    }
                    Image fSaveImage = Image.FromStream(aImage.InputStream);
                    fSaveImage.Save(UploadFolder + ImageName);
                    if (SaveThumb == true)
                    {
                        string ThumbUploadFolder = AppDomain.CurrentDomain.BaseDirectory + "Upload/Images/Thumb/";
                        int ThumbImageLength = int.Parse(ConfigurationManager.AppSettings["ThumbImageLength"]);
                        if (fSaveImage.Height > ThumbImageLength || fSaveImage.Width > ThumbImageLength)
                        {//原图高宽至少一项大于设定值  
                            Image fThumb;
                            if (fSaveImage.Height > fSaveImage.Width)
                            {//高大于宽，以高为主  
                                double p = (double)fSaveImage.Height / (double)ThumbImageLength;
                                fThumb = fSaveImage.GetThumbnailImage((int)(fSaveImage.Width / p),
                                    ThumbImageLength,
                                    new Image.GetThumbnailImageAbort(ThumbnailCallback),
                                    IntPtr.Zero);

                            }
                            else
                            { //宽大于高，以宽为主  
                                double p = (double)fSaveImage.Width / (double)ThumbImageLength;
                                fThumb = fSaveImage.GetThumbnailImage(ThumbImageLength,
                                    (int)(fSaveImage.Height / p),
                                    new Image.GetThumbnailImageAbort(ThumbnailCallback),
                                    IntPtr.Zero);
                            }
                            fThumb.Save(ThumbUploadFolder + ImageName);
                        }
                        else
                        {
                            fSaveImage.Save(ThumbUploadFolder + ImageName);
                        }
                    }
                    fSaveImage.Dispose();
                    return null;
                }
            }
            else
            {
                result = new { Result = false, Msg = "图片格式不正确，只允许上传jpg,png,gif！" };
            }
            return result;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }

        /// <summary>  
        /// 删除图片  
        /// </summary>  
        /// <param name="aImageName">要删除的图片名称</param>  
        public static void DeleteImage(string aImageName)
        {
            //删除旧图片  
            if (!string.IsNullOrEmpty(aImageName))
            { //原图片不是默认图片，需要清理  
                string fImagePath = AppDomain.CurrentDomain.BaseDirectory + "Upload/Images/" + aImageName;
                string fImageThumbPath = AppDomain.CurrentDomain.BaseDirectory + "Upload/Images/Thumb/" + aImageName;
                if (System.IO.File.Exists(fImagePath))
                {
                    System.IO.File.Delete(fImagePath);
                }
                if (System.IO.File.Exists(fImageThumbPath))
                {
                    System.IO.File.Delete(fImageThumbPath);
                }
            }
        }
        #endregion 
    }
}
