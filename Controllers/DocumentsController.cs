using CodeTheCloud.Models;
using CodeTheCloud.Repository;
using CodeTheCloud.ViewModels;
using Microsoft.AspNet.Identity;
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
    [Authorize]
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
        public ActionResult DocumentsInfo()
        {
            return View("DocumentsInfo");
        }


        [HttpGet]
        public ActionResult CreateDocument()
        {
            var model = new DocumentViewModel
            {
                ApplicantId = GetCurrentId()
            };

            return PartialView("_CreateDocument", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

                return RedirectToAction("DocumentsInfo", new { id = model.ApplicantId });
            }
            return RedirectToAction("DocumentsInfo", new { id = model.ApplicantId });
        }

        [HttpGet]
        public ActionResult GetDocuments()
        {
            var id = GetCurrentId();

            var documents = _repository.GetDocument(id);

            if (documents.Any() || documents != null)
            {
                return PartialView("_DocumentsIndex", documents);
            }

            return View("DocumentsInfo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDocument(int id, string azureFileName)
        {
            _repository.DeleteDocument(id);
            CloudBlobContainer sampleContainer = GetCloudBlobContainer();
            CloudBlockBlob blob = sampleContainer.GetBlockBlobReference(azureFileName);
            blob.Delete();
            return RedirectToAction("GetDocuments");
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
                CloudConfigurationManager.GetSetting("codethecloudstorage"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("codethecloudblob");
            return container;
        }
        private int GetCurrentId()
        {
            var userId = User.Identity.GetUserId();

            return _context.Applicants
                .Where(u => u.UserId == userId)
                .Select(i => i.Id).FirstOrDefault();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
