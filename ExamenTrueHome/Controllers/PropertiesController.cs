using ExamenTrueHome.DataAccess.Repository;
using ExamenTrueHome.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenTrueHome.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PropertiesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ExistInActivity = false;
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var a = _unitOfWork.Property.GetAll(includeProperties:"Status");
            return Json(new { data = a });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Property property)
        {
            if (ModelState.IsValid)
            {
                property.Created_At = DateTime.Now;
                property.Status_Id = 1;
                _unitOfWork.Property.Add(property);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PropertyViewModel property = new PropertyViewModel()
            {
                Property = new Property(),
                Statuses = _unitOfWork.Status.GetListItems()
            };
            property.Property = _unitOfWork.Property.Get(id);
            if (property.Property == null)
                return NotFound();

            return View(property);
        }

        [HttpPost]
        public IActionResult Edit(PropertyViewModel propertyViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Property.Update(propertyViewModel.Property);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var exist = _unitOfWork.Property.ExistInActivity(id);
            return Json(exist);

        }

        [HttpDelete]
        public IActionResult DeleteConfirm(int id)
        {
            _unitOfWork.Property.Remove(id);
            _unitOfWork.Save();
            return Json(new {data = true });
        }

    }
}
