using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Code;
using DataAccess;
using Common.Costant;
using System.Reflection;
using Common;
using Model.TableModel;
using Model.Home;
using Model.CommonModel;
using System.Data.SqlClient;
using Common.Enum;

namespace Business
{
    public class CodeBusiness
    {
        private static CodeDAL _codeDAL = new CodeDAL();
        private static CodeDefectDAL _defecrcodeDAL = new CodeDefectDAL();

        public static IEnumerable<CodeDisplayMode> SearchResult(CodeSearchModel searchModel, out int totalCount)
        {
            List<CodeDisplayMode> models = new List<CodeDisplayMode>();
            List<string> fields = GetCategoryList();

            var date = _codeDAL.SearchModelById().GroupBy(i => i.BCCategory);

            foreach (var item in date)
            {
                CodeDisplayMode model = new CodeDisplayMode
                {
                    Key = item.Key,
                    Value = string.Join(",", item.ToList().Select(i => i.BCCode).ToArray())
                };
                models.Add(model);
            }

            var result = (from field in fields
                          join model in models on field equals model.Key into temp
                          from tt in temp.DefaultIfEmpty()
                          select new CodeDisplayMode { Key = field.ToString(), Value = tt == null ? "" : tt.Value }).ToList();
            if (!string.IsNullOrWhiteSpace(searchModel.Key))
            {
                result = result.Where(i => i.Key == searchModel.Key).ToList();
            }
            totalCount = result.Count;
            return result.AsEnumerable<CodeDisplayMode>();
        }

        public static IEnumerable<CodeDefectModel> DefectCodeSearchResult(DefectCodeSearchModel searchModel, out int totalCount)
        {
            var date = _defecrcodeDAL.SearchCodeDefectPageList(searchModel, out totalCount);
            return date;
        }

        public static List<string> GetCategoryList()
        {
            Type type = typeof(CategoryConstant);
            var fields = CommonHelper.GetConstantValues<string>(type);
            return fields;
        }

        public static CodeInfoModel SaveCode(CodeModel model, UserLoginInfo loginUser)
        {
            var result = new CodeInfoModel() { IsSuccess = true };

            try
            {
                var isOrNotExistModel = _codeDAL.SearchModelByCode(model.BCCode, model.BCCategory).FirstOrDefault();
                if (isOrNotExistModel == null)
                {
                    model.BCIsValid = 1;
                    model.BCCreateUserNo = loginUser.JobNum;
                    model.BCCreateUserName = loginUser.UserName;
                    model.BCCreateTime = DateTime.Now;
                    model.BCOperateUserNo = loginUser.JobNum;
                    model.BCOperateUserName = loginUser.UserName;
                    model.BCOperateTime = DateTime.Now;
                    model.Id = _codeDAL.InsertModel(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString());
                    result.model = model;
                }
                else
                {
                    CodeModel updateModel = new CodeModel();
                    updateModel = isOrNotExistModel;
                    updateModel.BCIsValid = 1;
                    updateModel.BCCodeOrder = model.BCCodeOrder;
                    updateModel.BCOperateUserNo = loginUser.JobNum;
                    updateModel.BCOperateUserName = loginUser.UserName;
                    updateModel.BCOperateTime = DateTime.Now;
                    var updateResult = _codeDAL.Update(updateModel);
                    if (updateResult)
                        result.model = updateModel;
                    else
                    {
                        result.IsSuccess = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static DefectCodeInfoModel SaveDefectCode(CodeDefectModel model, UserLoginInfo loginUser)
        {
            var result = new DefectCodeInfoModel() { IsSuccess = true };

            try
            {
                if (model.Id == 0)
                {
                    model.BDIsValid = 1;
                    model.BDCreateUserNo = loginUser.JobNum;
                    model.BDCreateUserName = loginUser.UserName;
                    model.BDCreateTime = DateTime.Now;
                    model.BDOperateUserNo = loginUser.JobNum;
                    model.BDOperateUserName = loginUser.UserName;
                    model.BDOperateTime = DateTime.Now;
                    model.Id = _defecrcodeDAL.InsertModel(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString());
                    result.model = model;
                }
                else
                {
                    model.BDIsValid = 1;
                    model.BDOperateUserNo = loginUser.JobNum;
                    model.BDOperateUserName = loginUser.UserName;
                    model.BDOperateTime = DateTime.Now;
                    var updateResult = _defecrcodeDAL.Update(model);
                    if (updateResult)
                        result.model = model;
                    else
                    {
                        result.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static bool DeleteCode(int id, UserLoginInfo loginUser)
        {
            try
            {
                var model = _codeDAL.SearchModelById(id).FirstOrDefault();
                model.BCIsValid = 0;
                model.BCOperateUserNo = loginUser.JobNum;
                model.BCOperateUserName = loginUser.UserName;
                model.BCOperateTime = DateTime.Now;
                return _codeDAL.Update(model);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// init Delete Page
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<CodeDeletePageModel> InitCodeDeletePage(string key)
        {
            var result = _codeDAL.SearchModelByCategory(key)
                 .Select(i => new CodeDeletePageModel
                 {
                     Id = i.Id,
                     Category = i.BCCategory,
                     Value = i.BCCode
                 }).ToList();
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CodeDefectModel GetOneDefectCode(int id)
        {
            var result = _defecrcodeDAL.GetOneDefectCode(id);
            return result;
        }


        /// <summary>
        /// DefectCode Type
        /// </summary>
        /// <returns></returns>
        public static List<CodeDefectModel> GetDefectCodeTypeList()
        {
            var list = _defecrcodeDAL.GetDefectCodeTypeList();
            return list;
        }

        /// <summary>
        /// DefectCode
        /// </summary>
        /// <returns></returns>
        public static List<CodeDefectModel> GetDefectCodeByType(string type)
        {
            var list = _defecrcodeDAL.GetDefectCodeByType(type);
            return list;
        }

        public static ResultInfoModel InitialData()
        {
            var result = new ResultInfoModel { IsSuccess = false };
            using (var con = new SqlConnection(NewSqlHelper.ConnectionString))
            {
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    var now = DateTime.Now;
                    var code1 = new CodeModel
                    {
                        BCCode = "Assembling",
                        BCCodeDesc = "Assembling",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 1,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code1);
                    var code2 = new CodeModel
                    {
                        BCCode = "Molding",
                        BCCodeDesc = "Molding",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 2,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code2);
                    var code3 = new CodeModel
                    {
                        BCCode = "Press Molding",
                        BCCodeDesc = "Press Molding",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 3,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code3);
                    var code4 = new CodeModel
                    {
                        BCCode = "3D Blow Molding",
                        BCCodeDesc = "3D Blow Molding",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 4,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code4);
                    var code5 = new CodeModel
                    {
                        BCCode = "3D Blow Assembling",
                        BCCodeDesc = "3D Blow Assembling",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 5,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code5);
                    var code6 = new CodeModel
                    {
                        BCCode = "Foaming",
                        BCCodeDesc = "Foaming",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 6,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code6);
                    var code7 = new CodeModel
                    {
                        BCCode = "Other",
                        BCCodeDesc = "Other",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 7,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code7);
                    var code8 = new CodeModel
                    {
                        BCCode = "All",
                        BCCodeDesc = "All",
                        BCCategory = "ProblemProcess",
                        BCCodeOrder = 8,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code8);

                    var code21 = new CodeModel
                    {
                        BCCode = "Customer Complaint",
                        BCCodeDesc = "Customer Complaint",
                        BCCategory = "ProblemSource",
                        BCCodeOrder = 1,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code21);

                    var code22 = new CodeModel
                    {
                        BCCode = "Production",
                        BCCodeDesc = "Production",
                        BCCategory = "ProblemSource",
                        BCCodeOrder = 2,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code22);

                    var code23 = new CodeModel
                    {
                        BCCode = "Supplier",
                        BCCodeDesc = "Supplier",
                        BCCategory = "ProblemSource",
                        BCCodeOrder = 3,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code23);

                    var code24 = new CodeModel
                    {
                        BCCode = "Equipment",
                        BCCodeDesc = "Equipment",
                        BCCategory = "ProblemSource",
                        BCCodeOrder = 4,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code24);
                    var code25 = new CodeModel
                    {
                        BCCode = "Tooling",
                        BCCodeDesc = "Tooling",
                        BCCategory = "ProblemSource",
                        BCCodeOrder = 5,
                        BCIsValid = EnabledEnum.Enabled.GetHashCode(),
                        BCCreateUserNo = "00000",
                        BCCreateUserName = "System",
                        BCCreateTime = now,
                        BCOperateUserNo = "00000",
                        BCOperateUserName = "System",
                        BCOperateTime = now
                    };
                    _codeDAL.InsertModel(code25);

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return result;
        }

    }
}
