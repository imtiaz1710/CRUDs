using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Areas.Admin.Models.PhotoModels;
using SocialNetwork.Models;
using System;

namespace SocialNetwork.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class PhotoController : Controller
    {
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(ILogger<PhotoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new PhotoListModel();
            return View(model);
        }

        public JsonResult GetPhotoData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new PhotoListModel();
            var data = model.GetPhotos(dataTablesModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreatePhotoModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreatePhotoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.LoadUniquePhotoFileName();
                    model.CreatePhoto();
                    model.UploadPhoto();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create photo");
                    _logger.LogError(ex, "Create Photo Failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = new EditPhotoModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditPhotoModel model)
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
            var model = new PhotoListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
