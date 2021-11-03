using InventorySystem.Areas.Admin.Models;
using InventorySystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        private readonly ILogger<StockController> _logger;

        public StockController(ILogger<StockController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new StockListModel();
            return View(model);
        }

        public JsonResult GetStockData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new StockListModel();
            var data = model.GetStocks(dataTablesModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateStockModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateStockModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateStock();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create stock");
                    _logger.LogError(ex, "Create Stock Failed");
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var model = new EditStockModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditStockModel model)
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
            var model = new StockListModel();

            model.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
