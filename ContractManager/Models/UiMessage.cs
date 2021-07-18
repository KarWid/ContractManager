namespace ContractManager.Models
{
    using ContractManager.Enums;

    public class UiMessage
    {
        public UiMessage(string message, UiMessageStatusType status)
        {
            Message = message;
            Status = status;
        }

        public string Message { get; }
        public UiMessageStatusType Status {get;}
    }
}
