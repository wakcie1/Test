using Model.CommonModel;
using Model.Problem;
using System.Collections.Generic;

namespace Model.ViewModel.Problem
{
    public class ProblemUnionViewModel : ResultInfoModel
    {
        public ProblemUnionInfo data { get; set; }
    }

    public class ProblemUnionInfo
    {
        private ProblemInfoModel _problemInfo = new ProblemInfoModel();
        private List<ProblemSolvingTeamModel> _solvingteamlist = new List<ProblemSolvingTeamModel>();
        private List<ProblemQualityAlertModel> _qualityalertlist = new List<ProblemQualityAlertModel>();
        private List<ProblemSortingActivityModel> _sortingactivity = new List<ProblemSortingActivityModel>();
        private List<ProblemActionContainmentModel> _actioncomtainment = new List<ProblemActionContainmentModel>();
        private List<ProblemActionFactorAnalysisModel> _actionfactoranalysis = new List<ProblemActionFactorAnalysisModel>();
        private List<ProblemActionWhyanalysisModel> _actionwhyanalysisi = new List<ProblemActionWhyanalysisModel>();
        private List<ProblemActionCorrectiveModel> _actioncorrective = new List<ProblemActionCorrectiveModel>();
        private List<ProblemActionPreventiveModel> _actionpreventive = new List<ProblemActionPreventiveModel>();
        private List<ProblemLayeredAuditModel> _layeredaudit = new List<ProblemLayeredAuditModel>();
        private List<ProblemVerificationModel> _verification = new List<ProblemVerificationModel>();
        private List<ProblemStandardizationModel> _standardization = new List<ProblemStandardizationModel>();

        public ProblemInfoModel problem
        {
            get {
                return _problemInfo;
            }
            set {
                _problemInfo = value;
            }
        }
        public List<ProblemSolvingTeamModel> solvingteam
        {
            get
            {
                return _solvingteamlist;
            }
            set
            {
                _solvingteamlist = value;
            }
        }
        public List<ProblemQualityAlertModel> qualityalert
        {
            get
            {
                return _qualityalertlist;
            }
            set
            {
                _qualityalertlist = value;
            }
        }
        public List<ProblemSortingActivityModel> sortingactivity
        {
            get
            {
                return _sortingactivity;
            }
            set
            {
                _sortingactivity = value;
            }
        }
        public List<ProblemActionContainmentModel> actioncontainment
        {
            get
            {
                return _actioncomtainment;
            }
            set
            {
                _actioncomtainment = value;
            }
        }
        public List<ProblemActionFactorAnalysisModel> actionfactoranalysis
        {
            get
            {
                return _actionfactoranalysis;
            }
            set
            {
                _actionfactoranalysis = value;
            }
        }
        public List<ProblemActionWhyanalysisModel> actionwhyanalysisi
        {
            get
            {
                return _actionwhyanalysisi;
            }
            set
            {
                _actionwhyanalysisi = value;
            }
        }
        public List<ProblemActionCorrectiveModel> actioncorrective
        {
            get
            {
                return _actioncorrective;
            }
            set
            {
                _actioncorrective = value;
            }
        }
        public List<ProblemActionPreventiveModel> actionpreventive
        {
            get
            {
                return _actionpreventive;
            }
            set
            {
                _actionpreventive = value;
            }
        }
        public List<ProblemLayeredAuditModel> layeredaudit
        {
            get
            {
                return _layeredaudit;
            }
            set
            {
                _layeredaudit = value;
            }
        }
        public List<ProblemVerificationModel> verification
        {
            get
            {
                return _verification;
            }
            set
            {
                _verification = value;
            }
        }
        public List<ProblemStandardizationModel> standardization
        {
            get
            {
                return _standardization;
            }
            set
            {
                _standardization = value;
            }
        }





    }
}
