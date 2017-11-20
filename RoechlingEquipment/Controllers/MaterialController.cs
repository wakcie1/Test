
using Business;
using Common;
using Common.Enum;
using Model.CommonModel;
using Model.Material;
using Model.TableModel;
using System;
using System.Collections.Generic;
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
    public class MaterialController : BaseController
    {
        #region Setting页面

        public ActionResult MaterialIndex()
        {
            return View();
        }

        public ActionResult MachineToolIndex()
        {
            return View();
        }

        #endregion

        #region 增删改查
        /// <summary>
        /// 新增物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaterialOperate(MaterialInfoModel model)
        {
            var result = MaterialBusiness.SaveMaterial(model, this.LoginUser);
            return Json(result);
        }


        /// <summary>
        /// 获取物料
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public ActionResult GetOneMaterial(int materialId)
        {
            var result = MaterialBusiness.GetMaterialById(materialId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult MaterialSearch(MaterialSearchModel model)
        {

            //return  IEnumerable<MaterialInfoModel>
            var totalCount = 0;
            var result = MaterialBusiness.SearchMaterialPageList(model, out totalCount);
            var page = new Page(totalCount, model.CurrentPage);

            //var resultModel = new MaterialSearchResultModel
            //{
            //    Models = result.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize),
            //    Page = page
            //};
            var resultModel = new MaterialSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(resultModel);
        }


        /// <summary>
        /// 查询设备和工具
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult MachineToolSearch(MaToolSearchModel model)
        {
            var totalCount = 0;
            var result = MaterialBusiness.SearchMaToolPageList(model, out totalCount);
            var page = new Page(totalCount, model.CurrentPage);

            var resultModel = new MaToolSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View("MachineSearch", resultModel);
        }

        /// <summary>
        /// 新增物料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaToolOperate(MaToolModel model)
        {
            var result = MaterialBusiness.SaveMaTool(model, this.LoginUser);
            return Json(result);
        }

        /// <summary>
        /// 获取设备工具
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public ActionResult GetOneMaTool(int matoolId)
        {
            var result = MaterialBusiness.GetMaToolById(matoolId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMaterial(int Id)
        {
            var result = MaterialBusiness.DeleteMaterial(Id, this.LoginUser);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMachine(int Id)
        {
            var result = MaterialBusiness.DeleteMachine(Id, this.LoginUser);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region

        public ActionResult UpLoadFile()
        {
            return View();
        }

        public JsonResult UploadImage(HttpPostedFileBase imgFile)
        {
            object result = new object();
            if (result == null)
            {
                try
                {
                    if (imgFile != null)
                    { //新上传了图片，需要保存处理  
                        string fImageName = string.Empty;
                        object fResult = UDownHelperController.SaveImage(imgFile, ref fImageName, false);
                        if (fResult == null)
                        {//上传成功  
                            return Json(new { error = 0, url = "/Upload/Images/" + fImageName }, "text/html");
                        }
                        else
                        {//上传失败  
                            return Json(new { error = 1, message = "上传失败！" }, "text/html");
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = new { error = 1, message = ex.ToString() };
                }
            }
            else
            {
                result = new { error = 1, message = "没有文件！" };
            }
            return Json(result, "text/html");
        }
        #endregion

        #region 导入

        public FileStreamResult SAPFile()
        {
            string path = Request.MapPath(Request.ApplicationPath + "/ImportTemplate/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "MaterialUploadTemp.xls";
            return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "MaterialUploadTemp" + timestramp + ".xls");
        }

        public FileStreamResult MachineFile(int classification)
        {
            string path = Request.MapPath(Request.ApplicationPath + "/ImportTemplate/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = string.Empty;
            if (classification != 4)
            {
                fileName = "Machine" + classification.ToString() + "UploadTemp.xls";
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "Machine" + classification.ToString() + "UploadTemp" + timestramp + ".xls");
            }
            else
            {
                fileName= "ToolUploadTemp.xls";
                return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "ToolUploadTemp" + timestramp + ".xls");
            }
            
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult UploadMaterialFiles()
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
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            MaterialInfoModel model = new MaterialInfoModel();
                            model.MIProcessType = table.Rows[i][0].ToString();
                            model.MICustomer = table.Rows[i][1].ToString();
                            model.MISapPN = table.Rows[i][2].ToString();
                             
                            model.MIProductName = table.Rows[i][3].ToString();
                            model.MIInjectionMC = table.Rows[i][4].ToString();
                            model.MICustomerPN = table.Rows[i][5].ToString();
                          
                            model.MICavity = DataConvertHelper.ToInt(table.Rows[i][6].ToString(), 0);
                            model.MICycletime = DataConvertHelper.ToDecimal(table.Rows[i][7].ToString(), 0);
                            model.MICycletimeCav = DataConvertHelper.ToDecimal(table.Rows[i][8].ToString(), 0);
                            model.MIStandardHeadcount = DataConvertHelper.ToInt(table.Rows[i][9].ToString(), 0);
                            model.MTStandardScrap= table.Rows[i][10].ToString() ;
                            model.MIMaterialPN = table.Rows[i][11].ToString();
                            model.MICavityG = DataConvertHelper.ToDecimal(table.Rows[i][12].ToString(), 0);
                            model.MIMoldNo = table.Rows[i][13].ToString();
                            model.MIAssAC = table.Rows[i][14].ToString();
                            model.MIWorkOrder = table.Rows[i][15].ToString();
                            MaterialBusiness.SaveMaterial(model, this.LoginUser);
                            var insertResult = MaterialBusiness.SaveMaterial(model, LoginUser);
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
        }


        public ActionResult ImportMachine(int type)
        {
            var result = new ImportUploadResult()
            {
                IsSuccess = false
            };
            var invalidlist = new List<InvalidData>();
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
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var datevalid1 = false;
                        var datevalid2 = false;
                        var invalid = new InvalidData();
                        if (!string.IsNullOrEmpty(table.Rows[i][6].ToString()))
                        {
                            datevalid1 = DataConvertHelper.IsDateTime(table.Rows[i][6].ToString());
                            if (!datevalid1)
                            {
                                invalid.Key = table.Rows[i][2].ToString();
                                invalid.Value1 = table.Rows[i][6].ToString();
                            }
                        }
                        if (!string.IsNullOrEmpty(table.Rows[i][12].ToString()))
                        {
                            datevalid2 = DataConvertHelper.IsDateTime(table.Rows[i][12].ToString());
                            if (!datevalid2)
                            {
                                invalid.Key = table.Rows[i][2].ToString();
                                invalid.Value2 = table.Rows[i][12].ToString();
                            }
                        }
                        if (!datevalid1 && !datevalid2)
                        {
                            invalidlist.Add(invalid);
                            continue;
                        }
                        var model = new MaToolModel();
                        model.BMClassification = type;
                        model.BMEquipmentName = table.Rows[i][0].ToString();
                        model.BMEquipmentNo = table.Rows[i][1].ToString();
                        model.BMFixtureNo = table.Rows[i][2].ToString();
                        model.BMType = table.Rows[i][3].ToString();
                        model.BMSerialNumber = table.Rows[i][4].ToString();
                        model.BMQuantity = DataConvertHelper.ToInt(table.Rows[i][5].ToString(), 0);
                        model.BMManufacturedDate = table.Rows[i][6].ToString();
                        model.BMPower = table.Rows[i][7].ToString();
                        model.BMOutlineDimension = table.Rows[i][8].ToString();
                        model.BMAbility = table.Rows[i][9].ToString();
                        model.BMNeedPressureAir = DataConvertHelper.GetYesOrNoValue(table.Rows[i][10].ToString());
                        model.BMNeedCoolingWater = DataConvertHelper.GetYesOrNoValue(table.Rows[i][11].ToString());
                        model.BMIncomingDate = table.Rows[i][12].ToString();
                        model.BMRemarks = table.Rows[i][13].ToString();
                        var inserResult = MaterialBusiness.SaveMaTool(model, LoginUser);

                        //if (type != 4)
                        //{
                        //    MachineTool(type, table, i);
                        //}
                        //else {
                        //    MaterialTool(type, table, i);
                        //}
                    }
                    result.invalidData = invalidlist;
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
         
        public FileStreamResult ToolFile()
        {
            string path = Request.MapPath(Request.ApplicationPath + "/App_Data/uploads/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "ToolUploadTemp.xlsx";
            return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "ToolUploadTemp" + timestramp + ".xlsx");
        }
        //To Delete
        //public ActionResult Importool()
        //{
        //    var result = new ResultInfoModel()
        //    {
        //        IsSuccess = false
        //    };
        //    StringBuilder strbuild = new StringBuilder();
        //    string FileName;
        //    string savePath;
        //    HttpPostedFileBase file = Request.Files["file"];

        //    if (file == null || file.ContentLength <= 0)
        //    {
        //        result.Message = "please choose file";
        //        return Content(JsonHelper.JsonSerializer(result));
        //    }
        //    else
        //    {
        //        string fileName = Path.GetFileName(file.FileName);
        //        int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
        //        string fileEx = Path.GetExtension(fileName);//获取上传文件的扩展名
        //        string NoFileName = Path.GetFileNameWithoutExtension(fileName);//获取无扩展名的文件名
        //        int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
        //        string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

        //        FileName = NoFileName + fileEx;
        //        if (!FileType.Contains(fileEx))
        //        {
        //            result.Message = "please upload .xls and .xlsx";
        //            return Content(JsonHelper.JsonSerializer(result));
        //        }
        //        if (filesize >= Maxsize)
        //        {
        //            result.Message = string.Format("file size can't big than {0}", Maxsize);
        //            return Content(JsonHelper.JsonSerializer(result));
        //        }
        //        string path = Server.MapPath("~/App_Data/uploads");
        //        savePath = Path.Combine(path, FileName);
        //        file.SaveAs(savePath);
        //    }

        //    string strConn;
        //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savePath + ";Extended Properties=Excel 12.0;";
        //    using (OleDbConnection conn = new OleDbConnection(strConn))
        //    {
        //        conn.Open();
        //        OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
        //        DataSet myDataSet = new DataSet();
        //        try
        //        {
        //            myCommand.Fill(myDataSet, "ExcelInfo");
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Message = ex.Message;
        //            return Content(JsonHelper.JsonSerializer(result));
        //        }
        //        DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();

        //        var importResult = new Importresult();
        //        importResult.FalseInfo = new List<FalseInfo>();
        //        try
        //        {
        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                var model = new MaToolModel
        //                {
        //                    //BMCode = table.Rows[i]["Code"].ToString() ?? "",
        //                    //BMCodeDesc = table.Rows[i]["CodeDesc"].ToString() ?? "",
        //                    //BMType = MTTypeEnum.Tool.GetHashCode(),
        //                    //BMIsValid = EnabledEnum.Enabled.GetHashCode(),
        //                };
        //                var inserResult = MaterialBusiness.SaveMaTool(model, LoginUser);
        //            }
        //            result.IsSuccess = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            result.Message = ex.Message;
        //            return Content(JsonHelper.JsonSerializer(result));
        //        }  
        //        conn.Close();
        //    }
        //    return Content(JsonHelper.JsonSerializer(result));
        //}
        #endregion

        #region 导出
        public FileResult MaterialExcel(MaterialSearchModel searchModel)
        {
            searchModel.PageSize = 1000;
            var totalCount = 0;
            var result = MaterialBusiness.SearchMaterialPageList(searchModel, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("Process Type");
            row1.CreateCell(1).SetCellValue("Customer");
            row1.CreateCell(2).SetCellValue("RASP P/N");
            row1.CreateCell(3).SetCellValue("Part Name");
            row1.CreateCell(4).SetCellValue("Production Unit");
            row1.CreateCell(5).SetCellValue("Customer P/N ");
            row1.CreateCell(6).SetCellValue("Cavity");
            row1.CreateCell(7).SetCellValue("Cycletime");
            row1.CreateCell(8).SetCellValue("cycletime/cav");
            row1.CreateCell(9).SetCellValue("Standard Headcount");
            row1.CreateCell(10).SetCellValue("Standard scrap");
            row1.CreateCell(11).SetCellValue("Materials P/N"); 
            row1.CreateCell(12).SetCellValue("g/cav(SAP)");
            row1.CreateCell(13).SetCellValue("Mold No.");
            row1.CreateCell(14).SetCellValue("ASS AC");
            row1.CreateCell(15).SetCellValue("Work Order");

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].MIProcessType); 
                rowtemp.CreateCell(1).SetCellValue(result[i].MICustomer); 
                rowtemp.CreateCell(2).SetCellValue(result[i].MISapPN); 
                rowtemp.CreateCell(3).SetCellValue(result[i].MIProductName);
                rowtemp.CreateCell(4).SetCellValue(result[i].MIInjectionMC);
                rowtemp.CreateCell(5).SetCellValue(result[i].MICustomerPN);
                rowtemp.CreateCell(6).SetCellValue(Convert.ToDouble(result[i].MICavity));
                rowtemp.CreateCell(7).SetCellValue(Convert.ToDouble(result[i].MICycletime));
                rowtemp.CreateCell(8).SetCellValue(Convert.ToDouble(result[i].MICycletimeCav));
                rowtemp.CreateCell(9).SetCellValue(Convert.ToDouble(result[i].MIStandardHeadcount));
                rowtemp.CreateCell(10).SetCellValue(result[i].MTStandardScrap);
                rowtemp.CreateCell(11).SetCellValue(result[i].MIMaterialPN); 
                rowtemp.CreateCell(12).SetCellValue(Convert.ToDouble(result[i].MICavityG));
                rowtemp.CreateCell(13).SetCellValue(result[i].MIMoldNo);
                rowtemp.CreateCell(14).SetCellValue(result[i].MIAssAC);
                rowtemp.CreateCell(15).SetCellValue(result[i].MIWorkOrder); 
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "MaterialInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName);
        }

        public ActionResult MachineToolExcel(MaToolSearchModel model)
        {
            model.PageSize = 1000;
            var totalCount = 0;
            var result = MaterialBusiness.SearchMaToolPageList(model, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("Equipment/Fixture Name");
            row1.CreateCell(1).SetCellValue("Equipment No");
            row1.CreateCell(2).SetCellValue("Fixture No");
            row1.CreateCell(3).SetCellValue("Type");
            row1.CreateCell(4).SetCellValue("Serial Number");
            row1.CreateCell(5).SetCellValue("Quantity");
            row1.CreateCell(6).SetCellValue("Manufactured Date");
            row1.CreateCell(7).SetCellValue("power");
            row1.CreateCell(8).SetCellValue("Outline Dimension");
            row1.CreateCell(9).SetCellValue("Ability");
            row1.CreateCell(10).SetCellValue("Pressure air");
            row1.CreateCell(11).SetCellValue("Cooling water");
            row1.CreateCell(12).SetCellValue("Incoming Date");
            row1.CreateCell(13).SetCellValue("Remark");

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].BMEquipmentName);
                rowtemp.CreateCell(1).SetCellValue(result[i].BMEquipmentNo);
                rowtemp.CreateCell(2).SetCellValue(result[i].BMFixtureNo);
                rowtemp.CreateCell(3).SetCellValue(result[i].BMType);
                rowtemp.CreateCell(4).SetCellValue(result[i].BMSerialNumber);
                rowtemp.CreateCell(5).SetCellValue(Convert.ToInt32(result[i].BMQuantity));
                rowtemp.CreateCell(6).SetCellValue(result[i].BMManufacturedDate);
                rowtemp.CreateCell(7).SetCellValue(result[i].BMPower);
                rowtemp.CreateCell(8).SetCellValue(result[i].BMOutlineDimension);
                rowtemp.CreateCell(9).SetCellValue(result[i].BMAbility);
                rowtemp.CreateCell(10).SetCellValue(Convert.ToDouble(result[i].BMNeedPressureAir));
                rowtemp.CreateCell(11).SetCellValue(Convert.ToDouble(result[i].BMNeedCoolingWater));
                rowtemp.CreateCell(12).SetCellValue(result[i].BMIncomingDate);
                rowtemp.CreateCell(12).SetCellValue(result[i].BMRemarks);
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "MaterialToolInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName); 
        }
        #endregion

        #region materialOthers/WorkOrder
        [HttpPost]
        public ActionResult MaterialOtherSearchResult(MaterialOtherSearchModel model)
        { 
            var totalCount = 0;
            var result = MaterialBusiness.MaterialOtherSearch(model, out totalCount);
            var page = new Page(totalCount, model.CurrentPage);
             
            var resultModel = new MaterialOtherSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(resultModel);
        }
        [HttpPost]
        public ActionResult WorkOrderSubmmit(WorkOrderInfo model)
        {
            var result = MaterialBusiness.SaveWorkOrder(model, this.LoginUser);
            return Json(result);
        }
        /// <summary>
        /// 根据Id获取workOrder
        /// 创建人：wq
        /// </summary>
        /// <param name="workOrderId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetWorkOrderById(int workOrderId)
        {
            var result = MaterialBusiness.GetWorkOrderById(workOrderId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteWorkOrder(int Id)
        {
            var result = MaterialBusiness.DeleteWorkOrder(Id, this.LoginUser);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region 导入
        public ActionResult ImportWorkOrder(int type)
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
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            WorkOrderInfo model = new WorkOrderInfo();
                            model.WIWorkOrder = table.Rows[i][0].ToString();
                            model.WISapPN = table.Rows[i][1].ToString();
                            model.WIProductName = table.Rows[i][2].ToString();
                            model.WIReceiptTime = table.Rows[i][3].ToString();
                            model.WIReceiptBy = table.Rows[i][4].ToString();
                            model.WICloseDateShift =  table.Rows[i][5].ToString() ;
                            model.WIOrderArchived =  table.Rows[i][6].ToString() ;
                            model.WIParameterRecord = table.Rows[i][7].ToString() ;
                            model.WIToolMaintenanceRecord =  table.Rows[i][8].ToString() ;
                            model.WIToolMachineCheck = table.Rows[i][9].ToString();
                            model.WIQuantityConfirm =  table.Rows[i][10].ToString();
                            model.WIArchivedBy = table.Rows[i][11].ToString();
                            model.WIWeeklyCheck = table.Rows[i][12].ToString();
                            model.WIRemarks = table.Rows[i][7].ToString() ;
                            model.WIGetBy =  table.Rows[i][8].ToString() ;
                            model.WIGetTime = table.Rows[i][9].ToString();
                            model.WIType = type;
                            MaterialBusiness.SaveWorkOrder(model, this.LoginUser); 
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
        }
        #endregion

        #region 导出
        public ActionResult WorkOrderExport(MaterialOtherSearchModel model)
        {
            model.PageSize = 1000;
            var totalCount = 0;
            var result = MaterialBusiness.MaterialOtherSearch(model, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("Order No");
            row1.CreateCell(1).SetCellValue("Sap No");
            row1.CreateCell(2).SetCellValue("Product ");
            row1.CreateCell(3).SetCellValue("Receipt Time");
            row1.CreateCell(4).SetCellValue("Receipt By");
            row1.CreateCell(5).SetCellValue("Close Date/Shift");
            row1.CreateCell(6).SetCellValue("Order Archived");
            row1.CreateCell(7).SetCellValue("Parameter Record");
            row1.CreateCell(8).SetCellValue("Maintenance Record");
            row1.CreateCell(9).SetCellValue("Tool Machine Setup Check List  ");
            row1.CreateCell(10).SetCellValue("Quantity Confirm");
            row1.CreateCell(11).SetCellValue("Archived By");
            row1.CreateCell(12).SetCellValue("Weekly Check");
            row1.CreateCell(13).SetCellValue("Remarks");
            row1.CreateCell(14).SetCellValue("GetBy");
            row1.CreateCell(15).SetCellValue("GetTime");

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].WIWorkOrder);
                rowtemp.CreateCell(1).SetCellValue(result[i].WISapPN);
                rowtemp.CreateCell(2).SetCellValue(result[i].WIProductName);
                rowtemp.CreateCell(3).SetCellValue(result[i].WIReceiptTime);
                rowtemp.CreateCell(4).SetCellValue(result[i].WIReceiptBy);
                rowtemp.CreateCell(5).SetCellValue(result[i].WICloseDateShift);
                rowtemp.CreateCell(6).SetCellValue(result[i].WIOrderArchived);
                rowtemp.CreateCell(7).SetCellValue(result[i].WIParameterRecord);
                rowtemp.CreateCell(8).SetCellValue(result[i].WIToolMaintenanceRecord);
                rowtemp.CreateCell(9).SetCellValue(result[i].WIToolMachineCheck);
                rowtemp.CreateCell(10).SetCellValue(result[i].WIQuantityConfirm);
                rowtemp.CreateCell(11).SetCellValue(result[i].WIArchivedBy);
                rowtemp.CreateCell(12).SetCellValue(result[i].WIWeeklyCheck);
                rowtemp.CreateCell(13).SetCellValue( result[i].WIRemarks);
                rowtemp.CreateCell(14).SetCellValue(result[i].WIGetBy);
                rowtemp.CreateCell(15).SetCellValue(result[i].WIGetTime);
            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "WorkOrderInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName);
        }
        #endregion

        public FileStreamResult WorkOrderTempFile(int type)
        {
            string path = Request.MapPath(Request.ApplicationPath + "/ImportTemplate/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "WorkOrder" + type.ToString() + "UploadTemp.xls";
            return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "WorkOrder" + type.ToString() + "UploadTemp" + timestramp + ".xls");
        }
        #endregion

        #region MachineTool
        /// <summary>
        /// 导入
        /// 创建人：wq
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult ImportMaterialTool(int type)
        {
            var result = new ImportUploadResult()
            {
                IsSuccess = false
            };
            var invalidlist = new List<InvalidData>();
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
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        var model = new MaterialToolModel();
                        model.MTSapPN = table.Rows[i][0].ToString();
                        model.MTSapQuantity = table.Rows[i][1].ToString();
                        model.MTSapLibrary = table.Rows[i][2].ToString();
                        model.MTToolNo = table.Rows[i][3].ToString();
                        model.MTProductName = table.Rows[i][4].ToString();
                        model.MTStatus = table.Rows[i][5].ToString();
                        model.MTToolLibrary = table.Rows[i][6].ToString();
                        model.MTOutlineDimension = table.Rows[i][7].ToString();
                        model.MTQuality = table.Rows[i][8].ToString();
                        model.MTCustomerPN = table.Rows[i][9].ToString();
                        model.MTSapProductName = table.Rows[i][10].ToString();
                        model.MTBelong = table.Rows[i][11].ToString();
                        model.MTCustomerNo = table.Rows[i][12].ToString();
                        model.MTToolSupplier = table.Rows[i][13].ToString();
                        model.MTToolSupplierNo = table.Rows[i][14].ToString();
                        model.MTProductDate = table.Rows[i][15].ToString();
                        model.MTCavity = table.Rows[i][16].ToString();

                        var inserResult = MaterialBusiness.SaveMaterialTool(model, LoginUser);

                    }
                    result.invalidData = invalidlist;
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
        /// <summary>
        /// 查询MaterialTool
        /// 创建人：wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult MaterialToolSearchResult(MaterialToolSearchModel model)
        {
            //return  IEnumerable<MaterialInfoModel>
            var totalCount = 0;
            var result = MaterialBusiness.MaterialToolSearch(model, out totalCount);
            var page = new Page(totalCount, model.CurrentPage);

            //var resultModel = new MaterialSearchResultModel
            //{
            //    Models = result.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize),
            //    Page = page
            //};
            var resultModel = new MaterialToolSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(resultModel);
        }
        /// <summary>
        /// 新增和编辑
        /// 创建人；wq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MaterialToolOperate(MaterialToolModel model)
        {
            var result = MaterialBusiness.SaveMaterialTool(model, this.LoginUser);
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetOneMachineTool(int materialToolId)
        {
            var result = MaterialBusiness.GetMaterialToolById(materialToolId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMaterialTool(int Id)
        {
            var result = MaterialBusiness.DeleteMaterialTool(Id, this.LoginUser);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
