using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IS.Esd.VersionManager.Web.ViewModels
{
    public class ProductVM
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "请输入产品名称")]
        [Display(Name = "产品名称")]
        public string Name { get; set; }
    }
}