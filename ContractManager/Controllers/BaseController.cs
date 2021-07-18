namespace ContractManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using ContractManager.Enums;
    using ContractManager.Models;

    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected void AddUiMessage(string message, UiMessageStatusType status)
        {
            var uiMessages = JsonConvert.DeserializeObject<List<UiMessage>>((string)TempData[Constants.TempData.UI_MESSAGES] ?? string.Empty);
            if (uiMessages == null)
            {
                uiMessages = new List<UiMessage>();
                uiMessages.Add(new UiMessage(message, status));
                TempData[Constants.TempData.UI_MESSAGES] = JsonConvert.SerializeObject(uiMessages);
                return;
            }

            uiMessages.Add(new UiMessage(message, status));
        }

        protected void HandleException(Exception exception, string errorMessageOnUi = null, string additionalMessage = null)
        {
            errorMessageOnUi = errorMessageOnUi ?? "An error occured. Please contact with developer.";

            _logger.LogError(exception, additionalMessage);
            AddUiMessage(errorMessageOnUi, UiMessageStatusType.Error);
        }
    }
}
