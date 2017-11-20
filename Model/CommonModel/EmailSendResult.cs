using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class EmailSendModel
    {
        /// <summary>
        /// 发送地址
        /// </summary>
        public string MailAddress { get; set; }


        /// <summary>
        /// 发送标题
        /// </summary>
        public string MailTitle { get; set; }


        /// <summary>
        /// 发送内容
        /// </summary>
        public string MailContent { get; set; }
    }

    public class EmailSendResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 消息结果
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ExceptionMessage { get; set; }
    }

}
