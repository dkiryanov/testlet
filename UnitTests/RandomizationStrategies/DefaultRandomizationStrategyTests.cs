using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Testlet;
using Testlet.RandomizationStrategies;

namespace UnitTests.RandomizationStrategies
{
    [TestFixture]
    public class DefaultRandomizationStrategyTests : TestBase
    {
        private IRandomizationStrategy _strategy;

        [SetUp]
        public void SetUp()
        {
            _strategy = new DefaultRandomizationStrategy();
        }

        [Test]
        public void Randomize_ItemsAreNull_ThrowsArgumentNullException()
        {
            // Arrange
            IList<Item> items = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _strategy.Randomize(items));
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(4, 6)]
        [TestCase(20, 10)]
        public void Randomize_ReturnsTheSameNumberOfItems(int pretestItemsCount, int operationalItemsCount)
        {
            // Arrange
            IList<Item> items = GetTestletItems(pretestItemsCount, operationalItemsCount);

            // Act
            IList<Item> randomizedItems = _strategy.Randomize(items);

            // Assert
            Assert.That(randomizedItems, Is.Not.Null);
            Assert.That(randomizedItems.Count, Is.EqualTo(pretestItemsCount + operationalItemsCount));

            int randomizedPretestItemsCount = randomizedItems.Count(item => item.ItemType == ItemTypeEnum.Pretest);
            Assert.That(randomizedPretestItemsCount, Is.EqualTo(pretestItemsCount));

            int randomizedOperationalItemsCount = randomizedItems.Count(item => item.ItemType == ItemTypeEnum.Operational);
            Assert.That(randomizedOperationalItemsCount, Is.EqualTo(operationalItemsCount));
        }
    }
}
