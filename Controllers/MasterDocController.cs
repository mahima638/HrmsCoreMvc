using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCMINEHRMSCORE.Controllers
{
    public class MasterDocController : Controller
    {
        private readonly ApplicationDbContext adminDocDb;

        public MasterDocController(ApplicationDbContext adminDocDb)
        {
            this.adminDocDb = adminDocDb;

        }

        // GET: MasterDoc/AddAdminDocName
        public IActionResult AddAdminDocName()
        {
            return View(new AddAdminDocName());
        }

        // POST: MasterDoc/AddAdminDocName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdminDocName(AddAdminDocName doc)
        {
            if (ModelState.IsValid)
            {
                adminDocDb.AddAdminDocName.Add(doc);
                adminDocDb.SaveChanges();

                return RedirectToAction("AdminDocNameIndex");
            }

            return View(doc);
        }

        // GET: MasterDoc/AdminDocNameIndex
        public IActionResult AdminDocNameIndex()
        {
            var documentData = adminDocDb.AddAdminDocName.ToList();

            return View(documentData);
        }


        // =========================
        // EDIT
        // =========================

        // GET: MasterDoc/EditAdminDocName/5
        public IActionResult EditAdminDocName(int id)
        {
            var document = adminDocDb.AddAdminDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }


        // POST: MasterDoc/EditAdminDocName
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAdminDocName(AddAdminDocName doc)
        {
            if (ModelState.IsValid)
            {
                adminDocDb.AddAdminDocName.Update(doc);
                adminDocDb.SaveChanges();

                return RedirectToAction("AdminDocNameIndex");
            }

            return View(doc);
        }


        // =========================
        // DELETE
        // =========================

        // GET: MasterDoc/DeleteAdminDocName/5
        public IActionResult DeleteAdminDocName(int id)
        {
            var document = adminDocDb.AddAdminDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }


        // POST: MasterDoc/DeleteAdminDocName
        [HttpPost, ActionName("DeleteAdminDocName")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminDocNameConfirm(int id)
        {
            var document = adminDocDb.AddAdminDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document != null)
            {
                adminDocDb.AddAdminDocName.Remove(document);
                adminDocDb.SaveChanges();
            }

            return RedirectToAction("AdminDocNameIndex");
        }






        //EmpConrrolere


        public IActionResult AddEmpDocName()
        {
            return View(new AddEmpDocName());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmpDocName(AddEmpDocName doc)
        {
            if (ModelState.IsValid)
            {
                adminDocDb.AddEmpDocName.Add(doc);
                adminDocDb.SaveChanges();

                return RedirectToAction("EmpDocNameIndex");
            }

            return View(doc);
        }

        public IActionResult EmpDocNameIndex()
        {
            var documentData = adminDocDb.AddEmpDocName.ToList();

            return View(documentData);
        }


        // EDIT
        public IActionResult EditEmpDocName(int id)
        {
            var document = adminDocDb.AddEmpDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmpDocName(AddEmpDocName doc)
        {
            if (ModelState.IsValid)
            {
                adminDocDb.AddEmpDocName.Update(doc);
                adminDocDb.SaveChanges();

                return RedirectToAction("EmpDocNameIndex");
            }

            return View(doc);
        }


        public IActionResult DeleteEmpDocName(int id)
        {
            var document = adminDocDb.AddEmpDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }


        [HttpPost, ActionName("DeleteEmpDocName")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmpDocNameConfirm(int id)
        {
            var document = adminDocDb.AddEmpDocName
                .FirstOrDefault(x => x.DocId == id);

            if (document != null)
            {
                adminDocDb.AddEmpDocName.Remove(document);
                adminDocDb.SaveChanges();
            }

            return RedirectToAction("EmpDocNameIndex");
        }
    }
}