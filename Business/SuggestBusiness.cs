using Common;
using Common.Enum;
using DataAccess;
using Model.Home;
using Model.Suggest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class SuggestBusiness
    {
        private static SuggestDAL _suggestDAL = new SuggestDAL();

        public static IEnumerable<SuggestionsInfoModel> SearchSuggestInfoList(SuggestionsSearchModel model, out int totalCount)
        {
            var data = _suggestDAL.SearchSuggestInfoList(model);
            totalCount = data.Count();
            return data;
        }

        public static SuggestionModel SaveSuggest(SuggestionsInfoModel model, UserLoginInfo loginUser)
        {
            var result = new SuggestionModel() { IsSuccess = true };
            try {
                if (model.Id == 0)
                {
                    model.BFIsValid = 1;
                    model.BFStatus = 1;
                    model.BFCreateUserNo = loginUser.JobNum;
                    model.BFCreateUserName = loginUser.UserName;
                    model.BFCreateTime = DateTime.Now;
                    model.BFOperateUserNo = loginUser.JobNum;
                    model.BFOperateUserName = loginUser.UserName;
                    model.BFOperateTime = DateTime.Now;
                    model.BFFeedBackComment = string.Empty;
                    model.BFRespUserNo = string.Empty;
                    model.BFRespName = string.Empty;
                    model.Id = _suggestDAL.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString());
                    result.model = model;
                }
                else {
                    model.BFOperateUserNo = loginUser.JobNum;
                    model.BFOperateUserName = loginUser.UserName;
                    model.BFOperateTime = DateTime.Now;
                    var update = _suggestDAL.Update(model);
                    if (!update) result.IsSuccess = false;
                    result.model = model;
                }
            }
            catch(Exception ex){
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static SuggestionsInfoModel GetSuggesById(long userIdlong)
        {
            var data = _suggestDAL.GetSuggesById(userIdlong); 
            return data;
        }
    }
}
