namespace Core.DataContracts.Networking
{
    public class OperationReponse<T>
    {
        public bool IsOperationSuccessful { get; set; }
        public T? Result { get; set; }

        // TODO implement generic error codes for front-end to handling
        public string? ErrorMessage { get; set; }

        public OperationReponse(T result)
        {
            IsOperationSuccessful = true;
            Result = result;
        }

        public OperationReponse(string errorMessage)
        {
            IsOperationSuccessful = false;
            ErrorMessage = errorMessage;
        }
    }
}
