using System;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlSugar;
using UnitTestStudio.ServiceReference;

namespace UnitTestStudio
{
    [TestClass]
    public class UnitTest
    {
        
        [TestMethod]
        public void TestMethodWCFPlugin()
        {
            using(var client = new LoginPluginClient())
            {
              var result= client.DoWork("zxl");
            }
            
        }
    }
}
