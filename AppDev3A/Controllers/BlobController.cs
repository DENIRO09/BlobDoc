using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppDev3A.Models;
namespace AppDev3A.Controllers
{
    [Authorize]
    public class BlobController : Controller
    {
        // GET: Blob
        public ActionResult Index(AppDevBusiness business)
        {
            return View(business.GetPhotos("images"));
        }
        public ActionResult UploadView()
        {
            return View("Upload");
        }
        public ActionResult Upload(PhotoUpload photo, AppDevBusiness appbusiness,string studentnumber)
        {
            if (photo.FileUpload != null && photo.FileUpload.ContentLength > 0)
            {
                appbusiness.UploadPhoto("images", photo.FileUpload,studentnumber);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id, AppDevBusiness appdevbusiness)
        {
            if(ModelState.IsValid)
            {
                appdevbusiness.DeletePhoto("images", id);
            }
            return  RedirectToAction("Index");
        }

    }
}