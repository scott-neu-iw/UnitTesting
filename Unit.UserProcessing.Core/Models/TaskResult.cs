namespace Unit.UserProcessing.Core.Models
{
    public class TaskResult
    {
        public bool IsSuccessful { get; }
        public string ErrorMessage { get; }

        public TaskResult(bool isSuccessful, string errorMessage = "")
        {
            IsSuccessful = isSuccessful;
            ErrorMessage = errorMessage;
        }
    }
}
