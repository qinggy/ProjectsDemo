using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Composition.Hosting;

namespace System.ComponentModel.Composition.Aop.Test
{
    [TestClass]
    public class AOPCompositionContainerTest
    {

        private AOPCompositionContainer _container;

        [TestInitialize]
        public void Initialize()
        {
            var catalog = new AssemblyCatalog(this.GetType().Assembly);
            _container = new AOPCompositionContainer(catalog);

        }

        [TestMethod]
        public void TestMethod01()
        {
            var simpleService = _container.GetExportedValue<ISimpeMockService>();
            Assert.IsNotNull(simpleService);

            Assert.AreEqual("hello", simpleService.Say("hello"));
        }

        [TestMethod]
        public void TestMethod02()
        {
            var complexService = _container.GetExportedValue<IComplexMockService>();
           
            Assert.IsNotNull(complexService);
            Assert.IsNotNull(complexService.SimpeMockService);

            Assert.AreEqual("hello", complexService.SimpeMockService.Say("hello"));
            Assert.IsTrue(complexService.IsTrue());            
        }
    }


    public interface ISimpeMockService
    {
        string Say(string words);
    }

    [AOPExport(typeof(ISimpeMockService))]
    public class SimpeMockService : ISimpeMockService
    {
        public string Say(string words)
        {
            return words;
        }
    }

    public interface IComplexMockService
    {
        ISimpeMockService SimpeMockService { get; }

        bool IsTrue();
    }


    [AOPExport(typeof(IComplexMockService))]
    public class ComplexMockService : IComplexMockService
    {
        [Import]
        public ISimpeMockService SimpeMockService { get; set; }

        public bool IsTrue()
        {
            return true;
        }
    }

}
