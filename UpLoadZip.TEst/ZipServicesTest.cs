using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using UpLoadZip.Web.Services;
using UpLoadZip.Web.Models;

namespace UpLoadZip.TEst
{
    [TestClass]
    public class ZipServicesTest
    {
        ZipServices service = new ZipServices();


        [TestMethod]
        public void TestGetStruct()
        {
            string filepath = "C:\\Users\\wersa\\OneDrive\\Desktop\\fashion-maverick-009.myshopify.com-export-20220529174812 (1).zip";
            var expected = GetModel();
            var result = service.GetStruct(filepath);

            /*т.к. у разных экземпляров класса разные значения ссылок в стеке, по этому сравнение сделал таким образом*/
            bool equality = false;
            if (expected.FileName == result.FileName && expected.Acrchive.OrderBy(x => x.Key).SequenceEqual(result.Acrchive.OrderBy(x => x.Key)))
                equality = true;

            Assert.AreEqual(equality, true);
        }



        private ZipStructModel GetModel()
        {
            ZipStructModel model = new ZipStructModel();
            model.FileName = @"C:\Users\wersa\OneDrive\Desktop\fashion-maverick-009.myshopify.com-export-20220529174812 (1).zip";
            model.Acrchive = new Dictionary<string, int>();
            model.Acrchive.Add("Article", 26);            
            model.Acrchive.Add("Comment", 74);
            model.Acrchive.Add("Customer", 163);
            model.Acrchive.Add("Order", 2);
            model.Acrchive.Add("Product", 24);
            model.Acrchive.Add("Blog", 29);
            return model;
        }
    }
}
