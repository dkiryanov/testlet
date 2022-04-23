using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Testlet;
using Testlet.Models;
using Testlet.RandomizationStrategies;
using Testlet.Validation;

namespace UnitTests
{
    [TestFixture]
    public class TestletClientTests : TestBase
    {
        private Mock<ITestletModelValidator> _validatorMock;
        private Mock<ITestletRandomizer> _randomizerMock;
        private Mock<IRandomizationStrategy> _strategyMock;

        private ITestletClient _client;

        [SetUp]
        public void SetUp()
        {
            _validatorMock = new Mock<ITestletModelValidator>();
            _randomizerMock = new Mock<ITestletRandomizer>();
            _strategyMock = new Mock<IRandomizationStrategy>();

            _client = new TestletClient(_validatorMock.Object, _randomizerMock.Object, _strategyMock.Object);
        }

        [Test]
        public void CreateTestlet_ReturnsTestlet()
        {
            // Arrange
            _randomizerMock
                .Setup(m => m.Randomize(It.IsAny<ITestletModel>(), It.IsAny<IRandomizationStrategy>()))
                .Returns(new TestletModel());

            // Act
            ITestletModel result = _client.CreateTestlet("123", new List<Item>());

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateTestlet_CallsValidator()
        {
            // Act
            _client.CreateTestlet("123", new List<Item>());

            // Assert
            _validatorMock.Verify(m => m.Validate(It.IsAny<ITestletModel>()), Times.Once);
        }
    }
}
