using IS.Angus.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Angus.Service
{
    public class PlaneFly : IFly
    {
        [Aop("PlaneFly")]
        public void Fly()
        {
            Console.WriteLine("As a Plane, I will fly high");
        }
    }
}
