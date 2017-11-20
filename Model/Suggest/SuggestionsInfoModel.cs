using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Suggest
{
    public class SuggestionsInfoModel
    {
        public long Id { get; set; } 
        [Required]
        [Display(Name ="Require Type")]
        public string BFType { get; set; } 
        [Display(Name = "Phrase")]
        public string BFPhase { get; set; }
        [Display(Name ="")]
        public string BFDesc { get; set; }
        [Display(Name ="Assign To")]
        public string BFRespUserNo { get; set; } 
        public string BFRespName { get; set; }
        public int BFStatus { get; set; }
        public string BFPicture { get; set; }
        public string BFPictureUrl { get; set; }
        public string BFFeedBackComment { get; set; }
        public string BFCreateUserNo { get; set; }
        public string BFCreateUserName { get; set; }
        public DateTime BFCreateTime { get; set; }
        public string BFOperateUserNo { get; set; }
        public string BFOperateUserName { get; set; }
        public DateTime BFOperateTime { get; set; }
        public int BFIsValid { get; set; }
    }

    public class SuggestionModel : ResultInfoModel
    {
        public SuggestionsInfoModel model { get; set; }
    }
}
