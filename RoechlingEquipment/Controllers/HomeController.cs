
using Business;
using Common;
using Common.Costant;
using Common.Enum;
using Model.CommonModel;
using Model.Home;
using Model.Material;
using Model.Problem;
using Model.TableModel;
using Model.ViewModel.Department;
using Model.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        #region 后台设置

        public ActionResult AdSetIndex()
        {
            return View();
        }

        //TODO:CPF
        public ActionResult AdSetDepartments()
        {
            //获取根部门
            var rootDepartment = HomeBusiness.GetRootDepartment();
            ViewBag.RootDepartment = rootDepartment;

            return View(rootDepartment);
        }

        public ActionResult AdSetUsers()
        {
            //if (TempData["errorMsg"] != null)
            //{
            //    var importMsg = TempData["errorMsg"].ToString();
            //    if (!string.IsNullOrEmpty(importMsg))
            //    {
            //        ViewBag.ImportMsg = importMsg;
            //    }
            //}
            ViewBag.DefaultPass = ConfigurationManager.AppSettings["defaultPassword"].ToString();
            return View();
        }

        /// <summary>
        /// 描述：获取组织框架信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentManagerList()
        {
            var result = HomeBusiness.GetAllDepartmentList();
            return PartialView("DepartmentManagerList", result);
        }

        public ActionResult MyProfile()
        {
            return View(this.LoginUser);
        }

        public ActionResult GetAllDepartment()
        {
            var list = HomeBusiness.GetAllDepartmentName();
            return Json(new { results = JsonHelper.Serializer<List<OrganizationSearch>>(list) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 描述：新建部门
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <param name="level">level为1表示新建平级部门</param>
        /// <returns></returns>
        public ActionResult DepartmentCreate(string departmentId, int level)
        {
            if (!string.IsNullOrEmpty(departmentId))
            {
                int dpId = Convert.ToInt32(EncryptHelper.DesDecrypt(departmentId));
                DepartmentInfo model;
                if (level == 1)   //1表示新建平级
                {
                    model = HomeBusiness.GetDepartById(dpId);
                }
                else
                {
                    model = HomeBusiness.GetDepartById(dpId);
                    model.ParentId = model.Id;
                    model.ParentName = model.Name;
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult DepartmentSave(DepartmentSaveModel model)
        {
            if (!string.IsNullOrEmpty(model.ParentId))
            {
                model.ParentId = EncryptHelper.DesDecrypt(model.ParentId);
            }
            else
            {
                model.ParentId = "0";
            }

            ResultInfoModel result;
            if (!string.IsNullOrEmpty(model.DepartId))   //编辑·
            {
                model.DepartId = EncryptHelper.DesDecrypt(model.DepartId);
                result = HomeBusiness.SaveDepartment(model, this.LoginUser);//todo 
            }
            else //新增
            {
                result = HomeBusiness.SaveNewDepartment(model, this.LoginUser);
            }
            return Json(result);
        }

        public ActionResult DepartmentEdit(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int dpId = Convert.ToInt32(EncryptHelper.DesDecrypt(id));
                var model = HomeBusiness.GetDepartById(dpId);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return View(new DepartmentInfo());
        }

        public ActionResult UpdateDepartmentValid(string Id)
        {
            var result = new ResultInfoModel();
            if (!string.IsNullOrEmpty(Id))
            {
                result = HomeBusiness.UpdateDepartmentValid(EncryptHelper.DesDecrypt(Id), this.LoginUser);
            }
            return Json(result);
        }

        public ActionResult AdUser(string userId = "")
        {
            var dpList = new List<SelectListItem>();
            var defaultItem = new SelectListItem { Text = "请选择", Value = EncryptHelper.DesEncrypt("0") };
            dpList.Add(defaultItem);
            var list = HomeBusiness.GetAllDepartmentName();
            if (list != null & list.Count > 0)
            {
                foreach (var dp in list)
                {
                    var item = new SelectListItem();
                    item.Text = dp.text;
                    item.Value = dp.id;
                    dpList.Add(item);
                }
            }
            ViewBag.dpList = dpList;
            ViewBag.userId = userId;
            return View();
        }

        /// <summary>
        /// 描述：初始化用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult InitAdUser(string userId)
        {
            long userIdlong = long.Parse(EncryptHelper.DesDecrypt(userId));
            var userInfo = HomeBusiness.GetUserAndAccountById(userIdlong);
            var userView = new UserView();
            if (userInfo != null)
            {
                userView.UserId = EncryptHelper.DesEncrypt(userInfo.Id.ToString());
                userView.BUName = userInfo.BUName;
                userView.BUJobNumber = userInfo.BUJobNumber;
                userView.BUSex = userInfo.BUSex ?? 0;
                userView.BUAvatars = userInfo.BUAvatars;
                userView.AvatarsUrl = userInfo.AvatarsUrl;
                userView.BUPhoneNum = userInfo.BUPhoneNum;
                userView.BUEmail = userInfo.BUEmail;
                userView.DepartId = EncryptHelper.DesEncrypt(userInfo.BUDepartId.ToString());
                userView.BUTitle = userInfo.BUTitle;
                userView.BUIsValid = userInfo.BUIsValid;
                userView.BUDepartName = userInfo.BUDepartName;
                userView.BUEnglishName = userInfo.BUEnglishName;
                userView.BUPosition = userInfo.BUPosition;
                userView.BUExtensionPhone = userInfo.BUExtensionPhone;
                userView.BUMobilePhone = userInfo.BUMobilePhone;
                userView.Account = userInfo.Account;
                userView.IsExistAccount = string.IsNullOrEmpty(userView.Account) ? false : true;

            }
            return Json(userView, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UsrSave(UserView model)
        {
            var result = new ResultInfoModel
            {
                IsSuccess = true
            };
            try
            {
                if (string.IsNullOrEmpty(model.UserId) || model.UserId.Equals("0"))
                {
                    //新增
                    var user = new UserModel();
                    user.BUName = model.BUName;
                    user.BUSex = model.BUSex;
                    if (!string.IsNullOrWhiteSpace(model.BUAvatars))
                    {
                        var path = model.BUAvatars.Substring(ServerInfo.RootURI.Length);
                        user.BUAvatars = path;
                    }
                    else
                    {
                        user.BUAvatars = string.Empty;
                    }
                    user.BUPhoneNum = model.BUPhoneNum;
                    user.BUEmail = model.BUEmail;
                    //user.BUDepartId = int.Parse(EncryptHelper.DesDecrypt(model.DepartId));
                    user.BUTitle = model.BUTitle;
                    user.BUIsValid = model.BUIsValid;
                    user.BUCreateUserNo = LoginUser.JobNum;
                    user.BUCreateUserName = LoginUser.UserName;
                    user.BUCreateTime = DateTime.Now;
                    user.BUOperateUserNo = LoginUser.JobNum; ;
                    user.BUOperateUserName = LoginUser.UserName;
                    user.BUOperateTime = DateTime.Now;

                    user.BUDepartName = model.BUDepartName;
                    user.BUExtensionPhone = model.BUExtensionPhone;
                    user.BUEnglishName = model.BUEnglishName;
                    user.BUPosition = model.BUPosition;
                    user.BUMobilePhone = model.BUMobilePhone;
                    user.BUJobNumber = HomeBusiness.GenerateNewJobNumber(string.Empty);
                    user.Account = model.Account;
                    result = HomeBusiness.SaveNewUser(user);
                }
                else
                {
                    //解密id
                    model.UserId = EncryptHelper.DesDecrypt(model.UserId);
                    var user = HomeBusiness.GetUserById(long.Parse(model.UserId));
                    user.BUName = model.BUName;
                    user.BUSex = model.BUSex;
                    user.BUAvatars = model.BUAvatars;
                    user.BUPhoneNum = model.BUPhoneNum;
                    user.BUEmail = model.BUEmail;
                    //user.BUDepartId = int.Parse(EncryptHelper.DesDecrypt(model.DepartId));
                    user.BUTitle = model.BUTitle;
                    user.BUIsValid = model.BUIsValid;
                    user.BUOperateUserNo = LoginUser.JobNum;
                    user.BUOperateUserName = LoginUser.UserName;
                    //user.BUOperateUserNo = "33029";
                    //user.BUOperateUserName = "33029";
                    user.BUOperateTime = DateTime.Now;
                    user.BUDepartName = model.BUDepartName;
                    user.BUExtensionPhone = model.BUExtensionPhone;
                    user.BUEnglishName = model.BUEnglishName;
                    user.BUPosition = model.BUPosition;
                    user.BUMobilePhone = model.BUMobilePhone;
                    user.Account = model.Account;
                    var isExistAccount = model.IsExistAccount;
                    result = HomeBusiness.SaveUser(user, isExistAccount);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return Json(result);
        }

        public ActionResult UserSearch(UserSearchViewModel search)
        {
            var total = 0;
            var result = HomeBusiness.SearchUser(search, out total);

            var page = new Page(total, search.CurrentPage);

            var resultModel = new UserSearchRsultModel
            {
                Models = result,
                Page = page
            };
            return View(resultModel);
        }


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult UserAutoComplete(string key)
        {
            var userlist = HomeBusiness.UserAutoComplete(key);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteUser(string UserId)
        {
            var userId = Convert.ToInt32(EncryptHelper.DesDecrypt(UserId));
            var result = HomeBusiness.DeleteUser(userId, this.LoginUser);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserByJobNumber(string jobNumber)
        {
            var userlist = HomeBusiness.GetUserByJobNumber(jobNumber);
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult StreamFileFromDisk()
        {
            string path = Request.MapPath(Request.ApplicationPath + "/ImportTemplate/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "UserUploadTemp.xlsx";
            return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "UserUploadTemp" + timestramp + ".xlsx");
        }
        #region 用户导入
        [HttpPost]
        public ActionResult UpLoadFile()
        {
            var result = new ResultInfoModel()
            {
                IsSuccess = false
            };
            StringBuilder strbuild = new StringBuilder();
            string FileName;
            string savePath;
            HttpPostedFileBase file = Request.Files["file"];

            if (file == null || file.ContentLength <= 0)
            {
                result.Message = "please choose file";
                return Content(JsonHelper.JsonSerializer(result));
            }
            else
            {
                string fileName = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = Path.GetExtension(fileName);//获取上传文件的扩展名
                string NoFileName = Path.GetFileNameWithoutExtension(fileName);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    result.Message = "please upload .xls and .xlsx";
                    return Content(JsonHelper.JsonSerializer(result));
                }
                if (filesize >= Maxsize)
                {
                    result.Message = string.Format("file size can't big than {0}", Maxsize);
                    return Content(JsonHelper.JsonSerializer(result));
                }
                string path = Server.MapPath("~/App_Data/uploads");
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savePath + ";Extended Properties=Excel 12.0;";
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
                DataSet myDataSet = new DataSet();
                try
                {
                    myCommand.Fill(myDataSet, "ExcelInfo");
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    return Content(JsonHelper.JsonSerializer(result));
                }
                DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();

                try
                {
                    var maxJobNumber = string.Empty;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var model = new UserModel();
                        model.BUName = table.Rows[i]["ChineseName"].ToString() ?? "";
                        model.BUSex = 0;
                        model.BUAvatars = string.Empty;
                        model.BUPhoneNum = table.Rows[i]["PhoneNum"].ToString() ?? "";
                        model.BUEmail = table.Rows[i]["Email"].ToString() ?? "";
                        model.BUDepartId = 0;
                        model.BUTitle = string.Empty;
                        model.BUIsValid = EnabledEnum.Enabled.GetHashCode();
                        model.BUCreateUserNo = LoginUser.JobNum;
                        model.BUCreateUserName = LoginUser.UserName;
                        model.BUCreateTime = DateTime.Now;
                        model.BUOperateUserNo = LoginUser.JobNum;
                        model.BUOperateUserName = LoginUser.UserName;
                        model.BUOperateTime = DateTime.Now;
                        model.BUDepartName = table.Rows[i]["DepartName"].ToString() ?? "";
                        model.BUExtensionPhone = table.Rows[i]["ExtensionPhone"].ToString() ?? "";
                        model.BUEnglishName = table.Rows[i]["EnglishName"].ToString() ?? "";
                        model.BUPosition = table.Rows[i]["Position"].ToString() ?? "";
                        model.BUMobilePhone = table.Rows[i]["MobilePhone"].ToString() ?? "";
                        model.Account = table.Rows[i]["Account"].ToString() ?? "";
                        maxJobNumber = HomeBusiness.GenerateNewJobNumber(maxJobNumber);
                        model.BUJobNumber = maxJobNumber;


                        var inserResult = HomeBusiness.SaveNewUser(model);
                    }
                    result.IsSuccess = true;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    return Content(JsonHelper.JsonSerializer(result));
                }
                conn.Close();
            }
            return Content(JsonHelper.JsonSerializer(result));
        }
        #endregion
        public ActionResult UserImport()
        {
            return View();
        }

        #endregion

        #region 账号设置
        public ActionResult Logout()
        {
            string key = CommonHelper.Md5(CookieKey.COOKIE_KEY_USERINFO);
            CookieHelper.ClearCookie(key);
            return RedirectToAction("LoginPage", "Login");
        }
        public ActionResult Resort(string accont)
        {
            ViewBag.BasePath = BasePath;
            ViewBag.CurrentCulture = CultureHelper.GetCurrentCulture();
            ViewBag.Account = accont;
            return View();
        }

        [HttpPost]
        public ActionResult Reset(string account, string password)
        {
            var result = new ResultInfoModel { IsSuccess = false };
            try
            {
                var accontInfo = HomeBusiness.RestAccount(account, password, this.LoginUser);

                var login = HomeBusiness.Login(account, password);

                //写入cookie
                string key = CommonHelper.Md5(CookieKey.COOKIE_KEY_USERINFO);
                string data = JsonHelper.Serializer<UserLoginInfo>(login);
                CookieHelper.SetCookie(
                    key,
                    CommonHelper.DesEncrypt(data, CookieKey.COOKIE_KEY_ENCRYPT),
                    DateTime.Now.AddDays(1).Date,
                    ServerInfo.GetTopDomain);

                result.IsSuccess = accontInfo.IsSuccess;
                result.Message = accontInfo.Message;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        /// <summary>
        /// 描述：重置用户密码
        /// </summary>
        /// <param name="userid">用户Id</param>
        /// <returns></returns>
        public ActionResult RestPassByUserid(string userId)
        {
            var result = new ResultInfoModel { IsSuccess = false };
            try
            {
                result = HomeBusiness.RestAccountByUserid(userId, LoginUser);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 导出
        public FileResult UserExcel(UserSearchViewModel searchModel)
        {
            searchModel.PageSize = 1000;
            var totalCount = 0;
            var result = HomeBusiness.SearchUser(searchModel, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("DepartName");
            row1.CreateCell(1).SetCellValue("Position");
            row1.CreateCell(2).SetCellValue("EnglishName");
            row1.CreateCell(3).SetCellValue("ChineseName");
            row1.CreateCell(4).SetCellValue("Account");
            row1.CreateCell(5).SetCellValue("PhoneNum");
            row1.CreateCell(6).SetCellValue("ExtensionPhone");
            row1.CreateCell(7).SetCellValue("MobilePhone");
            row1.CreateCell(8).SetCellValue("Email");

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].BUDepartName);
                rowtemp.CreateCell(1).SetCellValue(result[i].BUPosition);
                rowtemp.CreateCell(2).SetCellValue(result[i].BUEnglishName);
                rowtemp.CreateCell(3).SetCellValue(result[i].BUName);
                rowtemp.CreateCell(4).SetCellValue(result[i].Account);
                rowtemp.CreateCell(5).SetCellValue(result[i].BUPhoneNum);
                rowtemp.CreateCell(6).SetCellValue(result[i].BUExtensionPhone);
                rowtemp.CreateCell(7).SetCellValue(result[i].BUMobilePhone);
                rowtemp.CreateCell(8).SetCellValue(result[i].BUEmail);

            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "UserInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName);
        }
        #endregion

        #region 验证 
        #endregion

        #region 页面初始的查询
        [HttpPost]
        public ActionResult HomeSearchResult(ProblemSearchModel searchModel = null)
        {
            var totalCount = 0;
            var result = ProblemBusiness.ProblemSearchResult(searchModel, out totalCount);
            var page = new Page(totalCount, searchModel.CurrentPage);

            var model = new ProblemSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(model);
        }

        #endregion

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult ImgUpload()
        {
            var result = new ImgUploadResult()
            {
                IsSuccess = false
            };
            var imgs = new List<ImgData>();
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                imgs.Add(UploadHelper.ImageUpload(file, Server.MapPath(@"\")));
            }
            result.data = imgs;
            if (imgs.Count > 0)
            {
                result.IsSuccess = true;
            }
            return Content(JsonHelper.JsonSerializer(result));
        }

        public ActionResult AttachmentUpload()
        {
            var result = new FileUploadResult()
            {
                IsSuccess = false
            };
            var files = new List<FileData>();
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item];
                string fileName = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = Path.GetExtension(fileName);//获取上传文件的扩展名
                string NoFileName = Path.GetFileNameWithoutExtension(fileName);//获取无扩展名的文件名
                int maxByte = 10;
                long Maxsize = maxByte * 1024 * 1024;//定义上传文件的最大空间大小为10M
                string FileType = ".exe,.bat";//定义上传文件的类型字符串

                if (FileType.Contains(fileEx))
                {
                    result.Message = "can't upload .exe and .bat";
                    return Content(JsonHelper.JsonSerializer(result));
                }
                if (filesize >= Maxsize)
                {
                    result.Message = string.Format("file size can't big than {0} MB", maxByte);
                    return Content(JsonHelper.JsonSerializer(result));
                }
                files.Add(UploadHelper.FileUpload(file, Server.MapPath(@"\")));
            }
            result.data = files;
            if (files.Count > 0)
            {
                result.IsSuccess = true;
            }
            return Content(JsonHelper.JsonSerializer(result));
        }


        public ActionResult TestEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendTestMail(EmailSendModel send)
        {
            send.MailTitle = "test";
            send.MailContent = "this email is from QRQC test system!";
            var result = EmailHelper.Send(send);
            return Json(result);
        }
    }
}
