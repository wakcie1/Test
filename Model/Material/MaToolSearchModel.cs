using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Material
{
    public class MaToolSearchModel
    {
        private int _pageSize = 10;

        private int _currentPage = 1;

        public string MachineName { get; set; }
        public string EquipmentNo { get; set; }
        public string Type { get; set; }
        public string FixtureNo { get; set; }

        public int Classification { get; set; }

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
