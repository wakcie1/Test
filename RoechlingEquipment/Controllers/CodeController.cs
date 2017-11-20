using Business;
using Common;
using Common.Costant;
using Model.Code;
using Model.CommonModel;
using Model.TableModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class CodeController : BaseController
    {
        //
        // GET: /Code/

        public ActionResult CodeIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CodeSearchResult(CodeSearchModel searchModel)
        {
            var totalCount = 0;
            var result = CodeBusiness.SearchResult(searchModel, out totalCount);
            var page = new Page(totalCount, searchModel.CurrentPage);

            var model = new CodeSearchResultModel
            {
                Models = result.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize),
                Page = page
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult DefectCodeSearchResult(DefectCodeSearchModel searchModel)
        {
            var totalCount = 0;
            var result = CodeBusiness.DefectCodeSearchResult(searchModel, out totalCount);
            var page = new Page(totalCount, searchModel.CurrentPage);

            var model = new DefectCodeSearchResultModel
            {
                Models = result,
                Page = page
            };
            return View(model);
        }

        public ActionResult GetCategoryList(bool isNeedDefalut)
        {
            var list = CodeBusiness.GetCategoryList();
            var gategory = list.Select(i => new SelectListItem
            {
                Text = i,
                Value = i
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "All", Value = "All", Selected = true };
                result.Add(value1);
            }
            if (gategory.Count() > 0)
            {
                result.AddRange(gategory);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCode(CodeModel model)
        {
            var result = CodeBusiness.SaveCode(model, this.LoginUser);
            return Json(result);
        }

        [HttpPost]
        public ActionResult SaveDefectCode(CodeDefectModel model)
        {
            var result = CodeBusiness.SaveDefectCode(model, this.LoginUser);
            return Json(result);
        }

        public ActionResult CodeDelete()
        {

            return View();
        }
        public ActionResult InitCodeDeletePage(string key)
        {
            var result = CodeBusiness.InitCodeDeletePage(key);
            return Json(result);
        }

        public ActionResult DeleteCode(int Id)
        {
            var result = CodeBusiness.DeleteCode(Id, this.LoginUser);
            return Json(new { IsSuccess = result });
        }

        public ActionResult GetOneDefectCode(int defectId)
        {
            var result = CodeBusiness.GetOneDefectCode(defectId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadDefectCodeFiles()
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
                int Maxsize = 5000 * 1024;//定义上传文件的最大空间大小为4M
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

                    var importResult = new Importresult();
                    importResult.FalseInfo = new List<FalseInfo>();

                    try
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            CodeDefectModel model = new CodeDefectModel();
                            model.BDCodeType = table.Rows[i][0].ToString();
                            model.BDCodeNo = DataConvertHelper.ToInt(table.Rows[i][1].ToString(), 0);
                            model.BDCode = table.Rows[i][2].ToString();
                            model.BDCodeNameEn = table.Rows[i][3].ToString();
                            model.BDCodeNameCn = table.Rows[i][4].ToString();
                            var inserResult = CodeBusiness.SaveDefectCode(model, this.LoginUser); 
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
            }
            return Content(JsonHelper.JsonSerializer(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileStreamResult DefectCodeFile()
        {
            string path = Request.MapPath(Request.ApplicationPath + "/ImportTemplate/");
            var timestramp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = "DefectCodeUploadTemp.xls";
            return File(new FileStream(path + fileName, FileMode.Open), "text/plain", "DefectCodeUploadTemp" + timestramp + ".xls");
        }

        #region 导出
        /// <summary>
        /// DefectCode 导出
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public FileResult DefectCodeExcel(DefectCodeSearchModel searchModel)
        {
            searchModel.PageSize = 1000;
            var totalCount = 0;
            var result = CodeBusiness.DefectCodeSearchResult(searchModel, out totalCount).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("Code Type");
            row1.CreateCell(1).SetCellValue("No");
            row1.CreateCell(2).SetCellValue("Code No");
            row1.CreateCell(3).SetCellValue("Code Name(English)");
            row1.CreateCell(4).SetCellValue("Code Name(Chinese)"); 

            for (int i = 0; i < result.Count(); i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue(result[i].BDCodeType);
                rowtemp.CreateCell(1).SetCellValue(result[i].BDCodeNo);
                rowtemp.CreateCell(2).SetCellValue(result[i].BDCode);
                rowtemp.CreateCell(3).SetCellValue(result[i].BDCodeNameEn);
                rowtemp.CreateCell(4).SetCellValue(result[i].BDCodeNameCn); 

            }

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            var exportFileName = string.Format("{0}{1}.xls", "DefectCodeInfo", DateTime.Now.ToString("yyyyMMddHHmmss"));
            return File(ms, "application/vnd.ms-excel", exportFileName);
        }
        #endregion
    }
}
