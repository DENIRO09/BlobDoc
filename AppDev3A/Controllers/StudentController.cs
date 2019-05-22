using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Threading.Tasks;
using AppDev3A.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace AppDev3A.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        
       
      
        // GET: Student
        [ActionName("Index")]
        public async Task<ActionResult> IndexAsync()
        {

            var students = await DocumentDBRepository<Student>.GetItemsAsync(d => !d.isActive || d.isActive);

            return RedirectToAction("Index","VM");
        }
        [ActionName("Create")]
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }
        //
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync ([Bind(Include = "Id,Name,Surname,Email,TelephoneNumber,CellphoneNumber,isActive")] Student student)
        {
            if (ModelState.IsValid)
            {
                
                await DocumentDBRepository<Student>.CreateItemAsync(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,Name,Surname,Email,TelephoneNumber,CellphoneNumber,isActive")] Student student)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Student>.UpdateItemAsync(student.Id, student);
                return RedirectToAction("Index","VM");
            }

            return View(student);
        }
       
        [ActionName("Edit")]
        public async Task<ActionResult> EditAsync(string id)
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

            return View(student);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository<Student>.DeleteItemAsync(student.Id, student);
                return RedirectToAction("Index");
            }
            return View(student);
        }
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id, AppDevBusiness appDevBusiness,string uriLink)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = await DocumentDBRepository<Student>.GetItemAsync(id);

            string link = "https://appdevproject3.blob.core.windows.net/images" + id;
            if(link==uriLink)
            {
                appDevBusiness.DeletePhoto("images", id);
            }
            
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        private static CloudBlobContainer GetBlobContainer(string containername)
        {

            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            var blobClient = storageAccount.CreateCloudBlobClient();

            var container = blobClient.GetContainerReference(containername);

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            container.CreateIfNotExists();

            container.SetPermissions(permissions);

            return container;
        }
    }

}