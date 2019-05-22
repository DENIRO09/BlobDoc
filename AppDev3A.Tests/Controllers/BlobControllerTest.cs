using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDev3A;
using AppDev3A.Controllers;
using AppDev3A.Models;
using System.Web.Mvc;
using System.Web;

namespace AppDev3A.Tests.Controllers
{
    [TestClass]
    public class BlobControllerTest
    {
        AppDevBusiness appDevBusiness = new AppDevBusiness();

        [TestMethod]
        public void BlobIndex_Test()
        {
            //Arrange
            var controller = new BlobController();

            //Actual
            ViewResult result = controller.Index(appDevBusiness) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BlobUploadView_Test()
        {
            //Arrange
            var controller = new BlobController();

            //Actual
            ViewResult result = controller.UploadView() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BlobUpload_Test()
        {
            //Arrange
            var controller = new BlobController();

            PhotoUpload ImageToInsert = new PhotoUpload
            {
                //not sure of data to enter for image
                //FileUpload = ,
                StudentNumber = "21638797"
               
            };

            //Act
            var result = controller.Upload(ImageToInsert, appDevBusiness, ImageToInsert.StudentNumber);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
