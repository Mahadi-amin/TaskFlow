using Microsoft.AspNetCore.Mvc;
using System.Web;
using TaskFlow.Application.ServicesInterface;
using TaskFlow.Web.Models;

namespace TaskFlow.Web.Controllers
{
    public class StatusController : Controller
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IStatusService _statusService;

        public StatusController(ILogger<StatusController> logger,
            IStatusService statusService)
        {
            _logger = logger;
            _statusService = statusService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var status = _statusService.GetAllStatus();
            return View();
        }


        public JsonResult GetBarcodeJsonData([FromBody] StatusListModel model)
        {
            var result = _statusService.GetStatus(model.PageIndex, model.PageSize, model.Search,
                model.FormatSortExpression("StatusName", "Id"));

            var barcodeJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totalDisplay,
                data = (from record in result.data
                        select new string[]
                        {
                    HttpUtility.HtmlEncode(record.StatusName),
                    HttpUtility.HtmlEncode(record.StatusDescription ?? "N/A"),
                    record.Id.ToString()
                        }
                    ).ToArray()
            };

            return Json(barcodeJsonData);
        }



    }
}
