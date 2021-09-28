namespace TestCQRS3.Domain.Contracts
{
    public interface IServiceWrapper
    {
        IItemService Item { get; }
        IItem2Service Item2 { get; }
    }
}
