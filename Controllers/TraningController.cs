using HrmsCoreMvc.Data;
using HrmsCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCMINEHRMSCORE.Controllers
{
    public class TraningController : Controller
    {
        private readonly ApplicationDbContext traningDb;

        public TraningController(ApplicationDbContext traningDb)
        {
            this.traningDb = traningDb;
        }

        public IActionResult MainPage()
        {
            return View();
        }


        public IActionResult TrainingTypeIndex()
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
            if (ModelState.IsValid)
            {
                traningDb.TraningType.Add(train);
                traningDb.SaveChanges();
                return RedirectToAction("TrainingTypeIndex", "Traning");
            }
            return View(train);
        }

        public IActionResult EditTraningType(int? TrainingTypeId)
        {
            if (TrainingTypeId == null || traningDb.TraningType == null) { return NotFound(); }
            var TraningTypeData = traningDb.TraningType.Find(TrainingTypeId);
            if (TraningTypeData == null) { return NotFound(); }
            return View(TraningTypeData);
        }

        [HttpPost]
        public IActionResult EditTraningType(int? TrainingTypeId, TrainingType train)
        {
            if (TrainingTypeId != train.TrainingTypeId) { return NotFound(); }
            if (ModelState.IsValid)
            {
                traningDb.Update(train);
                traningDb.SaveChanges();
                return RedirectToAction("TrainingTypeIndex", "Traning");
            }
            return View(train);
        }

        public IActionResult DeleteTraningType(int? TrainingTypeId)
        {
            if (TrainingTypeId == null || traningDb.TraningType == null) { return NotFound(); }
            var TraningTypeData = traningDb.TraningType.FirstOrDefault(x => x.TrainingTypeId == TrainingTypeId);
            if (TraningTypeData == null) { return NotFound(); }
            return View(TraningTypeData);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteTraningTypeConform(int? TrainingTypeId)
        {
            var TraningTypeData = traningDb.TraningType.Find(TrainingTypeId);
            if (TraningTypeData != null)
            {
                traningDb.TraningType.Remove(TraningTypeData);

            }
            traningDb.SaveChanges();
            return RedirectToAction("TrainingTypeIndex", "Traning");
        }



        // =========================
        // TRAINER
        // =========================

        public IActionResult TrainerIndex()
        {
            var trainerData = traningDb.Trainer.ToList();

            return View(trainerData);
        }

        public IActionResult AddTrainer()
        {
            ViewBag.Roles = traningDb.role
        .Where(r => r.Status == "Active")
        .ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddTrainer(Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                traningDb.Trainer.Add(trainer);
                traningDb.SaveChanges();

                return RedirectToAction("TrainerIndex", "Traning");
            }

            // Reload roles if validation fails
            ViewBag.Roles = traningDb.role
                .Where(r => r.Status == "Active")
                .ToList();

            return View(trainer);
        }

        public IActionResult EditTrainer(int? TrainerId)
        {
            if (TrainerId == null || traningDb.Trainer == null)
            {
                return NotFound();
            }

            var TrainerData = traningDb.Trainer.Find(TrainerId);

            if (TrainerData == null)
            {
                return NotFound();
            }

            return View(TrainerData);
        }

        [HttpPost]
        public IActionResult EditTrainer(
            int? TrainerId,
            Trainer trainer)
        {
            if (TrainerId != trainer.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                traningDb.Update(trainer);
                traningDb.SaveChanges();

                return RedirectToAction("TrainerIndex", "Traning");
            }

            return View(trainer);
        }

        public IActionResult DeleteTrainer(int? TrainerId)
        {
            if (TrainerId == null || traningDb.Trainer == null)
            {
                return NotFound();
            }

            var TrainerData = traningDb.Trainer
                .FirstOrDefault(x => x.TrainerId == TrainerId);

            if (TrainerData == null)
            {
                return NotFound();
            }

            return View(TrainerData);
        }

        [HttpPost, ActionName("DeleteTrainer")]
        public IActionResult DeleteTrainerConfirm(int? TrainerId)
        {
            var TrainerData = traningDb.Trainer.Find(TrainerId);

            if (TrainerData != null)
            {
                traningDb.Trainer.Remove(TrainerData);
            }

            traningDb.SaveChanges();

            return RedirectToAction("TrainerIndex", "Traning");
        }

        // =========================
        // TRAINING
        // =========================

        public IActionResult TrainingIndex()
        {
            var trainingData = traningDb.Training
                .Include(x => x.Trainer)
                .Include(x => x.TrainingType)
                .ToList();

            return View(trainingData);
        }


        // GET: Add Training
        public IActionResult AddTraining()
        {
            ViewBag.Trainers = traningDb.Trainer
                .Where(x => x.Status == Status.Active)
                .ToList();

            ViewBag.TrainingTypes = traningDb.TraningType
                .Where(x => x.Status == Status.Active)
                .ToList();

            ViewBag.Employees = traningDb.user
       .Where(u => u.Status == "Active")
       .ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTraining(Training training)
        {
            training.CreatedBy = "Admin";
            training.ModifiedBy = "Admin";
            training.CreatedAt = DateTime.Now;
            training.ModifiedAt = DateTime.Now;

            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = traningDb.Trainer
                    .Where(x => x.Status == Status.Active)
                    .ToList();

                ViewBag.TrainingTypes = traningDb.TraningType
                    .Where(x => x.Status == Status.Active)
                    .ToList();

                ViewBag.Employees = traningDb.user
                    .Where(x => x.Status == "Active")
                    .ToList();

                return View(training);
            }

            traningDb.Training.Add(training);
            traningDb.SaveChanges();

            return RedirectToAction("TrainingIndex", "Traning");
        }



        // GET: Edit Training
        public IActionResult EditTraining(int id)
        {
            var trainingData = traningDb.Training
                .Include(x => x.Trainer)
                .Include(x => x.TrainingType)
                .Include(x => x.User)
                .FirstOrDefault(x => x.TrainingId == id);

            if (trainingData == null)
            {
                return NotFound();
            }

            ViewBag.Trainers = traningDb.Trainer
                .Where(x => x.Status == Status.Active)
                .ToList();

            ViewBag.TrainingTypes = traningDb.TraningType
                .Where(x => x.Status == Status.Active)
                .ToList();

            ViewBag.Employees = traningDb.user
                .Where(x => x.Status == "Active")
                .ToList();

            return View(trainingData);
        }


        // POST: Edit Training
        // POST: Edit Training
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTraining(Training training)
        {
            training.ModifiedBy = "Admin";
            training.ModifiedAt = DateTime.Now;

            ModelState.Remove("Trainer");
            ModelState.Remove("TrainingType");
            ModelState.Remove("User");
            ModelState.Remove("CreatedBy");
            ModelState.Remove("ModifiedBy");

            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = traningDb.Trainer
                    .Where(x => x.Status == Status.Active)
                    .ToList();

                ViewBag.TrainingTypes = traningDb.TraningType
                    .Where(x => x.Status == Status.Active)
                    .ToList();

                ViewBag.Employees = traningDb.user
                    .Where(x => x.Status == "Active")
                    .ToList();

                return View(training);
            }

            var existingTraining = traningDb.Training
                .FirstOrDefault(x => x.TrainingId == training.TrainingId);

            if (existingTraining == null)
            {
                return NotFound();
            }

            existingTraining.TrainerId = training.TrainerId;
            existingTraining.TrainingTypeId = training.TrainingTypeId;
            existingTraining.UserId = training.UserId;
            existingTraining.TrainingCost = training.TrainingCost;
            existingTraining.Description = training.Description;
            existingTraining.Status = training.Status;
            existingTraining.StartDate = training.StartDate;
            existingTraining.EndDate = training.EndDate;

            existingTraining.ModifiedBy = "Admin";
            existingTraining.ModifiedAt = DateTime.Now;

            traningDb.SaveChanges();

            return RedirectToAction("TrainingIndex", "Traning");
        }


        // GET: Delete Training
        public IActionResult DeleteTraining(int id)
        {
            var trainingData = traningDb.Training
                .Include(x => x.Trainer)
                .Include(x => x.TrainingType)
                .Include(x => x.User)
                .FirstOrDefault(x => x.TrainingId == id);

            if (trainingData == null)
            {
                return NotFound();
            }

            return View(trainingData);
        }


        // POST: Delete Training
        [HttpPost]
        [ActionName("DeleteTraining")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTrainingConfirm(int id)
        {
            var trainingData = traningDb.Training.Find(id);

            if (trainingData != null)
            {
                traningDb.Training.Remove(trainingData);
                traningDb.SaveChanges();
            }

            return RedirectToAction("TrainingIndex", "Traning");
        }
    }
}
