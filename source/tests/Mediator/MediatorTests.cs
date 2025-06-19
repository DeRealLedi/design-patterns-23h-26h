using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.Mediator;
using FluentAssertions;

namespace DesignPatterns.Tests.Mediator
{
    public class MediatorTests
    {
        [Fact]
        public void Order_State_DefaultsToNone()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            order.State.Should().Be(OrderStates.None);
        }

        [Fact]
        public void Order_State_ReturnsCompletedOnSuccessfullRun()
        {
            // Arrange
            var customer = new Customer();
            var orderSystem = new PizzaOrderSystem(customer, new Chef(), new Delivery());
            var order = new Order("Margherita");

            // Act
            customer.PlaceOrder(order);

            // Assert
            order.State.Should().Be(OrderStates.Completed);
        }

        [Fact]
        public void Order_Name_IsPreservedThroughoutProcess()
        {
            // Arrange
            var customer = new Customer();
            var orderSystem = new PizzaOrderSystem(customer, new Chef(), new Delivery());
            var order = new Order("Margherita");

            // Act
            customer.PlaceOrder(order);

            // Assert
            order.Name.Should().Be("Margherita");
            order.State.Should().Be(OrderStates.Completed);
        }

        [Fact]
        public void Customer_OrderComplete_WithNullOrder_ShouldNotThrow()
        {
            // Arrange
            var customer = new Customer();

            // Act
            Action act = () => customer.OrderComplete(null);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Chef_PreparePizza_TransitionsStateToPrepared()
        {
            // Arrange
            var chef = new Chef();
            var order = new Order("Margherita");

            // Act
            chef.PreparePizza(order);

            // Assert
            order.State.Should().Be(OrderStates.Prepared);
        }

        [Fact]
        public void Mediator_Notify_WithNullSender_DoesNothing()
        {
            // Arrange
            var mediator = new PizzaOrderSystem(new Customer(), new Chef(), new Delivery());
            var order = new Order("Margherita");

            // Act
            Action act = () => mediator.Notify(null, order);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void Mediator_Notify_WithNullOrder_DoesNothing()
        {
            // Arrange
            var customer = new Customer();
            var mediator = new PizzaOrderSystem(customer, new Chef(), new Delivery());

            // Act
            Action act = () => mediator.Notify(customer, null);

            // Assert
            act.Should().NotThrow();
        }
    }
}
