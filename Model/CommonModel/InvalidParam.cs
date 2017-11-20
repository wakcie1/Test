using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.CommonModel
{
    public class InvalidParam
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        public string JobNum { get; set; }

        public string Name { get; set; }

        public DateTime InvalidTime { get; set; }

    }
}
