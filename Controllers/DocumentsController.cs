using CodeTheCloud.Models;
using CodeTheCloud.Repository;
using CodeTheCloud.ViewModels;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeTheCloud.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DocumentsRepository _repository;

        public DocumentsController()
        {
            _context = new ApplicationDbContext();
            _repository = new DocumentsRepository(_context);
        }

        [HttpGet]
        public ActionResult CreateDocument(int id)
        {
            var model = new DocumentViewModel
            {
                ApplicantId = id
            };
            return View();
        }

        [HttpPost]
        public ActionResult UploadDocument(DocumentViewModel model)
        {
            HttpPostedFileBase file = model.FormFile;

            var tempPath = Path.GetTempFileName();

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);

                file.SaveAs(tempPath);
                model.FilePath = tempPath;
                model.FileName = fileName;
                model.FileExtension = fileExtension;

                var documentId = _repository.SaveDocument(model);

                var azureFileName = model.ApplicantId + "_" + documentId + fileExtension;
                SendFilesToAzure(tempPath, azureFileName, file.ContentType);

                model.Id = documentId;
                model.AzureFileName = azureFileName;

                _repository.UpdateDocument(model);

                return RedirectToAction("Information", new { id = model.ApplicantId });
            }

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetDocuments(int id)
        {
            var documents = _repository.GetDocument(id);

            if (documents.Any() || documents != null)
            {
                return PartialView("_DocumentsIndex", documents);
            }

            return PartialView("SprInformation");
        }

        [HttpPost]
        public ActionResult DeleteDocument(int id, string azureFileName, string user)
        {
            _repository.DeleteDocument(id);
            CloudBlobContainer sampleContainer = GetCloudBlobContainer();
            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(azureFileName);
            blob.Delete();
            return RedirectToAction("GetDocuments", new { id = user });
        }

        private void SendFilesToAzure(string pathToTempFile, string uniqueFileName, string type)
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            BlobContainerPermissions permissions = container.GetPermissions();
            SharedAccessBlobPolicy policy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(30),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var result = container.CreateIfNotExistsAsync();

            CloudBlockBlob blob = container.GetBlockBlobReference(uniqueFileName);
            blob.Properties.ContentType = type;
            using (var fileStream = System.IO.File.OpenRead(pathToTempFile))
            {
                blob.UploadFromStream(fileStream);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetReferenceToViewAzureBlob(string referenceToBlob, string extensionName)
        {
            CloudBlobContainer container = GetCloudBlobContainer();
            CloudBlockBlob blob = container.GetBlockBlobReference(referenceToBlob);
            string filePath = blob.SnapshotQualifiedUri.ToString();

            var viewModel = new ViewDocumentViewModel
            {
                PathToFile = filePath,
                Extension = extensionName
            };

            return View("ViewDocument", viewModel);

        }

        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("somestorage"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("someblobstorage");
            return container;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
