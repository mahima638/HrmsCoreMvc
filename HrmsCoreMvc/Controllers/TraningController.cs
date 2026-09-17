using Microsoft.AspNetCore.Mvc;
using HrmsCoreMvc.Models;
using HrmsCoreMvc.Data;

namespace HrmsCoreMvc.Controllers
{
    public class TraningController : Controller
    {
        private readonly ApplicationDbContext traningDb;

        public TraningController (ApplicationDbContext traningDb)
        {
            this.traningDb = traningDb;
        }
        public IActionResult Index()
        {
            var TraningTypeData = traningDb.TraningType.ToList();
            return View(TraningTypeData);
        }
        public IActionResult AddTraningType()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTraningType(TrainingType train)
        {
            if (ModelState.IsValid){
                traningDb.TraningType.Add(train);
                traningDb.SaveChanges();
                return RedirectToAction("Index","Traning");
            }
            return View(train);        
        }

        public IActionResult EditTraningType(int? TrainingTypeId)
        {   
            if(TrainingTypeId == null  || traningDb.TraningType == null){return NotFound();}
            var TraningTypeData = traningDb.TraningType.Find(TrainingTypeId);
            if (TraningTypeData == null) { return NotFound(); }
            return View(TraningTypeData);
        }

        [HttpPost]
        public IActionResult EditTraningType(int? TrainingTypeId, TrainingType train)
        {
            if(TrainingTypeId != train.TrainingTypeId) { return NotFound(); }
            if (ModelState.IsValid)
            {
                traningDb.Update(train);
                traningDb.SaveChanges();
                return RedirectToAction("Index", "Traning");
            }
            return View(train);
        }

        public IActionResult DeleteTraningType(int? TrainingTypeId)
        {
            if (TrainingTypeId == null || traningDb.TraningType == null){return NotFound();}
            var TraningTypeData = traningDb.TraningType.FirstOrDefault(x => x.TrainingTypeId == TrainingTypeId);
            if (TraningTypeData == null) { return NotFound(); }
            return View(TraningTypeData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteTraningTypeConform(int? TrainingTypeId)
        {
            var TraningTypeData = traningDb.TraningType.Find(TrainingTypeId);
            if(TraningTypeData != null) 
            { 
                traningDb.TraningType.Remove(TraningTypeData); 

            }
            traningDb.SaveChanges ();
            return RedirectToAction("Index", "Traning");
        }

    }
}
