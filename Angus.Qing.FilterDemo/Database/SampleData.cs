using Angus.Qing.FilterDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angus.Qing.FilterDemo.Database
{
    public class SampleData
    {
        public static List<User> users { get; set; }
        public static List<Role> roles { get; set; }
        public static List<RoleWithControllerAction> roleWithControllerAction { get; set; }

        static SampleData()
        {
            // 初始化用户
            users = new List<User>(){
                new User(){ Id=1, UserName="wangjie", RoleId=1},
                new User(){ Id=2, UserName ="senior1", RoleId=2},
                new User(){ Id=3, UserName ="senior2", RoleId=2},
                new User(){ Id=5, UserName="junior1", RoleId=3},
                new User(){ Id=6, UserName="junior2", RoleId=3},
                new User(){ Id=6, UserName="junior3", RoleId=3}
            };
            // 初始化角色
            roles = new List<Role>()
            {
                new Role() { Id=1, RoleName="管理员", Description="管理员角色"},
                new Role() { Id=2, RoleName="高级会员", Description="高级会员角色"},
                new Role() { Id=3, RoleName="初级会员", Description="初级会员角色"}
            };
            // 初始化角色和控制器以及Action的对应类
            roleWithControllerAction = new List<RoleWithControllerAction>()
            {
                new RoleWithControllerAction(){ Id=1, ControllerName="AuthFilters", ActionName="AdminUser", RoleIds="1"},
                new RoleWithControllerAction(){ Id=2, ControllerName="AuthFilters", ActionName="SeniorUser", RoleIds="1,2"},
                new RoleWithControllerAction(){ Id=3, ControllerName="AuthFilters", ActionName="JuniorUser", RoleIds="1,2,3"},
                new RoleWithControllerAction(){ Id=4, ControllerName="ActionFilters", ActionName="Index", RoleIds="2,3"}
            };
        }
    }
}