using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using AppDev3A.Models;
using System.Net;

namespace AppDev3A.Controllers
{
    [Authorize]
    public class VMController : Controller
    {
        // GET: VM
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync(string containername)
        {

            var list = await DocumentDBRepository<Student>.GetItemsAsync(d => !d.isActive || d.isActive);
            List<VM> vm = new List<VM>();
            foreach (var item in list)
            {
                CloudBlobContainer container = GetBlobContainer(containername);
                CloudBlockBlob blockblob = container.GetBlockBlobReference(item.Id);
                VM m = new VM();
                m.Id = item.Id;
                m.Name = item.Name;
                m.Surname = item.Surname;
                m.isActive = item.isActive;
                m.TelephoneNumber = item.TelephoneNumber;
                m.CellphoneNumber = item.CellphoneNumber;
                m.Email = item.Email;
                m.NameView = blockblob.Name;
                m.URI = blockblob.Uri.ToString();
                m.StudentNumber = item.Id;
                vm.Add(m);

            }
            return View(vm);
        }


        private static CloudBlobContainer GetBlobContainer(string containername)
        {

            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference("images");
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            container.CreateIfNotExists();

            container.SetPermissions(permissions);

            return container;
        }


        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Id,Name,Surname,Email,TelephoneNumber,CellphoneNumber,isActive,StudentNumber")] VM vm, AppDevBusiness appbusiness, PhotoUpload photo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Student s = new Student();
                s.Name = vm.Name;
                s.Surname = vm.Surname;
                s.TelephoneNumber = vm.TelephoneNumber;
                s.isActive = vm.isActive;
                s.Id = vm.Id;
                s.Email = vm.Email;
                s.CellphoneNumber = vm.CellphoneNumber;

                photo.StudentNumber = vm.StudentNumber;

                String studentnumber = vm.Id;


                await DocumentDBRepository<Student>.CreateItemAsync(s);
                if (photo.FileUpload != null && photo.FileUpload.ContentLength > 0)
                {
                    appbusiness.UploadPhoto("images", photo.FileUpload, studentnumber);
                }

            }
            return RedirectToAction("Index");
        }

        
        [ActionName("Details")]
        public async Task<ActionResult> DetailsAsync(string id, string containername)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = await DocumentDBRepository<Student>.GetItemAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            CloudBlobContainer container = GetBlobContainer(containername);
            CloudBlockBlob blockblob = container.GetBlockBlobReference(student.Id);
            VM m = new VM();
            m.Id = student.Id;
            m.Name = student.Name;
            m.Surname = student.Surname;
            m.isActive = student.isActive;
            m.TelephoneNumber = student.TelephoneNumber;
            m.CellphoneNumber = student.CellphoneNumber;
            m.Email = student.Email;
            m.NameView = blockblob.Name;
            m.URI = blockblob.Uri.ToString();
            m.StudentNumber = student.Id;


            return View(m);
        }


    }
}