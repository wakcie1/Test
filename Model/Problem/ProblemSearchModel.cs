using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Problem
{
    public class ProblemSearchModel
    {
        private int _pageSize = 10;

        private int _currentPage = 1;

        public Nullable<DateTime> DateForm { get; set; }
        public Nullable<DateTime> DateTo { get; set; }
        public string Process { get; set; }
        public string PlantNo { get; set; }
        public Nullable<DateTime> ReportDate { get; set; }
        public Nullable<DateTime> NextProblemDateFrom { get;set;}
        public Nullable<DateTime> NextProblemDateTo { get; set; }
        public string WorkOrderNo { get; set; }
        public string ToolingNo { get; set; }
        public string MachineNo { get; set; }
        public string ProblemSeverity { get; set; }
        public int Status { get; set; }
        public string KeyWords { get; set; }
        public string Repeatable { get; set; }
        public string SapNo { get; set; }
        public string PartName { get; set; }
        public string Customer { get; set; }
        public string Source { get; set; }
        public string DefectType { get; set; }

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
