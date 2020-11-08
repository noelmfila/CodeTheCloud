using CodeTheCloud.Models;
using CodeTheCloud.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CodeTheCloud.Repository
{
    public class DocumentsRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DocumentViewModel> GetDocument(int id)
        {
            var docs = _context.Documents.Where(d => d.ApplicantId == id).ToList();

            if (docs == null) return null;

            var documents = new List<DocumentViewModel>();
            foreach (var x in docs)
            {
                var viewModel = new DocumentViewModel()
                {
                    Id = x.Id,
                    ApplicantId = x.ApplicantId,
                    Description = x.Description,
                    FileName = x.FileName,
                    UploadDate = x.UploadDate,
                    FilePath = x.FilePath,
                    AzureFileName = x.AzureFileName,
                    FileExtension = x.FileExtension
                };

                documents.Add(viewModel);
            }

            return documents;
        }

        public int SaveDocument(DocumentViewModel model)
        {
            var document = new Document
            {
                ApplicantId = model.ApplicantId,
                Description = model.Description,
                FileName = model.FileName,
                UploadDate = model.UploadDate,
                FilePath = model.FilePath,
                AzureFileName = model.AzureFileName,
                FileExtension = model.FileExtension
            }; 

            _context.Documents.Add(document);
            _context.SaveChanges();
            return document.Id;
        }

        public void UpdateDocument(DocumentViewModel model)
        {
            var updateDocument = new Document
            {
                ApplicantId = model.ApplicantId,
                Description = model.Description,
                FileName = model.FileName,
                UploadDate = model.UploadDate,
                FilePath = model.FilePath,
                AzureFileName = model.AzureFileName,
                FileExtension = model.FileExtension
            };

            _context.Entry(updateDocument).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteDocument(int id)
        {
            var document = _context.Documents.Find(id);
            _context.Documents.Remove(document);
            _context.SaveChanges();
        }

    }
}