using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class ImportUploadResult : ResultInfoModel
    {
        /// <summary>
        /// 未导入数据
        /// </summary>
        public List<InvalidData> invalidData { get; set; }
    }

    public class InvalidData
    {
        public string Key
        {
            get { return _key ?? string.Empty; }
            set { _key = value; }
        }

        public string Value1
        {
            get { return _value1 ?? string.Empty; }
            set { _value1 = value; }
        }

        public string Value2
        {
            get { return _value2 ?? string.Empty; }
            set { _value2 = value; }
        }

        private string _value1 = string.Empty;
        private string _value2 = string.Empty;
        private string _key = string.Empty;

    }


}
