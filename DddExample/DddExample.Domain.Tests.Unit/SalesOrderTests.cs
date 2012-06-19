using System;
using DddExample.Domain.SalesOrders;
using NUnit.Framework;

namespace DddExample.Domain.Tests.Unit
{
    [TestFixture]
    public class SalesOrderTests
    {
        [Test]
        public void GivenBrandNewObject_WhenInitialised_ShouldHaveCorrectValues()
        {
            // Arrange
            var order = new SalesOrder();

            // Act
            order.Open(new SalesOrderId(1), new CustomerId(2), new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), (decimal)7.5);

            // Assert
            Assert.AreEqual(new SalesOrderId(1), order.Id);
            Assert.AreEqual(new CustomerId(2), order.CustomerId);
            Assert.AreEqual(SalesOrderStatus.Open, order.Status);
            Assert.AreEqual(new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), order.Address);
        }

        [Test]
        public void GivenOpenOrder_WhenCancelled_ShouldSucceed()
        {
            // Arrange
            var order = new SalesOrder();
            order.Open(new SalesOrderId(1), new CustomerId(2), new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), (decimal)7.5);

            // Act
            order.Cancel();            

            // Assert
            Assert.AreEqual(SalesOrderStatus.Cancelled, order.Status);
        }

        [Test]
        public void GivenOpenOrder_WhenAddingItems_ShouldSucceed()
        {
            // Arrange
            var order = new SalesOrder();
            order.Open(new SalesOrderId(1), new CustomerId(2), new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), (decimal)7.5);

            // Act
            order.AddItems(3, new ProductId(3), (decimal)2.5);

            // Assert
            Assert.AreEqual((decimal)7.5, order.TotalValue);
        }

        [Test]
        public void GivenOpenOrder_WhenAddingItemsThatExceedMaxOrderValue_ShouldFail()
        {
            // Arrange
            var order = new SalesOrder();
            order.Open(new SalesOrderId(1), new CustomerId(2), new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), (decimal)7.5);

            // Act
            order.AddItems(3, new ProductId(3), (decimal)2.5);

            // Assert
            Assert.Throws<InvalidOperationException>(() => order.AddItems(1, new ProductId(3), (decimal)2.5));
        }

        [Test]
        public void GivenPendingOrder_WhenPlaced_ShouldSucceed()
        {
            // Arrange
            var order = new SalesOrder();
            
            // Act
            order.Place();

            // Assert
            Assert.AreEqual(SalesOrderStatus.Pending, order.Status);
        }

        [Test]
        public void GivenNonPendingOrder_WhenCancelled_ShouldFail()
        {
            // Arrange
            var order = new SalesOrder();
            order.Open(new SalesOrderId(1), new CustomerId(2), new Address("1 Arnold Lane", "Elsewhere", new PostalCode("EL12", "1YY")), (decimal)7.5);
            order.Cancel();

            // Act / Assert
            Assert.Throws<InvalidOperationException>(() => order.Cancel());
        }
    }
}