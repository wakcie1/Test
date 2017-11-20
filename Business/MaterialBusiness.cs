using Common;
using Common.Enum;
using DataAccess;
using Model.CommonModel;
using Model.Home;
using Model.Material;
using Model.TableModel;
using Model.ViewModel.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class MaterialBusiness
    {
        private static MaterialDAL _materialDal = new MaterialDAL();
        private static MaToolDAL _matoolDal = new MaToolDAL(); 

        #region Material

        /// <summary>
        /// 描述:保存物料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static MaterialViewModel SaveMaterial(MaterialInfoModel model, UserLoginInfo loginUser)
        {
            var result = new MaterialViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    model.MIIsValid = 1;
                    model.MICreateUserNo = loginUser.JobNum;
                    model.MICreateUserName = loginUser.UserName;
                    model.MICreateTime = DateTime.Now;
                    model.MIOperateUserNo = loginUser.JobNum;
                    model.MIOperateUserName = loginUser.UserName;
                    model.MIOperateTime = DateTime.Now;
                    model.Id = _materialDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }
                else
                {
                    //Update
                    model.MIOperateUserNo = loginUser.JobNum;
                    model.MIOperateUserName = loginUser.UserName;
                    model.MIOperateTime = DateTime.Now;
                    _materialDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 查询物料列表
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<MaterialInfoModel> SearchMaterialPageList(MaterialSearchModel param, out int totalCount)
        {
            var result = _materialDal.SearchMaterialPageList(param, out totalCount);
            return result;
        }

        /// <summary>
        /// 描述：获取物料信息
        /// </summary>
        /// <param name="materialId">物料Id</param>
        /// <returns></returns>
        public static MaterialInfoModel GetMaterialById(int materialId)
        {
            var result = _materialDal.GetMaterialById(materialId);
            return result;
        }

        /// <summary>
        /// 通过Key查询物料
        /// </summary>
        /// <returns></returns>
        public static List<MaterialInfoModel> SapAutoComplete(string key, string workorder)
        {
            var last = new List<MaterialInfoModel>();
            if (!string.IsNullOrEmpty(key))
            {
                last = _materialDal.GetSelectMaterialInfo(key, workorder);
            }
            return last;
        }

        /// <summary>
        /// 通过Key查询workorder
        /// </summary>
        /// <returns></returns>
        public static List<WorkOrderInfo> WorkOrderAutoComplete(string key)
        {
            var last = new List<WorkOrderInfo>();
            if (!string.IsNullOrEmpty(key))
            {
                last = _materialDal.GetSelectWorkOrderInfo(key);
            }
            return last;
        }

        public static bool DeleteMaterial(int id, UserLoginInfo loginUser)
        {
            try
            {
                var model = _materialDal.GetMaterialById(id);
                model.MIIsValid = 0;
                model.MIOperateUserNo = loginUser.JobNum;
                model.MIOperateUserName = loginUser.UserName;
                model.MIOperateTime = DateTime.Now;
                return _materialDal.Update(model);
            }
            catch
            {
                return false;
            }
        }

        #endregion
        #region 设备和工具
        /// <summary>
        /// 查询设备和工具
        /// </summary>
        /// <param name="param"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static List<MaToolModel> SearchMaToolPageList(MaToolSearchModel param, out int totalCount)
        {
            var result = _matoolDal.SearchPageList(param, out totalCount);
            return result;
        }

        /// <summary>
        /// 描述:编辑设备工具
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static MaToolViewModel SaveMaTool(MaToolModel model, UserLoginInfo loginUser)
        {
            var result = new MaToolViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    model.BMIsValid = 1;
                    model.BMCreateUserNo = loginUser.JobNum;
                    model.BMCreateUserName = loginUser.UserName;
                    model.BMCreateTime = DateTime.Now;
                    model.BMOperateUserNo = loginUser.JobNum;
                    model.BMOperateUserName = loginUser.UserName;
                    model.BMOperateTime = DateTime.Now;
                    model.Id = _matoolDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }
                else
                {
                    //Update
                    model.BMOperateUserNo = loginUser.JobNum;
                    model.BMOperateUserName = loginUser.UserName;
                    model.BMOperateTime = DateTime.Now;
                    _matoolDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        ///  删除machine
        ///  创建人：wq
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool DeleteMachine(int id, UserLoginInfo loginUser)
        {
            try
            {
                var model = _matoolDal.GetMaToolById(id);
                model.BMIsValid = 0;
                model.BMOperateUserNo = loginUser.JobNum;
                model.BMOperateUserName = loginUser.UserName;
                model.BMOperateTime = DateTime.Now;
                return _matoolDal.Update(model);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 描述：获取设备工具信息
        /// </summary>
        /// <param name="machineId">物料Id</param>
        /// <returns></returns>
        public static MaToolModel GetMaToolById(int machineId)
        {
            var result = _matoolDal.GetMaToolById(machineId);
            return result;
        }

        ///// <summary>
        ///// 描述：获取设备列表
        ///// </summary>
        ///// <returns></returns>
        public static List<MaToolModel> GetMachineList(string key)
        {
            var result = _matoolDal.GetMainMachineList(key);
            //var query = from tool in result
            //            where tool.BMType == MTTypeEnum.Machine.GetHashCode()
            //            select tool.BMCode;
            //var last = query.ToList<string>();
            return result;
        }

        ///// <summary>
        ///// 描述：获取工具列表
        ///// </summary>
        ///// <returns></returns>
        public static List<MaterialToolModel> GetToolList(string key)
        {
            var result = _materialDal.GetMaterialToolList(key);
            //var query = from tool in result
            //            where tool.BMType == MTTypeEnum.Tool.GetHashCode()
            //            select tool.BMCode;
            //var last = query.ToList<string>();
            return result;
        }
        #endregion
        #region
        public static List<WorkOrderInfo> MaterialOtherSearch(MaterialOtherSearchModel model,out int totalCount)
        {
            var result = _materialDal.MaterialOtherSearch(model,out totalCount); 
            return result;
        }

        public static MaterialWorkOrderViewModel SaveWorkOrder(WorkOrderInfo model, UserLoginInfo loginUser)
        {
            var result = new MaterialWorkOrderViewModel() { IsSuccess = true }; 
            try
            {
                //add
                if (model.Id == 0)
                {
                    model.WIIsValid = 1;
                    model.WICreateUserNo = loginUser.JobNum;
                    model.WICreateUserName = loginUser.UserName;
                    model.WICreateTime = DateTime.Now;
                    model.WIOperateUserNo = loginUser.JobNum;
                    model.WIOperateUserName = loginUser.UserName;
                    model.WIOperateTime = DateTime.Now;
                    model.Id = _materialDal.InsertWorkOrder(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }
                else
                {
                    //Update
                    model.WIOperateUserNo = loginUser.JobNum;
                    model.WIOperateUserName = loginUser.UserName;
                    model.WIOperateTime = DateTime.Now;
                    _materialDal.UpdateWorkOrder(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static WorkOrderInfo GetWorkOrderById(int workOrderId)
        {
            var result = _materialDal.GetWorkOrderById(workOrderId);
            return result;
        }
        /// <summary>
        /// 删除
        /// 创建人：wq
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool DeleteWorkOrder(int workOrderId, UserLoginInfo loginUser)
        {
            try
            {
                var model = _materialDal.GetWorkOrderById(workOrderId);
                model.WIIsValid = 0;
                model.WIOperateUserNo = loginUser.JobNum;
                model.WIOperateUserName = loginUser.UserName;
                model.WIOperateTime = DateTime.Now;
                return _materialDal.UpdateWorkOrder(model);
            }
            catch
            {
                return false;
            }
        }

        #endregion
        #region MaterialTool
        public static MaterialToolViewModel SaveMaterialTool(MaterialToolModel model, UserLoginInfo loginUser)
        {
            var result = new MaterialToolViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    model.MTIsValid = 1;
                    model.MTCreateUserNo = loginUser.JobNum;
                    model.MTCreateUserName = loginUser.UserName;
                    model.MTCreateTime = DateTime.Now;
                    model.MTOperateUserNo = loginUser.JobNum;
                    model.MTOperateUserName = loginUser.UserName;
                    model.MTOperateTime = DateTime.Now;
                    model.Id = _materialDal.InsertMaterialTool(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }
                else
                {
                    //Update
                    model.MTOperateUserNo = loginUser.JobNum;
                    model.MTOperateUserName = loginUser.UserName;
                    model.MTOperateTime = DateTime.Now;
                    _materialDal.UpdateMaterialTool(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                }

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static List<MaterialToolModel> MaterialToolSearch(MaterialToolSearchModel model, out int totalCount)
        {
            var result = _materialDal.MaterialToolSearch(model, out totalCount);
            return result;
        }

        public static MaterialToolModel GetMaterialToolById(int materialToolId)
        {
            var result = _materialDal.GetMaterialToolById(materialToolId);
            return result;
        }

        public static bool DeleteMaterialTool(int id, UserLoginInfo loginUser)
        {
            try
            {
                var model = _materialDal.GetMaterialToolById(id);
                model.MTIsValid = 0;
                model.MTOperateUserNo = loginUser.JobNum;
                model.MTOperateUserName = loginUser.UserName;
                model.MTOperateTime = DateTime.Now;
                return _materialDal.UpdateMaterialTool(model);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
