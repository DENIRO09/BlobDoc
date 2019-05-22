using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using AppDev3A.Models;

namespace AppDev3A.Models
{
    public class AppDevBusiness
    {
        public void UploadPhoto(string containername, HttpPostedFileBase file, string studentnumber)
        {
            var container = GetBlobContainer(containername);

            //Student Number reference
            var fileName = studentnumber;

            var blockBlob = container.GetBlockBlobReference(fileName);

            blockBlob.UploadFromStream(file.InputStream);
        }
        public void DeletePhoto(string containername,string id)
        {

            CloudBlobContainer container = GetBlobContainer(containername);
            CloudBlockBlob blockblob = container.GetBlockBlobReference(id);
            blockblob.Delete();

        }

        public List<ViewModelBlobs> GetPhotos(string containername)
        {
            var container = GetBlobContainer(containername);

            var returnList = new List<ViewModelBlobs>();
           
            if (container.ListBlobs(null, false).Count() > 0)
            {
                foreach (var item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        var blob = (CloudBlockBlob)item;

                        returnList.Add(new ViewModelBlobs()
                        {
                            Name = blob.Name,
                            URI = blob.Uri.ToString()       
                        }
                            );
                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        var pageBlob = (CloudPageBlob)item;

                        returnList.Add(new ViewModelBlobs()
                        {
                            // Information that is on the index page
                            Name = pageBlob.Name,
                            URI = pageBlob.Uri.ToString()
                        }
                        );

                    }
                }
            }
            return returnList;
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