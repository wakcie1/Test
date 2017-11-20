using Common.Costant;
using System.Web;
using System;
using System.IO;
using Model.CommonModel;

namespace Common
{
    /// <summary>
    /// UploadHelper
    /// </summary>
    public class UploadHelper
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">文件流</param>
        /// <param name="iisRootPath">IIS本地文件夹路径</param>
        public static ImgData ImageUpload(HttpPostedFileBase file, string iisRootPath)
        {
            var result = new ImgData();
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var generateFolder = DateTime.Now.ToString(CommonConstant.DateTimeFormatDayOnly);
                    //随机文件名
                    var generateFileName = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), Path.GetExtension(file.FileName));
                    //相对路径（\Imgaes\XXXX）
                    var localPath = string.Format("{0}{1}", "\\Images\\", generateFolder);
                    //保存本地路径(D:\XXX\Imgaes\XXXX)
                    var uploadFolder = string.Format("{0}{1}", iisRootPath, localPath);
                    //保存图片地址(D:\XXX\Imgaes\XXXX\XXX.JPG)
                    var savepath = string.Format("{0}\\{1}", uploadFolder, generateFileName);
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);
                    file.SaveAs(savepath);
                    //图片URL（http://XXXX/XXX/XXX.JPG）
                    var imgUrl = string.Format("{0}{1}{2}/{3}", ServerInfo.RootURI, "Images/", generateFolder, generateFileName);
                    result.ImgLocalPath = string.Format("{0}\\{1}", localPath, generateFileName);
                    result.ImgName = generateFileName;
                    result.ImgUrl = imgUrl;
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 获取图片URL
        /// </summary>
        /// <param name="imgLocalPath">图片储存相对路径</param>
        /// <returns></returns>
        public static string GetDownLoadUrl(string imgLocalPath)
        {
            var imgUrl = string.Empty;
            try
            {
                //获取网站地址
                string rootUrl = ServerInfo.RootURI;
                //转换路径
                imgLocalPath = imgLocalPath.Replace(@"\", @"/");
                imgUrl = string.Format("{0}{1}", rootUrl, imgLocalPath);
            }
            catch (Exception)
            {
            }
            return imgUrl;
        }


        public static FileData FileUpload(HttpPostedFileBase file, string iisRootPath)
        {
            var result = new FileData();
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    var generateFolder = DateTime.Now.ToString(CommonConstant.DateTimeFormatDayOnly);
                    //随机文件名
                    var generateFileName = string.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(file.FileName), Guid.NewGuid().ToString("N"), Path.GetExtension(file.FileName));
                    //相对路径（\Imgaes\XXXX）
                    var localPath = string.Format("{0}{1}", "\\Files\\", generateFolder);
                    //保存本地路径(D:\XXX\Imgaes\XXXX)
                    var uploadFolder = string.Format("{0}{1}", iisRootPath, localPath);
                    //保存图片地址(D:\XXX\Imgaes\XXXX\XXX.JPG)
                    var savepath = string.Format("{0}\\{1}", uploadFolder, generateFileName);
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);
                    file.SaveAs(savepath);

                    var fileUrl = string.Format("{0}{1}{2}/{3}", ServerInfo.RootURI, "Files/", generateFolder, generateFileName);
                    result.FileLocalPath = string.Format("{0}\\{1}", localPath, generateFileName);
                    result.FileName = file.FileName;
                    result.GenerateFileName = generateFileName;
                    result.FileUrl = fileUrl;
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
    }
}