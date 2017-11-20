using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Code
{
    public class CodeSearchModel
    {
        public string Key { get; set; }
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

    public class DefectCodeSearchModel
    {
        public string codetype { get; set; }

        public string code { get; set; }

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
