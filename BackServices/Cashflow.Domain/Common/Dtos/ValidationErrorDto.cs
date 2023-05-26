namespace Cashflow.Domain.Common.ViewModel
{
    public class ValidationErrorDto
    {
        public List<string> Errors { get; set; } = new List<string>();

        public static ValidationErrorDto New(List<string> errors)
        {
            return new ValidationErrorDto()
            {
                Errors = errors
            };
        }
    }
}
