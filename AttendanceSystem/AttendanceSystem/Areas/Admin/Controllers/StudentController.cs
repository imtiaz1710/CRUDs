using AttendanceSystem.Areas.Admin.Models.AttendanceModel;
using AttendanceSystem.Areas.Admin.Models.StudentModel;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystem.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new StudentListModel();
            return View(model);
        }

        public JsonResult GetStudentData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new StudentListModel();
            var data = model.GetStudents(dataTablesModel);
            return Json(data);
        }


        public IActionResult Create()
        {
            var model = new CreateStudentModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateStudent();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create student");
                    _logger.LogError(ex, "Create Student Failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = new EditStudentModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditStudentModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new StudentListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
