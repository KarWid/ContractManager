namespace ContractManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using ContractManager.Enums;
    using ContractManager.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class BaseController : Controller
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected void AddUiMessage(string message, UiMessageStatusType status)
        {
            var uiMessages = TempData[Constants.TempData.UI_MESSAGES] as List<UiMessage>;
            if (uiMessages == null)
            {
                uiMessages = new List<UiMessage>();
                uiMessages.Add(new UiMessage(message, status));
                TempData["UiMessages"] = uiMessages;
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
