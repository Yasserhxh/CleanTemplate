using FluentResults;
namespace CleanTemplate.Application.Common.Errors
{
    public class DuplicateEmailError : IError
    {
        public string Message => throw new NotImplementedException();

        public Dictionary<string, object> Metadata => throw new NotImplementedException();
        public List<IError> Reasons => throw new NotImplementedException();
    }
    
}
