using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.Areas.Admin.Models.MemberModels;
using SocialNetwork.Models;
using System;

namespace SocialNetwork.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new MemberListModel();
            return View(model);
        }

        public JsonResult GetMemberData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new MemberListModel();
            var data = model.GetMembers(dataTablesModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateMemberModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateMember();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create member");
                    _logger.LogError(ex, "Create Member Failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = new EditMemberModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditMemberModel model)
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
            var model = new MemberListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
