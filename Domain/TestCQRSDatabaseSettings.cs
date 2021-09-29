namespace TestCQRS3.Domain

{
    public class TestCQRS3DatabaseSettings : ITestCQRS3DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string LogDBName { get; set; }
    }

    public interface ITestCQRS3DatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string LogDBName { get; set; }
    }
}