using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HrmsCoreHRMSCORE.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext documentDb;

        public DocumentController(ApplicationDbContext documentDb)
        {
            this.documentDb = documentDb;
        }


        // =========================
        // DOCUMENT
        // =========================

        // GET: Document/Index
        public IActionResult Index()
        {
            return View();
        }


        // GET: Document/AdminFileUpload
        public IActionResult AdminFileUpload()
        {
            // User Email
            ViewBag.Users = documentDb.user
                .Where(x => x.Email != null && x.Status == "Active")
                .ToList();

            // Master Documents
            ViewBag.Documents = documentDb.AddAdminDocName
                .ToList();

            return View();
        }


        // POST: Document/AdminFileUpload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminFileUpload(
            AdminDocuments document,
            IFormFile pdfFile)
        {
            // Remove validation for DocFile
            // because PDF is received through pdfFile
            ModelState.Remove("DocFile");


            // Reload dropdowns
            ViewBag.Users = documentDb.user
                .Where(x => x.Email != null && x.Status == "Active")
                .ToList();

            ViewBag.Documents = documentDb.AddAdminDocName
                .ToList();


            // =========================
            // CHECK PDF
            // =========================

            if (pdfFile == null || pdfFile.Length == 0)
            {
                ModelState.AddModelError(
                    "DocFile",
                    "Please upload a PDF file.");
            }
            else
            {
                string extension =
                    Path.GetExtension(pdfFile.FileName)
                    .ToLower();

                if (extension != ".pdf")
                {
                    ModelState.AddModelError(
                        "DocFile",
                        "Only PDF files are allowed.");
                }
            }


            // =========================
            // MODEL VALIDATION
            // =========================

            if (!ModelState.IsValid)
            {
                return View(document);
            }


            // =========================
            // UPLOAD FILE
            // =========================

            string uploadFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "admin-documents"
            );


            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }


            // Generate unique file name
            string fileName =
                Guid.NewGuid().ToString() + ".pdf";


            string filePath =
                Path.Combine(uploadFolder, fileName);


            // Save PDF
            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }


            // Save file name in database
            document.DocFile = fileName;


            // Save database record
            documentDb.AdminDocuments.Add(document);

            await documentDb.SaveChangesAsync();


            TempData["Success"] =
                "Document uploaded successfully.";


            return RedirectToAction(
                "AdminFileUpload",
                "Document");
        }


        // =========================
        // UPLOADED DOCUMENT LIST
        // =========================

        // GET: Document/FetchEmpDoc
        public IActionResult FetchEmpDoc()
        {
            var documentData = documentDb.AdminDocuments
                .ToList();

            return View(documentData);
        }


        // =========================
        // VIEW DOCUMENT
        // =========================

        // GET: Document/ViewDocument
        public IActionResult ViewDocument(int id)
        {
            var document = documentDb.AdminDocuments
                .FirstOrDefault(x => x.ADocId == id);

            if (document == null)
            {
                return NotFound();
            }


            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "admin-documents",
                document.DocFile
            );


            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }


            string fileUrl =
                "/uploads/admin-documents/" + document.DocFile;


            return Redirect(fileUrl);
        }


        // =========================
        // DOWNLOAD DOCUMENT
        // =========================

        // GET: Document/DownloadDocument
        public IActionResult DownloadDocument(int id)
        {
            var document = documentDb.AdminDocuments
                .FirstOrDefault(x => x.ADocId == id);

            if (document == null)
            {
                return NotFound();
            }


            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "admin-documents",
                document.DocFile
            );


            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }


            byte[] fileBytes =
                System.IO.File.ReadAllBytes(filePath);


            return File(
                fileBytes,
                "application/pdf",
                document.DocFile
            );
        }


        // =========================
        // DELETE DOCUMENT
        // =========================

        // GET: Document/DeleteDocument
        public IActionResult DeleteDocument(int id)
        {
            var document = documentDb.AdminDocuments
                .FirstOrDefault(x => x.ADocId == id);

            if (document == null)
            {
                return NotFound();
            }


            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "admin-documents",
                document.DocFile
            );


            // Delete physical PDF
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            // Delete database record
            documentDb.AdminDocuments.Remove(document);

            documentDb.SaveChanges();


            return RedirectToAction(
                "FetchEmpDoc",
                "Document");
        }
    }
}