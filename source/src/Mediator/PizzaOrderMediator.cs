using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator
{
    public class PizzaOrderSystem : IMediator
    {
        private readonly Customer _customer;
        private readonly Chef _kitchen;
        private readonly Delivery _delivery;

        public PizzaOrderSystem(Customer customer, Chef kitchen, Delivery delivery)
        {
            _customer = customer;
            _customer.SetMediator(this);
            _kitchen = kitchen;
            _kitchen.SetMediator(this);
            _delivery = delivery;
            _delivery.SetMediator(this);
        }

        public void Notify(object sender, Order order)
        {
            if (sender is null) return;
            if (order is null) return;

            switch (order.State)
            {
                case OrderStates.Placed:
                    Console.WriteLine("Mediator: Order placed. Sending to kitchen...");
                    _kitchen?.PreparePizza(order);
                    break;
                case OrderStates.Prepared:
                    Console.WriteLine("Mediator: Pizza prepared. Calling delivery...");
                    _delivery?.CollectPizza(order);
                    break;
                case OrderStates.Collected:
                    Console.WriteLine("Mediator: Pizza was collected by delivery. On its way...");
                    _delivery?.DeliverPizza(order);
                    break;
                case OrderStates.Delivered:
                    Console.WriteLine("Mediator: Pizza delivered. Notifying customer...");
                    _customer?.OrderComplete(order);
                    break;
            }
        }
    }
}
