using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class ImgUploadResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message
        {
            get { return _message ?? string.Empty; }
            set { _message = value; }
        }
        private string _message = string.Empty;

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<ImgData> data { get; set; }
    }

    public class ImgData
    {
        /// <summary>
        /// 图片访问地址
        /// </summary>
        public string ImgUrl
        {
            get { return _imgurl ?? string.Empty; }
            set { _imgurl = value; }
        }
        /// <summary>
        /// 图片虚拟路径
        /// </summary>
        public string ImgLocalPath
        {
            get { return _imglocalpath ?? string.Empty; }
            set { _imglocalpath = value; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImgName
        {
            get { return _imgname ?? string.Empty; }
            set { _imgname = value; }
        }
        private string _imgname = string.Empty;
        private string _imglocalpath = string.Empty;
        private string _imgurl = string.Empty;
    }


}
