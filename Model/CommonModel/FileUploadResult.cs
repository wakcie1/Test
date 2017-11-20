using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class FileUploadResult
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
        /// 文件列表
        /// </summary>
        public List<FileData> data { get; set; }
    }

    public class FileData
    {
        /// <summary>
        /// 文件访问地址
        /// </summary>
        public string FileUrl
        {
            get { return _fileurl ?? string.Empty; }
            set { _fileurl = value; }
        }
        /// <summary>
        /// 文件虚拟路径
        /// </summary>
        public string FileLocalPath
        {
            get { return _filelocalpath ?? string.Empty; }
            set { _filelocalpath = value; }
        }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get { return _filename ?? string.Empty; }
            set { _filename = value; }
        }

        public string GenerateFileName
        {
            get { return _generateFileName ?? string.Empty; }
            set { _generateFileName = value; }
        }
        private string _filename = string.Empty;
        private string _generateFileName = string.Empty;
        private string _filelocalpath = string.Empty;
        private string _fileurl = string.Empty;
    }


}
