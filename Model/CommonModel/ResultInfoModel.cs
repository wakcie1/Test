using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class ResultInfoModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message
        {
            get { return _message ?? string.Empty; }
            set { _message = value; }
        }
        private string _message = string.Empty;

    }

    public class Importresult
    {
        /// <summary>
        /// 一共多少条
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 成功条数
        /// </summary>
        public int SuccessTotal { get; set; }

        /// <summary>
        /// 失败条数
        /// </summary>
        public int FalseTotal { get; set; }

        public List<FalseInfo> FalseInfo { get; set; }

    }

    public class FalseInfo
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Identification { get; set; }

        public string Message { get; set; }
    }
}
