using IS.Angus.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Angus.Service
{
    public class BirdFly : IFly
    {
        [Aop("BirdFly")]
        public void Fly()
        {
            Console.WriteLine("As a bird, I Can fly in the sky");
        }
    }
}
