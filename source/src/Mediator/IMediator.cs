namespace DesignPatterns.Mediator
{
    public interface IMediator
    {
        void Notify(object sender, Order order);
    }
}
