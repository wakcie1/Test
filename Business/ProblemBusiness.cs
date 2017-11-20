using Common;
using Common.Costant;
using Common.Enum;
using DataAccess;
using Model.CommonModel;
using Model.Home;
using Model.Problem;
using Model.ViewModel.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class ProblemBusiness
    {
        #region dal
        private static ProblemDAL _problemDal = new ProblemDAL();
        private static ProblemSolvingTeamDAL _solvingteamDal = new ProblemSolvingTeamDAL();
        private static ProblemQualityAlertDAL _qualityalertDal = new ProblemQualityAlertDAL();
        private static ProblemSortingActivityDAL _sortingactivityDal = new ProblemSortingActivityDAL();
        private static ProblemActionFactorAnalysisDAL _actionfactoranalysisDal = new ProblemActionFactorAnalysisDAL();
        private static ProblemActionContainmentDAL _actioncontainmentDal = new ProblemActionContainmentDAL();
        private static ProblemActionWhyanalysisDAL _actionwhyanalysisDal = new ProblemActionWhyanalysisDAL();
        private static PeoblemActionCorrectiveDAL _actioncorrectiveDal = new PeoblemActionCorrectiveDAL();
        private static ProblemActionPreventiveDAL _actionpreventiveDal = new ProblemActionPreventiveDAL();
        private static ProblemLayeredAuditDAL _layeredauditDal = new ProblemLayeredAuditDAL();
        private static ProblemVerificationDAL _verificationDal = new ProblemVerificationDAL();
        private static ProblemStandardizationDAL _standardizationDal = new ProblemStandardizationDAL();
        #endregion

        #region SaveProblem
        /// <summary>
        /// 描述:新增问题
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ProblemViewModel SaveNewProblem(ProblemInfoModel model, UserLoginInfo loginUser)
        {
            var result = new ProblemViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PIProblemNo = GenerateProblemNo();
                    model.PIIsValid = 1;
                    model.PICreateUserNo = loginUser.JobNum;
                    model.PICreateUserName = loginUser.UserName;
                    model.PICreateTime = DateTime.Now;
                    model.PIOperateUserNo = loginUser.JobNum;
                    model.PIOperateUserName = loginUser.UserName;
                    model.PIOperateTime = DateTime.Now;
                    model.Id = _problemDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PIProblemNo, "Create Problem", "ProblemInfo", string.Empty);
                }
                else
                {
                    //Update
                    model.PIOperateUserNo = loginUser.JobNum;
                    model.PIOperateUserName = loginUser.UserName;
                    model.PIOperateTime = DateTime.Now;
                    _problemDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PIProblemNo, "Edit Problem", "ProblemInfo", string.Empty);
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
        /// 描述:新增问题小组
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static SolvingTeamViewModel SaveSolvingTeam(ProblemSolvingTeamModel model, UserLoginInfo loginUser)
        {
            var result = new SolvingTeamViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PSIsValid = 1;
                    model.PSCreateUserNo = loginUser.JobNum;
                    model.PSCreateUserName = loginUser.UserName;
                    model.PSCreateTime = DateTime.Now;
                    model.PSOperateUserNo = loginUser.JobNum;
                    model.PSOperateUserName = loginUser.UserName;
                    model.PSOperateTime = DateTime.Now;
                    model.Id = _solvingteamDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSProblemNo, "Add SolvingTeam", "SolvingTeam", string.Empty);
                }
                else
                {
                    //Update
                    model.PSIsValid = 1;
                    model.PSOperateUserNo = loginUser.JobNum;
                    model.PSOperateUserName = loginUser.UserName;
                    model.PSOperateTime = DateTime.Now;
                    _solvingteamDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSProblemNo, "Edit SolvingTeam", "SolvingTeam", string.Empty);
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
        /// 描述:新增问题质量报警
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static QualityAlertViewModel SaveQualityAlert(ProblemQualityAlertModel model, UserLoginInfo loginUser)
        {
            var result = new QualityAlertViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PQIsValid = 1;
                    model.PQCreateUserNo = loginUser.JobNum;
                    model.PQCreateUserName = loginUser.UserName;
                    model.PQCreateTime = DateTime.Now;
                    model.PQOperateUserNo = loginUser.JobNum;
                    model.PQOperateUserName = loginUser.UserName;
                    model.PQOperateTime = DateTime.Now;
                    model.PQAttachmentDownloadUrl = string.IsNullOrEmpty(model.PQAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PQAttachmentUrl);
                    model.Id = _qualityalertDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PQProblemNo, "Add QualityAlert", "QualityAlert", string.Empty);
                }
                else
                {
                    //Update
                    model.PQIsValid = 1;
                    model.PQOperateUserNo = loginUser.JobNum;
                    model.PQOperateUserName = loginUser.UserName;
                    model.PQOperateTime = DateTime.Now;
                    model.PQAttachmentDownloadUrl = string.IsNullOrEmpty(model.PQAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PQAttachmentUrl);
                    _qualityalertDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PQProblemNo, "Edit QualityAlert", "QualityAlert", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static SortingActivityViewModel SaveSortingActivity(ProblemSortingActivityModel model, UserLoginInfo loginUser)
        {
            var result = new SortingActivityViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PSAIsValid = 1;
                    model.PSACreateUserNo = loginUser.JobNum;
                    model.PSACreateUserName = loginUser.UserName;
                    model.PSACreateTime = DateTime.Now;
                    model.PSAOperateUserNo = loginUser.JobNum;
                    model.PSAOperateUserName = loginUser.UserName;
                    model.PSAOperateTime = DateTime.Now;
                    model.Id = _sortingactivityDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSAProblemNo, "Add SortingActivity", "SortingActivity", string.Empty);
                }
                else
                {
                    //Update
                    model.PSAIsValid = 1;
                    model.PSAOperateUserNo = loginUser.JobNum;
                    model.PSAOperateUserName = loginUser.UserName;
                    model.PSAOperateTime = DateTime.Now;
                    _sortingactivityDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSAProblemNo, "Edit SortingActivity", "SortingActivity", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ActionFactorAnalysisViewModel SaveFactorAnalysis(ProblemActionFactorAnalysisModel model, UserLoginInfo loginUser)
        {
            var result = new ActionFactorAnalysisViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PAFIsValid = 1;
                    model.PAFCreateUserNo = loginUser.JobNum;
                    model.PAFCreateUserName = loginUser.UserName;
                    model.PAFCreateTime = DateTime.Now;
                    model.PAFOperateUserNo = loginUser.JobNum;
                    model.PAFOperateUserName = loginUser.UserName;
                    model.PAFOperateTime = DateTime.Now;
                    model.Id = _actionfactoranalysisDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAFProblemNo, "Add FactorAnalysis", "FactorAnalysis", string.Empty);
                }
                else
                {
                    //Update
                    model.PAFIsValid = 1;
                    model.PAFOperateUserNo = loginUser.JobNum;
                    model.PAFOperateUserName = loginUser.UserName;
                    model.PAFOperateTime = DateTime.Now;
                    _actionfactoranalysisDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAFProblemNo, "Edit FactorAnalysis", "FactorAnalysis", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ActionContainmentViewModel SaveActionContainment(ProblemActionContainmentModel model, UserLoginInfo loginUser)
        {
            var result = new ActionContainmentViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PACIsValid = 1;
                    model.PACCreateUserNo = loginUser.JobNum;
                    model.PACCreateUserName = loginUser.UserName;
                    model.PACCreateTime = DateTime.Now;
                    model.PACOperateUserNo = loginUser.JobNum;
                    model.PACOperateUserName = loginUser.UserName;
                    model.PACOperateTime = DateTime.Now;
                    model.PACAttachmentDownloadUrl = string.IsNullOrEmpty(model.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PACAttachmentUrl);
                    model.Id = _actioncontainmentDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PACProblemNo, "Add ActionContainment", "ActionContainment", string.Empty);
                }
                else
                {
                    //Update
                    model.PACIsValid = 1;
                    model.PACOperateUserNo = loginUser.JobNum;
                    model.PACOperateUserName = loginUser.UserName;
                    model.PACOperateTime = DateTime.Now;
                    model.PACAttachmentDownloadUrl = string.IsNullOrEmpty(model.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PACAttachmentUrl);
                    _actioncontainmentDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PACProblemNo, "Edit ActionContainment", "ActionContainment", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ActionWhyanalysisViewModel SaveActionWhyanalysis(ProblemActionWhyanalysisModel model, UserLoginInfo loginUser)
        {
            var result = new ActionWhyanalysisViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PAWIsValid = 1;
                    model.PAWCreateUserNo = loginUser.JobNum;
                    model.PAWCreateUserName = loginUser.UserName;
                    model.PAWCreateTime = DateTime.Now;
                    model.PAWOperateUserNo = loginUser.JobNum;
                    model.PAWOperateUserName = loginUser.UserName;
                    model.PAWOperateTime = DateTime.Now;
                    model.Id = _actionwhyanalysisDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAWProblemNo, "Add ActionWhyanalysis", "ActionWhyanalysis", string.Empty);
                }
                else
                {
                    //Update
                    model.PAWIsValid = 1;
                    model.PAWOperateUserNo = loginUser.JobNum;
                    model.PAWOperateUserName = loginUser.UserName;
                    model.PAWOperateTime = DateTime.Now;
                    _actionwhyanalysisDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAWProblemNo, "Edit ActionWhyanalysis", "ActionWhyanalysis", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ActionCorrectiveViewModel SaveActionCorrective(ProblemActionCorrectiveModel model, UserLoginInfo loginUser)
        {
            var result = new ActionCorrectiveViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PACIsValid = 1;
                    model.PACCreateUserNo = loginUser.JobNum;
                    model.PACCreateUserName = loginUser.UserName;
                    model.PACCreateTime = DateTime.Now;
                    model.PACOperateUserNo = loginUser.JobNum;
                    model.PACOperateUserName = loginUser.UserName;
                    model.PACOperateTime = DateTime.Now;
                    model.PACAttachmentDownloadUrl = string.IsNullOrEmpty(model.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PACAttachmentUrl);
                    model.Id = _actioncorrectiveDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PACProblemNo, "Add ActionCorrective", "ActionCorrective", string.Empty);
                }
                else
                {
                    //Update
                    model.PACIsValid = 1;
                    model.PACOperateUserNo = loginUser.JobNum;
                    model.PACOperateUserName = loginUser.UserName;
                    model.PACOperateTime = DateTime.Now;
                    model.PACAttachmentDownloadUrl = string.IsNullOrEmpty(model.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PACAttachmentUrl);
                    _actioncorrectiveDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PACProblemNo, "Edit ActionCorrective", "ActionCorrective", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static ActionPreventiveViewModel SaveActionPreventive(ProblemActionPreventiveModel model, UserLoginInfo loginUser)
        {
            var result = new ActionPreventiveViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PAPIsValid = 1;
                    model.PAPCreateUserNo = loginUser.JobNum;
                    model.PAPCreateUserName = loginUser.UserName;
                    model.PAPCreateTime = DateTime.Now;
                    model.PAPOperateUserNo = loginUser.JobNum;
                    model.PAPOperateUserName = loginUser.UserName;
                    model.PAPOperateTime = DateTime.Now;
                    model.PAPAttachmentDownloadUrl = string.IsNullOrEmpty(model.PAPAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PAPAttachmentUrl);
                    model.Id = _actionpreventiveDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAPProblemNo, "Add ActionPreventive", "ActionPreventive", string.Empty);
                }
                else
                {
                    //Update
                    model.PAPIsValid = 1;
                    model.PAPOperateUserNo = loginUser.JobNum;
                    model.PAPOperateUserName = loginUser.UserName;
                    model.PAPOperateTime = DateTime.Now;
                    model.PAPAttachmentDownloadUrl = string.IsNullOrEmpty(model.PAPAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PAPAttachmentUrl);
                    _actionpreventiveDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PAPProblemNo, "Edit ActionPreventive", "ActionPreventive", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static StandLayeredAuditViewModel SaveStandLayeredAudit(ProblemLayeredAuditModel model, UserLoginInfo loginUser)
        {
            var result = new StandLayeredAuditViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PLIsValid = 1;
                    model.PLCreateUserNo = loginUser.JobNum;
                    model.PLCreateUserName = loginUser.UserName;
                    model.PLCreateTime = DateTime.Now;
                    model.PLOperateUserNo = loginUser.JobNum;
                    model.PLOperateUserName = loginUser.UserName;
                    model.PLOperateTime = DateTime.Now;
                    model.PLAttachmentDownloadUrl = string.IsNullOrEmpty(model.PLAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PLAttachmentUrl);
                    model.Id = _layeredauditDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PLProblemNo, "Add LayeredAudit", "LayeredAudit", string.Empty);
                }
                else
                {
                    //Update
                    model.PLIsValid = 1;
                    model.PLOperateUserNo = loginUser.JobNum;
                    model.PLOperateUserName = loginUser.UserName;
                    model.PLOperateTime = DateTime.Now;
                    model.PLAttachmentDownloadUrl = string.IsNullOrEmpty(model.PLAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PLAttachmentUrl);
                    _layeredauditDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PLProblemNo, "Edit LayeredAudit", "LayeredAudit", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static StandVerificationViewModel SaveStandVerification(ProblemVerificationModel model, UserLoginInfo loginUser)
        {
            var result = new StandVerificationViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PVIsValid = 1;
                    model.PVCreateUserNo = loginUser.JobNum;
                    model.PVCreateUserName = loginUser.UserName;
                    model.PVCreateTime = DateTime.Now;
                    model.PVOperateUserNo = loginUser.JobNum;
                    model.PVOperateUserName = loginUser.UserName;
                    model.PVOperateTime = DateTime.Now;
                    model.PVAttachmentDownloadUrl = string.IsNullOrEmpty(model.PVAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PVAttachmentUrl);
                    model.Id = _verificationDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PVProblemNo, "Add Verificatio", "Verificatio", string.Empty);
                }
                else
                {
                    //Update
                    model.PVIsValid = 1;
                    model.PVOperateUserNo = loginUser.JobNum;
                    model.PVOperateUserName = loginUser.UserName;
                    model.PVOperateTime = DateTime.Now;
                    model.PVAttachmentDownloadUrl = string.IsNullOrEmpty(model.PVAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PVAttachmentUrl);
                    _verificationDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PVProblemNo, "Edit Verificatio", "Verificatio", string.Empty);
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
        /// 描述:
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static StandardizationViewModel SaveStandardization(ProblemStandardizationModel model, UserLoginInfo loginUser)
        {
            var result = new StandardizationViewModel() { IsSuccess = true };
            try
            {
                //add
                if (model.Id == 0)
                {
                    //add
                    model.PSIsValid = 1;
                    model.PSCreateUserNo = loginUser.JobNum;
                    model.PSCreateUserName = loginUser.UserName;
                    model.PSCreateTime = DateTime.Now;
                    model.PSOperateUserNo = loginUser.JobNum;
                    model.PSOperateUserName = loginUser.UserName;
                    model.PSOperateTime = DateTime.Now;
                    model.PSAttachmentDownloadUrl = string.IsNullOrEmpty(model.PSAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PSAttachmentUrl);
                    model.Id = _standardizationDal.Insert(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSProblemNo, "Add Standardization", "Standardization", string.Empty);
                }
                else
                {
                    //Update
                    model.PSIsValid = 1;
                    model.PSOperateUserNo = loginUser.JobNum;
                    model.PSOperateUserName = loginUser.UserName;
                    model.PSOperateTime = DateTime.Now;
                    model.PSAttachmentDownloadUrl = string.IsNullOrEmpty(model.PSAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(model.PSAttachmentUrl);
                    _standardizationDal.Update(model);
                    result.Message = EncryptHelper.DesEncrypt(model.Id.ToString()); //TODO
                    result.data = model;
                    LogBusiness.Problemfollow(model.PSProblemNo, "Edit Standardization", "Standardization", string.Empty);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            return result;
        }

        #endregion

        #region UpdateProblem
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static ProblemViewModel UpdateProblemStatus(ProblemInfoModel proInfo, UserLoginInfo loginUser)
        {
            var returnvalue = new ProblemViewModel() { IsSuccess = false };
            proInfo.PIOperateUserNo = loginUser.JobNum;
            proInfo.PIOperateUserName = loginUser.UserName;
            proInfo.PIOperateTime = DateTime.Now;
            try
            {
                var result = _problemDal.Update(proInfo);
                LogBusiness.Problemfollow(proInfo.PIProblemNo, "Approve Problem", "ProblemInfo", string.Empty);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidActionCorrective(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _actioncorrectiveDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidActionContainment(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _actioncontainmentDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidFactorAnalysis(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _actionfactoranalysisDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidActionPreventive(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _actionpreventiveDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidActionWhyanalysis(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _actionwhyanalysisDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidStandLayeredAudit(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _layeredauditDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidQualityAlert(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _qualityalertDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidSolvingTeam(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _solvingteamDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }

        public static ResultInfoModel InvalidStandVerification(long id, UserLoginInfo loginUser)
        {
            var returnvalue = new ResultInfoModel() { IsSuccess = false };
            var upParam = new InvalidParam()
            {
                Id = id,
                JobNum = loginUser.JobNum,
                Name = loginUser.UserName,
                InvalidTime = DateTime.Now
            };
            try
            {
                var result = _verificationDal.Invalid(upParam);
                returnvalue.IsSuccess = result;
            }
            catch (Exception ex)
            {
                returnvalue.IsSuccess = false;
                returnvalue.Message = ex.Message;
            }
            return returnvalue;
        }
        #endregion

        #region SearchProblem

        /// <summary>
        /// 描述:获取完整的问题信息
        /// </summary>
        /// <param name="proId"></param>
        /// <returns></returns>
        public static ProblemUnionViewModel GetProblemUnionInfo(long proId)
        {
            var result = new ProblemUnionViewModel() { IsSuccess = true };
            var proinfo = _problemDal.GetProblemUnionInfo(proId);
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture1))
            { proinfo.problem.PIPicture1Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture1); }
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture2))
            { proinfo.problem.PIPicture2Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture2); }
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture3))
            { proinfo.problem.PIPicture3Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture3); }
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture4))
            { proinfo.problem.PIPicture4Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture4); }
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture5))
            { proinfo.problem.PIPicture5Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture5); }
            if (!string.IsNullOrEmpty(proinfo.problem.PIPicture6))
            { proinfo.problem.PIPicture6Url = UploadHelper.GetDownLoadUrl(proinfo.problem.PIPicture6); }
            if (proinfo.problem.PIStatus != null)
            { proinfo.problem.PIStatusDesc = EnumHelper.GetDescriptionByValue<ProblemStatusEnum>(proinfo.problem.PIStatus.GetValueOrDefault()); }
            if (proinfo.problem.PISeverity != null)
            { proinfo.problem.PISeverityDesc = EnumHelper.GetDescriptionByValue<ProblemSeverityEnum>(proinfo.problem.PISeverity.GetValueOrDefault()); }

            foreach (var item in proinfo.qualityalert)
            {
                item.PQAttachmentDownloadUrl = string.IsNullOrEmpty(item.PQAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PQAttachmentUrl);
            }
            foreach (var item in proinfo.actioncontainment)
            {
                item.PACAttachmentDownloadUrl = string.IsNullOrEmpty(item.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PACAttachmentUrl);
            }
            foreach (var item in proinfo.actioncorrective)
            {
                item.PACAttachmentDownloadUrl = string.IsNullOrEmpty(item.PACAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PACAttachmentUrl);
            }
            foreach (var item in proinfo.layeredaudit)
            {
                item.PLAttachmentDownloadUrl = string.IsNullOrEmpty(item.PLAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PLAttachmentUrl);
            }
            foreach (var item in proinfo.actionpreventive)
            {
                item.PAPAttachmentDownloadUrl = string.IsNullOrEmpty(item.PAPAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PAPAttachmentUrl);
            }
            foreach (var item in proinfo.standardization)
            {
                item.PSAttachmentDownloadUrl = string.IsNullOrEmpty(item.PSAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PSAttachmentUrl);
            }
            foreach (var item in proinfo.verification)
            {
                item.PVAttachmentDownloadUrl = string.IsNullOrEmpty(item.PVAttachmentUrl) ? string.Empty : UploadHelper.GetDownLoadUrl(item.PVAttachmentUrl);
            }
            result.data = proinfo;
            return result;
        }


        public static IEnumerable<ProblemInfoModel> ProblemSearchResult(ProblemSearchModel searchModel, out int totalCount)
        {
            var data = _problemDal.ProblemSearchResultPage(searchModel, out totalCount);
            return data;
        }

        public static List<ProblemInfoModel> GetMyToDoProblemList(UserLoginInfo loginUser)
        {
            var data = _problemDal.GetMyToDoProblemList(loginUser.JobNum);
            return data;
        }

        #endregion

        #region Other
        /// <summary>
        /// 生成新的问题单号
        /// </summary>
        /// <returns></returns>
        public static string GenerateProblemNo()
        {
            var problemNo = string.Empty;
            var getProblemNo = _problemDal.GetMaxProblemNoCurrentDay().FirstOrDefault();
            if (getProblemNo.PIProblemNo == null)
            {
                problemNo = "0001";
            }
            else
            {
                var length = getProblemNo.PIProblemNo.Length;
                problemNo = (Convert.ToInt32(getProblemNo.PIProblemNo.Substring(length - 4, 4)) + 1).ToString().PadLeft(4, '0');
            }
            var no = string.Format("{0}{1}", DateTime.Now.ToString(CommonConstant.DateTimeFormatDayOnly), problemNo);
            return no;
        }

        public static List<ProblemInfoModel> ProblemNoAuto(string key)
        {
            var list = new List<ProblemInfoModel>();
            if (!string.IsNullOrEmpty(key))
            {
                list = _problemDal.GetSelectProblemNo(key);
            }
            return list;
        }

        #endregion
    }
}
