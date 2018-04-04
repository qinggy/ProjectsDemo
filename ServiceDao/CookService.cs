using Entities;
using ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceDao
{
    [AutoLog]
    public class CookService : ContextBoundObject, ICookService
    {
        [AutoLogMethod]
        public string GoToKitchen(string cookName, string hotel)
        {
            return hotel + "的" + cookName + "厨师正在做饭";
        }

        [AutoLogMethod]
        public Cook GetCookInfo()
        {
            return new Cook() { CookName = "张贤", Hotel = "凤凰大酒店"};
        }
    }
}
