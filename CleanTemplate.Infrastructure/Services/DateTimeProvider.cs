using CleanTemplate.Application.Common.Interfaces;

namespace CleanTemplate.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
