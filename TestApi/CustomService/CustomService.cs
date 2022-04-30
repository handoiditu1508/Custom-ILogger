namespace TestApi.CustomService
{
    public class CustomService : ICustomService
    {
        private string test;

        public void DoSomething(string message)
        {
            test = message;
        }
    }
}
