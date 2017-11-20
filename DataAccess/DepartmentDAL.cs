using Common;
using Common.Enum;
using Model.TableModel;
using Model.ViewModel.Department;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DepartmentDAL : NewSqlHelper
    {
        private const string tableName = "T_BASE_DEPARTMENT";

        /// <summary>
        /// 描述：根据部门Id获取部门信息
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public DepartmentModel GetDpById(int departmentId)
        {
            var departmentInfo = new DepartmentModel();
            SqlParameter[] para = {
                new SqlParameter("@Id", departmentId)
            };

            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                departmentInfo = DataConvertHelper.DataTableToList<DepartmentModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql,null, para))
            //{
            //    if (dr.Read())
            //    {
            //        departmentInfo.Id = Convert.ToInt32(dr["Id"]);
            //        departmentInfo.BDParentId = Convert.ToInt32(dr["BDParentId"]);
            //        departmentInfo.BDDeptName = dr["BDDeptName"].ToString();
            //        departmentInfo.BDDeptDesc = dr["BDDeptDesc"].ToString();
            //        departmentInfo.BDIsMin = Convert.ToInt32(dr["BDIsMin"]);
            //        departmentInfo.BDIsValid = Convert.ToInt32(dr["BDIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        departmentInfo = null;
            //    }
            //}

            return departmentInfo;
        }

        public DepartmentModel GetRootDepartment()
        {
            var departmentInfo = new DepartmentModel();

            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid,BDCreateUserNo,"
                       + "BDCreateUserName,BDCreateTime,BDOperateUserNo,BDOperateUserName,BDOperateTime FROM " + tableName
                + " WITH(NOLOCK) WHERE BDParentId=0";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    departmentInfo = DataConvertHelper.DataTableToList<DepartmentModel>(dt)[0];
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql,null))
            //{
            //    if (dr.Read())
            //    {
            //        departmentInfo.Id = Convert.ToInt32(dr["Id"]);
            //        departmentInfo.BDParentId = Convert.ToInt32(dr["BDParentId"]);
            //        departmentInfo.BDDeptName = dr["BDDeptName"].ToString();
            //        departmentInfo.BDDeptDesc = dr["BDDeptDesc"].ToString();
            //        departmentInfo.BDIsMin = Convert.ToInt32(dr["BDIsMin"]);
            //        departmentInfo.BDIsValid = Convert.ToInt32(dr["BDIsValid"]);
            //        departmentInfo.BDCreateUserNo = dr["BDCreateUserNo"].ToString();
            //        departmentInfo.BDCreateUserName = dr["BDCreateUserName"].ToString();
            //        departmentInfo.BDCreateTime = DateTime.Parse(dr["BDCreateTime"].ToString());
            //        departmentInfo.BDOperateUserNo = dr["BDOperateUserNo"].ToString();
            //        departmentInfo.BDOperateUserName = dr["BDOperateUserName"].ToString();
            //        departmentInfo.BDOperateTime = DateTime.Parse(dr["BDOperateTime"].ToString());
            //        dr.Close();
            //    }
            //    else
            //    {
            //        departmentInfo = null;
            //    }
            //}
            return departmentInfo;
        }

        /// <summary>
        /// 描述：获取所有的组织框架
        /// 创建标识：cpf
        /// 创建时间：2017-9-19 20:37:36
        /// </summary>
        /// <returns></returns>
        public List<DepartmentModel> GetAllDepartMent()
        {
            var list = new List<DepartmentModel>();
            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid,BDCreateUserNo,
                       BDCreateUserName,BDCreateTime,BDOperateUserNo,BDOperateUserName,BDOperateTime FROM " + tableName + " WITH(NOLOCK)";
            var ds = ExecuteDataSet(CommandType.Text, sql);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<DepartmentModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述：根据id获取部门信息
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public DepartmentInfo GetDepartById(int departmentId)
        {
            var departmentInfo = new DepartmentInfo();
            SqlParameter[] para = {
                new SqlParameter("@Id", departmentId)
            };
            var sql = @"SELECT a.Id Id,a.BDParentId ParentId,a.BDDeptName Name,a.BDDeptDesc Description,b.BDDeptName ParentName FROM " + tableName
                + " a WITH(NOLOCK ) LEFT JOIN " + tableName + " b WITH(NOLOCK ) ON a.BDParentId = b.Id WHERE a.Id=@Id";

            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                departmentInfo = DataConvertHelper.DataTableToList<DepartmentInfo>(dt)[0];
            }
            else
            {
                return null;
            }

            //using (var dr = ExecuteReader(CommandType.Text, sql, null,para))
            //{
            //    if (dr.Read())
            //    {
            //        departmentInfo.Id = dr["Id"].ToString();
            //        departmentInfo.ParentId = dr["ParentId"].ToString();
            //        departmentInfo.Name = dr["Name"].ToString();
            //        departmentInfo.Description = dr["Description"].ToString();
            //        departmentInfo.ParentName = dr["ParentName"].ToString();
            //        dr.Close();
            //    }
            //    else
            //    {
            //        departmentInfo = null;
            //    }
            //}

            return departmentInfo;
        }

        /// <summary>
        /// 描述：判断部门名称是否重名
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public bool CheckDepartment(string departmentName)
        {
            SqlParameter[] para = {
                new SqlParameter("@BDDeptName", departmentName),
                new SqlParameter("@BDIsValid", EnabledEnum.Enabled.GetHashCode()),
            };

            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BDDeptName=@BDDeptName AND BDIsValid=@BDIsValid";

            var count = ExecuteScalar(CommandType.Text, sql, null, para);
            return count != null;

        }

        /// <summary>
        /// 描述：根据部门名称查找部门信息
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        public DepartmentModel GetDepartByName(string departmentName)
        {
            var departmentInfo = new DepartmentModel();
            SqlParameter[] para = {
                new SqlParameter("@BDDeptName", departmentName),
                new SqlParameter("@BDIsValid", EnabledEnum.Enabled.GetHashCode()),
            };

            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BDDeptName=@BDDeptName AND BDIsValid=@BDIsValid";
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                departmentInfo = DataConvertHelper.DataTableToList<DepartmentModel>(dt)[0];
            }
            else
            {
                return null;
            }
            //using (var dr = ExecuteReader(CommandType.Text, sql, null, para))
            //{
            //    if (dr.Read())
            //    {
            //        departmentInfo.Id = Convert.ToInt32(dr["Id"]);
            //        departmentInfo.BDParentId = Convert.ToInt32(dr["BDParentId"]);
            //        departmentInfo.BDDeptName = dr["BDDeptName"].ToString();
            //        departmentInfo.BDDeptDesc = dr["BDDeptDesc"].ToString();
            //        departmentInfo.BDIsMin = Convert.ToInt32(dr["BDIsMin"]);
            //        departmentInfo.BDIsValid = Convert.ToInt32(dr["BDIsValid"]);
            //        dr.Close();
            //    }
            //    else
            //    {
            //        departmentInfo = null;
            //    }
            //}
            return departmentInfo;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(DepartmentModel model)
        {
            var sql = @"INSERT INTO " + tableName +
                " (BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid,BDCreateUserNo,BDCreateUserName,BDCreateTime,BDOperateUserNo,BDOperateUserName,BDOperateTime)" +
                " VALUES (@BDParentId,@BDDeptName,@BDDeptDesc,@BDIsMin,@BDIsValid,@BDCreateUserNo,@BDCreateUserName,@BDCreateTime,@BDOperateUserNo,@BDOperateUserName,@BDOperateTime)" +
                " select id = scope_identity()";
            SqlParameter[] para = {
                new SqlParameter("@BDParentId",model.BDParentId),
                new SqlParameter("@BDDeptName",model.BDDeptName),
                new SqlParameter("@BDDeptDesc",model.BDDeptDesc),
                new SqlParameter("@BDIsMin",model.BDIsMin),
                new SqlParameter("@BDIsValid",model.BDIsValid),
                new SqlParameter("@BDCreateUserNo",model.BDCreateUserNo),
                new SqlParameter("@BDCreateUserName",model.BDCreateUserName),
                new SqlParameter("@BDCreateTime",model.BDCreateTime),
                new SqlParameter("@BDOperateUserNo",model.BDOperateUserNo),
                new SqlParameter("@BDOperateUserName",model.BDOperateUserName),
                new SqlParameter("@BDOperateTime",model.BDOperateTime),
            };
            long result = 0;
            var ds = ExecuteDataSet(CommandType.Text, sql.ToString(), null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                var IdString = ds.Tables[0].Rows[0][0].ToString();
                result = string.IsNullOrEmpty(IdString) ? 0 : long.Parse(IdString);
            }
            return result;
        }

        public bool Update(DepartmentModel model)
        {
            var sql = @"UPDATE  " + tableName +
                " SET BDDeptName=@BDDeptName,BDDeptDesc=@BDDeptDesc," +
                "BDOperateUserNo=@BDOperateUserNo,BDOperateUserName=@BDOperateUserName,BDOperateTime=@BDOperateTime WHERE Id=@Id";
            SqlParameter[] para = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@BDDeptName",model.BDDeptName),
                new SqlParameter("@BDDeptDesc",model.BDDeptDesc),
                new SqlParameter("@BDOperateUserNo",model.BDOperateUserNo),
                new SqlParameter("@BDOperateUserName",model.BDOperateUserName),
                new SqlParameter("@BDOperateTime",model.BDOperateTime),
            };
            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }

        /// <summary>
        /// 描述：根据部门Id获取下一级部门信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<DepartmentModel> GetDepartmentByParentId(long parentId)
        {
            var list = new List<DepartmentModel>();
            var departmentInfo = new DepartmentModel();
            SqlParameter[] para = {
                new SqlParameter("@BDParentId", parentId)
            };

            var sql = @"SELECT Id,BDParentId,BDDeptName,BDDeptDesc,BDIsMin,BDIsValid FROM " + tableName
                + " WITH(NOLOCK) WHERE BDParentId=@BDParentId";

            var ds = ExecuteDataSet(CommandType.Text, sql, null, para);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                list = DataConvertHelper.DataTableToList<DepartmentModel>(dt);
            }
            return list;
        }

        /// <summary>
        /// 描述：根据部门Id更新有效性
        /// 创建标识：cpf
        /// 创建时间：2017-9-24 17:50:28
        /// </summary>
        /// <param name="dpId"></param>
        /// <param name="isvalid"></param>
        /// <returns></returns>
        public bool UpdateIsValid(DepartmentModel model)
        {
            var sql = @"UPDATE  " + tableName +
                " SET BDIsValid=@BDIsValid, " +
                " BDOperateUserNo=@BDOperateUserNo,BDOperateUserName=@BDOperateUserName,BDOperateTime=@BDOperateTime WHERE Id=@Id";
            SqlParameter[] para = {
                new SqlParameter("@Id",model.Id),
                new SqlParameter("@BDIsValid",model.BDIsValid),
                new SqlParameter("@BDOperateUserNo",model.BDOperateUserNo),
                new SqlParameter("@BDOperateUserName",model.BDOperateUserName),
                new SqlParameter("@BDOperateTime",model.BDOperateTime),
            };
            return ExecteNonQuery(CommandType.Text, sql, null, para) > 0;
        }
    }
}
