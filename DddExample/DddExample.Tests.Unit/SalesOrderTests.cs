using NUnit.Framework;

namespace DddExample.Tests.Unit
{
    [TestFixture]
    public class SalesOrderTests
    {
        [Test]
        public void InitialisingOrderShouldWork()
        {
            // Arrange
            var order = new SalesOrder();

            // Act
            order.Initialise(new Id(1), new Id(2), SalesOrderStatus.New);

            // Assert
            Assert.AreEqual(1, order.Id.Value);
            Assert.AreEqual(2, order.CustomerId.Value);
            Assert.AreEqual(SalesOrderStatus.New, order.Status);
        }
    }
}