namespace ContractManager.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using ContractManager.Common.Services;
    using ContractManager.Models;
    using ContractManager.Repository.Entities;
    using ContractManager.Repository.Repositories;
    using ContractManager.ViewModels.Paragraphs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IParagraphService _paragraphService;

        public HomeController(ILogger<HomeController> logger, IParagraphService paragraphService)
        {
            _logger = logger;
            _paragraphService = paragraphService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _paragraphService.GetAsync("00000064293ae16dca5f2bd8");

            // mapujesz paragraph do view modelu

            return View(new ParagraphVM());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
