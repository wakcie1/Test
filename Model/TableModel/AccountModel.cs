using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.TableModel
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string BAAccount { get; set; }

        public string BAPassword { get; set; }

        public int BAUserId { get; set; }

        public int BAType { get; set; }

        public int BAIsValid { get; set; }

        public string BACreateUserNo { get; set; }

        public string BACreateUserName { get; set; }

        public DateTime BACreateTime { get; set; }

        public string BAOperateUserNo { get; set; }

        public string BAOperateUserName { get; set; }

        public DateTime BAOperateTime { get; set; }
    }
}
