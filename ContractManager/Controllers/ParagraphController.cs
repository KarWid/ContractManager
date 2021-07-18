namespace ContractManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ContractManager.Common.Models;
    using ContractManager.Common.Services;
    using ContractManager.ViewModels.Paragraphs;
    using ContractManager.Enums;
    using ContractManager.Models;

    public class ParagraphController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IParagraphService _paragraphService;

        public ParagraphController(ILogger<ParagraphController> logger, IMapper mapper, 
            IParagraphService paragraphService) : base(logger)
        {
            _mapper = mapper;
            _paragraphService = paragraphService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _paragraphService.GetAllAsync();
            var result = _mapper.Map<List<ParagraphVM>>(list);
            return View("Index", result);
        }

        public async Task<IActionResult> Get(string id)
        {
            try 
            {
                var paragraph = await _paragraphService.GetAsync(id);
                var result = _mapper.Map<ParagraphVM>(paragraph);
                return View("View", result);
            }
            catch(Exception ex)
            {
                HandleException(ex, null, "Paragraph - Get");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData[Constants.ViewData.ACTION] = "Add";
            return View("Edit", new ParagraphVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ParagraphVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData[Constants.ViewData.ACTION] = "Add";
                return View("Edit", viewModel);
            }

            try
            {
                var paragraph = _mapper.Map<Paragraph>(viewModel);
                var newParagraph = await _paragraphService.AddAsync(paragraph);
                var result = _mapper.Map<ParagraphVM>(newParagraph);
                AddUiMessage("Paragraph successfully created.", UiMessageStatusType.Success);
                return View("View", result);
            }
            catch (Exception ex)
            {
                HandleException(ex, null, "Paragraph - Add");
            }

            return View("Edit", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var paragraph = await _paragraphService.GetAsync(id);
                var result = _mapper.Map<ParagraphVM>(paragraph);

                ViewData[Constants.ViewData.ACTION] = "Edit";
                return View("Edit", result);
            }
            catch (Exception ex)
            {
                HandleException(ex, null, "Paragraph - Get");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ParagraphVM viewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData[Constants.ViewData.ACTION] = "Edit";
                return View("Edit", viewModel);
            }

            try
            {
                var paragraph = _mapper.Map<Paragraph>(viewModel);
                var newParagraph = await _paragraphService.EditAsync(paragraph);
                var result = _mapper.Map<ParagraphVM>(newParagraph);
                AddUiMessage("Paragraph successfully updated.", UiMessageStatusType.Success);
                return View("View", result);
            }
            catch (Exception ex)
            {
                HandleException(ex, null, "Paragraph - Add");
            }

            return View("Edit", viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var result = await _paragraphService.DeleteAsync(id);

            if (result)
            {
                AddUiMessage("Paragraph successfully removed.", UiMessageStatusType.Success);
            }
            else
            {
                AddUiMessage("Paragraph was not removed.", UiMessageStatusType.Error);
            }

            return RedirectToAction("Index");
        }
    }
}
