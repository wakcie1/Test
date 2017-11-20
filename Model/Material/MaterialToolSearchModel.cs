using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaterialToolSearchModel
    {
        public string ToolNo { get; set; }
        public string ProductName { get; set; }
        public string ToolSupplier { get; set; }

        private int _pageSize = 10;

        private int _currentPage = 1; 

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
