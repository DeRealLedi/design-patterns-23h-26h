using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator
{
    public class Person
    {
        private protected IMediator _mediator;
        public Person(IMediator mediator = null) => _mediator = mediator;

        public void SetMediator(IMediator mediator) => _mediator = mediator;
    }

    public class Customer : Person
    {

        public void PlaceOrder(Order order)
        {
            Console.WriteLine("Customer: {0} order for pizza '{1}'.", order.State, order.Name);
            order.State = OrderStates.Placed;
            _mediator?.Notify(this, order);
        }

        public void OrderComplete(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("No order to complete.");
                return;
            }

            order.State = OrderStates.Completed;
            Console.WriteLine("Customer: Order {0}.", order.State);
        }
    }

    public class Chef : Person
    {
        public void PreparePizza(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("No order to prepare.");
                return;
            }

            order.State = OrderStates.Preparing;
            Console.WriteLine("Chef: {0} pizza.", order.State);
            order.State = OrderStates.Prepared;
            _mediator?.Notify(this, order);
        }
    }

    public class Delivery : Person
    {
        public void CollectPizza(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("No order to collect.");
                return;
            }

            order.State = OrderStates.Collecting;
            Console.WriteLine("Delivery: {0} pizza.", order.State);
            order.State = OrderStates.Collected;
            _mediator?.Notify(this, order);
        }

        public void DeliverPizza(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("No order to deliver.");
                return;
            }

            order.State = OrderStates.Delivering;
            Console.WriteLine("Delivery: {0} pizza.", order.State);
            order.State = OrderStates.Delivered;
            _mediator?.Notify(this, order);
        }
    }
}
