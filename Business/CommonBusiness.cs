using Common.Costant;
using DataAccess;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class CommonBusiness
    {
        private static CommonDAL _commonDal = new CommonDAL();

        public static List<CodeModel> GetProcessList()
        {
            var list = _commonDal.GetCodeList(CategoryConstant.Process);

            return list;
            //list.Select(i => new  {
            //      Value= i.BCCode,
            //     Text = i.BCCodeDesc}).ToList();
        }

        public static List<CodeModel> GetRequireTypeList()
        {
            var list = _commonDal.GetCodeList(CategoryConstant.RequireType);

            return list;
        }

        public static List<CodeModel> GetSourceList()
        {
            var list = _commonDal.GetCodeList(CategoryConstant.Source);

            return list;
        }

        public static List<CodeModel> GetPhraseList()
        {
            var list = _commonDal.GetCodeList(CategoryConstant.Phrase);

            return list;
        }
    }
}
