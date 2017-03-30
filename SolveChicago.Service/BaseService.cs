using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SolveChicago.Entities;
using SolveChicago.Common;

namespace SolveChicago.Service
{
    public class BaseService
    {
        protected SolveChicagoEntities db;

        public BaseService(SolveChicagoEntities db)
        {
            this.db = db;
        }

        protected string UploadPhoto(string directory, HttpPostedFileBase image, int id)
        {
            if (image != null)
            {
                Uri pictureUri = null;
                using (MemoryStream target = new MemoryStream())
                {
                    image.InputStream.CopyTo(target);
                    byte[] fileBytes = Helpers.ConvertImageToPng(target.ToArray(), 500, 500);
                    pictureUri = UploadToBlob(directory, string.Format("{0}.png", id), fileBytes);
                    return Helpers.RemoveSchemeFromUri(pictureUri);
                }
            }
            return null;
        }

        protected static Uri UploadToBlob(string containerName, string fileName, byte[] fileBytes)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromByteArray(fileBytes, 0, fileBytes.Length);
            return blockBlob.Uri;
        }

        protected static Uri UploadToBlob(string containerName, string fileName, Stream stream)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.UploadFromStream(stream);
            return blockBlob.Uri;
        }

        protected static Uri UploadToBlob(string containerName, string fileName, Stream stream, string name, string description)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.StorageConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

            if (name != null)
                blockBlob.Metadata["name"] = name;
            if (description != null)
                blockBlob.Metadata["description"] = description;

            blockBlob.UploadFromStream(stream);
            return blockBlob.Uri;
        }
    }
}