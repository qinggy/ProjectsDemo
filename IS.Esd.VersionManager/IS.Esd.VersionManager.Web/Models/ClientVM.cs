using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IS.Esd.VersionManager.Web.ViewModels
{
    public class ClientVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "请输入客户名称")]
        [Display(Name="客户名称")]
        public string Name { get; set; }
    }
}