using AttendanceSystem.Areas.Admin.Models.AttendanceModel;
using AttendanceSystem.Areas.Admin.Models.StudentModel;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AttendanceSystem.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AttendanceController : Controller
    {
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(ILogger<AttendanceController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new AttendanceListModel();
            return View(model);
        }

        public JsonResult GetAttendanceData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new AttendanceListModel();
            var data = model.GetAttendances(dataTablesModel);
            return Json(data);
        }


        public IActionResult Create()
        {
            var model = new CreateAttendanceModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateAttendanceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateAttendance();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create attendance");
                    _logger.LogError(ex, "Create Attendance Failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = new EditAttendanceModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditAttendanceModel model)
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
            var model = new AttendanceListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
