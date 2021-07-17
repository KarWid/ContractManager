namespace ContractManager.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using ContractManager.Common.Models;
    using ContractManager.Common.Services;
    using ContractManager.ViewModels.Paragraphs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ParagraphController : BaseController
    {
        private readonly ILogger<ParagraphController> _logger;
        private readonly IMapper _mapper;
        private readonly IParagraphService _paragraphService;

        public ParagraphController(ILogger<ParagraphController> logger, IMapper mapper, 
            IParagraphService paragraphService)
        {
            _logger = logger;
            _mapper = mapper;
            _paragraphService = paragraphService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _paragraphService.GetListAsync();
            var result = _mapper.Map<List<ParagraphVM>>(list);
            return View("Index", result);
        }

        public async Task<IActionResult> Get(string id)
        {
            var paragraph = await _paragraphService.GetAsync(id);
            var result = _mapper.Map<ParagraphVM>(paragraph);

            return View("View", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ParagraphVM viewModel)
        {
            var paragraph = _mapper.Map<Paragraph>(viewModel);
            var newParagraph = await _paragraphService.AddAsync(paragraph);
            var result = _mapper.Map<ParagraphVM>(newParagraph);

            return View("View", result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var paragraph = await _paragraphService.GetAsync(id);
            var result = _mapper.Map<ParagraphVM>(paragraph);

            return View("Edit", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ParagraphVM viewModel)
        {
            var paragraph = _mapper.Map<Paragraph>(viewModel);
            var newParagraph = await _paragraphService.EditAsync(paragraph);
            var result = _mapper.Map<ParagraphVM>(newParagraph);

            return View("View", result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _paragraphService.DeleteAsync(id);
            ViewBag.IsElementSuccessfullyDeleted = result;
            return RedirectToAction("Index");
        }
    }
}
