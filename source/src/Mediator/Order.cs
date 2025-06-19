using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediator
{
    public enum OrderStates
    {
        None = -1,
        Placed,
        Preparing,
        Prepared,
        Collecting,
        Collected,
        Delivering,
        Delivered,
        Completed
    }

    public class Order
    {
        private OrderStates _state = OrderStates.None;
        public Guid Id { get; private set; } = Guid.CreateVersion7();
        public string? Name { get; set; }
        public OrderStates State
        {
            get => _state;
            set => _state = value;
        }

        public Order() { }

        public Order(string name)
        {
            _state = OrderStates.Placed;
            Name = name;
        }
    }
}
