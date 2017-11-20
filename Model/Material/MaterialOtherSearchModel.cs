using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaterialOtherSearchModel
    {
        private int _pageSize = 10;

        private int _currentPage = 1;
         
        public string Type { get; set; }

        public string WorkOrderName { get; set; }
        public string SapNo { get; set; }
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
