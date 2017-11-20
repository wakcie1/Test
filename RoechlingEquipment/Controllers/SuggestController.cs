using Business;
using Common;
using Common.Enum;
using Model.CommonModel;
using Model.Suggest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    [ServerAuthorize]
    public class SuggestController : BaseController
    {
        public ActionResult SuggestIndex()
        {
            return View();
        }

        public ActionResult SuggestEdit(string id = "")
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public ActionResult SuggestSearchResult(SuggestionsSearchModel searchModel)
        {
            var totalCount = 0;
            var result = SuggestBusiness.SearchSuggestInfoList(searchModel, out totalCount);
            var page = new Page(totalCount, searchModel.CurrentPage);

            var model = new SuggestionsSearchResultModel
            {
                SearchResultModel = result.Skip((page.CurrentPage - 1) * page.PageSize).Take(page.PageSize),
                Page = page
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveSuggest(SuggestionsInfoModel model)
        {
            var result = SuggestBusiness.SaveSuggest(model, this.LoginUser);
            return Json(result);
        }

        public ActionResult InitEditSuggestPage(string userId)
        {
            var userInfo = SuggestBusiness.GetSuggesById(Convert.ToInt64(userId));
            return Json(userInfo, JsonRequestBehavior.AllowGet);
        }

        #region 控件数据源
        /// <summary>
        /// ToolList
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRequireTypeList(bool isNeedDefalut)
        {
            var list = CommonBusiness.GetRequireTypeList();
            var RequireType = list.Select(i => new SelectListItem
            {
                Text = i.BCCodeDesc,
                Value = i.BCCode
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "All", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (RequireType.Count() > 0)
            {
                result.AddRange(RequireType);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Phrase
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPhraseList(bool isNeedDefalut)
        {
            var list = CommonBusiness.GetPhraseList();
            var phraseType = list.Select(i => new SelectListItem
            {
                Text = i.BCCodeDesc,
                Value = i.BCCode
            });
            var result = new List<SelectListItem>();
            if (isNeedDefalut)
            {
                var value1 = new SelectListItem() { Text = "All", Value = "-1", Selected = true };
                result.Add(value1);
            }
            if (phraseType.Count() > 0)
            {
                result.AddRange(phraseType);
            }

            return Json(result, JsonRequestBehavior.AllowGet);

            //var phraseList = new List<SelectListItem>();
            ////TODO
            //var value1 = new SelectListItem() { Text = "Assembling", Value = "Assembling" };
            //phraseList.Add(value1);
            //var value2 = new SelectListItem() { Text = "Molding", Value = "Molding" };
            //phraseList.Add(value2);
            //var value3 = new SelectListItem() { Text = "Press Molding", Value = "Press Molding" };
            //phraseList.Add(value3);
            //return Json(phraseList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// AssignTo
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAssignToList()
        {
            var assignToList = new List<SelectListItem>();
            //TODO
            var value1 = new SelectListItem() { Text = "Assembling", Value = "Assembling" };
            assignToList.Add(value1);
            var value2 = new SelectListItem() { Text = "Molding", Value = "Molding" };
            assignToList.Add(value2);
            var value3 = new SelectListItem() { Text = "Press Molding", Value = "Press Molding" };
            assignToList.Add(value3);
            return Json(assignToList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSuggestStatusList(bool isNeedDefalut)
        {
            var sourcelist = EnumHelper.SelectListEnum<SuggestionsStatusEnum>(null, isNeedDefalut);

            return Json(sourcelist, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }

}
