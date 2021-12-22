using ExamenTrueHome.DataAccess.Repository;
using ExamenTrueHome.Models;
using ExamenTrueHome.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Activity = ExamenTrueHome.Models.Activity;
using ActivityVieModel = ExamenTrueHome.Models.ViewModel.ActivityViewModel;

namespace ExamenTrueHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ActivityViewModel activityViewModel = new ActivityViewModel()
            {
                Activity = new Activity(),
                Properties = _unitOfWork.Property.GetListActive()
            };
            activityViewModel.Activity.Schedule = DateTime.Now;
            return View(activityViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActivityViewModel activityViewModel)
        {
            activityViewModel.Properties = _unitOfWork.Property.GetListActive();


            if (ModelState.IsValid)
            {
                var exist = _unitOfWork.Activity.CheckIfExis(activityViewModel);
                if (exist)
                {
                    ModelState.AddModelError(nameof(Activity.Schedule), "No se puede agendar en la misma hora");
                    return View(activityViewModel);
                }

                activityViewModel.Activity.Created_At = DateTime.Now;
                activityViewModel.Activity.Status_Id = 1;
                _unitOfWork.Activity.Add(activityViewModel.Activity);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(activityViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel()
            {
                Activity = new Activity(),
                Properties = _unitOfWork.Property.GetListActive(),
                Status = _unitOfWork.Status.GetListItems()
            };
            activityViewModel.Activity = _unitOfWork.Activity.Get(id);
            if (activityViewModel.Activity == null)
                return NotFound();

            activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
            activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);


            return View(activityViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ActivityViewModel activityViewModel)
        {

            if (activityViewModel.Activity.Status_Id == 3)
            {
                ModelState.AddModelError(nameof(Activity.Schedule), "La actividad no se puede reagendar cuando esta cancelada");
                activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
                activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);
                return View(activityViewModel);
            }
            if (ModelState.IsValid)
            {
                var exist = _unitOfWork.Activity.CheckIfExis(activityViewModel);
                if (exist)
                {
                    ModelState.AddModelError(nameof(Activity.Schedule), "La hora ya esta siendo utilizada, favor de seleccionar otra");
                    activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
                    activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);

                    return View(activityViewModel);
                }
                _unitOfWork.Activity.Update(activityViewModel.Activity);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(activityViewModel);
        }


        [HttpGet]
        public IActionResult EditReAgendar(int id)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel()
            {
                Activity = new Activity(),
                Properties = _unitOfWork.Property.GetListActive(),
                Status = _unitOfWork.Status.GetListItems()
            };
            activityViewModel.Activity = _unitOfWork.Activity.Get(id);
            if (activityViewModel.Activity == null)
                return NotFound();

            activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
            activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);


            ViewBag.Reagendar = true;
            ViewBag.ActionUrl = "EditReAgendar";
            return View("Edit", activityViewModel);
        }


        [HttpPost]
        public IActionResult EditReAgendar(ActivityViewModel activityViewModel)
        {
            ViewBag.Reagendar = true;
            ViewBag.ActionUrl = "EditReAgendar";

            if (activityViewModel.Activity.Status_Id == 3)
            {
                ModelState.AddModelError(nameof(Activity.Schedule), "La actividad no se puede reagendar cuando esta cancelada");
                activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
                activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);
                return View("Edit", activityViewModel);
            }
            if (ModelState.IsValid)
            {
                var exist = _unitOfWork.Activity.CheckIfExis(activityViewModel);
                if (exist)
                {
                    ModelState.AddModelError(nameof(Activity.Schedule), "La hora ya esta siendo utilizada, favor de seleccionar otra");
                    activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
                    activityViewModel.Activity.Status = _unitOfWork.Status.Get(activityViewModel.Activity.Status_Id);

                    return View("Edit", activityViewModel);
                }
                _unitOfWork.Activity.Update(activityViewModel.Activity);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", activityViewModel);
        }
        [HttpGet]
        public IActionResult Cancel(int id)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel()
            {
                Activity = new Activity(),
                Properties = _unitOfWork.Property.GetListActive(),
                Status = _unitOfWork.Status.GetListItems()
            };
            activityViewModel.Activity = _unitOfWork.Activity.Get(id);
            activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
            if (activityViewModel.Activity == null)
                return NotFound();

            return View(activityViewModel);
        }
        [HttpPost]
        public IActionResult Cancel(ActivityViewModel activityViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Activity.UpdateCancel(activityViewModel.Activity);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            activityViewModel.Status = _unitOfWork.Status.GetListItems();
            activityViewModel.Activity.Property = _unitOfWork.Property.Get(activityViewModel.Activity.Property_Id);
            return View(activityViewModel);
        }

        [HttpGet]
        public IActionResult AdvancedFilter()
        {
            return View();
        }

        public IActionResult GetAllFilterData(string start = null, string end = null, int status = 0)
        {

            var list = _unitOfWork.Activity.GetAllFilterData(start, end, status);
            return Json(list);
        }

        [HttpGet]
        public IActionResult Survey(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDataStatus()
        {
            var listStatus = _unitOfWork.Status.GetListItems();
            return Json(listStatus);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var a = _unitOfWork.Activity.GetAll(includeProperties: "Property,Status");
            return Json(new { data = a });
        }

        [HttpGet]
        public IActionResult GetAllFilter()
        {
            var a = _unitOfWork.Activity.GetAllFilterCondition(includeProperties: "Property,Status");
            return Json(new { data = a });
        }

        [HttpGet]
        public IActionResult GetAllCondition()
        {
            var a = _unitOfWork.Activity.GetAllCondition(includeProperties: "Property,Status");
            return Json(new { data = a });
        }

    }
}
