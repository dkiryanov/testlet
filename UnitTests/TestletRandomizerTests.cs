using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Testlet;
using Testlet.Models;

namespace UnitTests
{
    [TestFixture]
    public class TestletRandomizerTests : TestBase
    {
        private const int PretestItemsCount = 4;
        private const int OperationalItemsCount = 6;
        private const int ItemsCount = 10;

        private ITestletRandomizer _testletRandomizer;

        [SetUp]
        public void SetUp()
        {
            _testletRandomizer = new TestletRandomizer();
        }

        [Test]
        public void Randomize_WellFormedTestletItems_ReturnsTestletWithTwoPretestsFirst()
        {
            // Arrange
            ITestletModel testletModel = GetTestletModel(PretestItemsCount, OperationalItemsCount);

            // Act
            ITestletModel result = _testletRandomizer.Randomize(testletModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(ItemsCount));

            List<Item> firstTwoItems = result.Items.Take(2).ToList();

            Assert.That(firstTwoItems.All(item => item.ItemType == ItemTypes.Pretest), Is.True);
        }

        [Test]
        public void Randomize_WellFormedTestletItems_ReturnsTestletContainingTwoPretestAndSixOperationalItems()
        {
            // Arrange
            ITestletModel testletModel = GetTestletModel(PretestItemsCount, OperationalItemsCount);

            // Act
            ITestletModel result = _testletRandomizer.Randomize(testletModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(ItemsCount));

            List<Item> lastEightItems = result.Items.Skip(2).ToList();

            int pretestItemsCount = lastEightItems.Count(item => item.ItemType == ItemTypes.Pretest);
            Assert.That(pretestItemsCount, Is.EqualTo(2));

            int operationalItemsCount = lastEightItems.Count(item => item.ItemType == ItemTypes.Operational);
            Assert.That(operationalItemsCount, Is.EqualTo(6));
        }

        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(3, 5)]
        public void Randomize_MalformedTestletItems_ReturnsTestletWithItems(int pretestItemsCount, int operationalItemsCount)
        {
            // Arrange
            ITestletModel testletModel = GetTestletModel(pretestItemsCount, operationalItemsCount);

            // Act
            ITestletModel result = _testletRandomizer.Randomize(testletModel);

            // Assert
            Assert.That(result, Is.Not.Null);

            int itemsCountExpected = pretestItemsCount + operationalItemsCount;

            Assert.That(result.Items.Count, Is.EqualTo(itemsCountExpected));
        }

        [Test]
        public void Randomize_TestletIsNull_ThrowsArgumentIsNullException()
        {
            // Arrange
            ITestletModel testletModel = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _testletRandomizer.Randomize(testletModel));
        }

        [Test]
        public void Randomize_TestletItemsCollectionIsNull_ThrowsArgumentIsNullException()
        {
            // Arrange
            ITestletModel testletModel = new TestletModel()
            {
                Items = null
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _testletRandomizer.Randomize(testletModel));
        }
    }
}