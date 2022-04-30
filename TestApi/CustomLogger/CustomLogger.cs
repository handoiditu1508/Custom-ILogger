using System.Runtime.Versioning;
using TestApi.CustomService;

namespace TestApi.CustomLogger
{
    public class CustomLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly ICustomService _customService;

        public CustomLogger(string categoryName, ICustomService customService)
        {
            _categoryName = categoryName;
            _customService = customService;
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var test = "123456asdzxc";
            _customService.DoSomething(formatter(state, exception));
            Console.WriteLine("MyLog: "+formatter(state, exception));
        }
    }

    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("CustomLogger")]
    public sealed class CustomLoggerProvider : ILoggerProvider
    {
        private readonly ICustomService _customService;

        public CustomLoggerProvider(ICustomService customService)
        {
            _customService = customService;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(categoryName, _customService);
        }

        public void Dispose()
        {
        }
    }

    public static class CustomLoggerProviderExtensions
    {
        public static ILoggingBuilder AddCustomLoggerProvider(this ILoggingBuilder builder)
        {
            builder.AddProvider(new CustomLoggerProvider(new CustomService.CustomService()));
            return builder;
        }
    }
}
