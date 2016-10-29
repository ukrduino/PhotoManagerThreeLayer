using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoManagerModels;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperConf>();
            });
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}
