using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Suggest
{
    public class SuggestionsSearchModel
    {
        [Display(Name ="DateForm")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateForm { get; set; }
        [Display(Name = "DateTo")]
        public DateTime DateTo { get; set; }

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
