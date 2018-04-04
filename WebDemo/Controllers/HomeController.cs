using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return Content("home index");
        }


        public ActionResult AddUserInfo()
        {
             Models.SchoolDbContext dbContext =new SchoolDbContext();
            
            //Student student =new Student();
            //student.Address = "shit";
            //student.SName = "nimabi";
            //dbContext.Student.Add(student);
            //dbContext.SaveChanges();

            UserInfo user=new   UserInfo();
            user.UName = "laoma";
            user.UPwd = "itcast";

            dbContext.UserInfo.Add(user);
            dbContext.SaveChanges();
            return Content("添加用户成功！");
        }
    }
}
