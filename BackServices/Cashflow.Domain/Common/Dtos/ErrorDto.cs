namespace Cashflow.Domain.Common.ViewModel
{
    public class ErrorDto
    {
        public string Error { get; set; }

        public static ErrorDto New(string error)
        {
            return new ErrorDto()
            {
                Error = error
            };
        }
    }
}
