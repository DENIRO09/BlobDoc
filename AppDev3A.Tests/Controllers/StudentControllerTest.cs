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

namespace AppDev3A.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        [TestMethod]
        public void StudentIndex_Test()
        {
            //Arrange
            StudentController controller = new StudentController();

            //Act
            var result = controller.IndexAsync();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StudentCreate_Test()
        {
            //Arrange
            StudentController controller = new StudentController();

            Student studentToInsert = new Student
            {
                Id = "1",
                Name = "Ayesha",
                Surname = "Khan",
                Email = "21638797@dut4life.ac.za",
                TelephoneNumber = "0955551234",
                CellphoneNumber = "0837311568",
                isActive = true
            };

            //Act
            var result = controller.CreateAsync(studentToInsert);

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void StudentEdit_Test()
        {
            //Arrange
            StudentController controller = new StudentController();

            var studentIdToEdit = "1";
            var newName = "Mustafa";

            Student student = new Student
            {
                Id = studentIdToEdit,
                Name = newName
            };
            
            //Act
            var result = controller.EditAsync(student);

            //Assert
            var studentRetrievedAgain = student;
            Assert.IsNotNull(result);
            Assert.AreEqual(studentRetrievedAgain.Id, studentIdToEdit);
            Assert.AreEqual(studentRetrievedAgain.Name, newName);
        }

        //[TestMethod]
        //public void Delete_Test()
        //{
        //    //Arrange
        //    //Act
        //    //Assert
        //}
    }
}
