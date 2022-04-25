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
            ITestletModel testletModel = GetTestletModel(Constants.DefaultPretestItemsCount, Constants.DefaultOperationalItemsCount);

            // Act
            ITestletModel result = _testletRandomizer.Randomize(testletModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(Constants.DefaultItemsCount));

            List<Item> firstTwoItems = result.Items.Take(Constants.DefaultPretestItemsToTakeFirst).ToList();

            Assert.That(firstTwoItems.All(item => item.ItemType == ItemTypeEnum.Pretest), Is.True);
        }

        [Test]
        public void Randomize_WellFormedTestletItems_ReturnsTestletContainingTwoPretestAndSixOperationalItems()
        {
            // Arrange
            ITestletModel testletModel = GetTestletModel(Constants.DefaultPretestItemsCount, Constants.DefaultOperationalItemsCount);

            // Act
            ITestletModel result = _testletRandomizer.Randomize(testletModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count, Is.EqualTo(Constants.DefaultItemsCount));

            List<Item> lastEightItems = result.Items.Skip(Constants.DefaultPretestItemsToTakeFirst).ToList();

            int pretestItemsCount = lastEightItems.Count(item => item.ItemType == ItemTypeEnum.Pretest);
            Assert.That(pretestItemsCount, Is.EqualTo(Constants.DefaultPretestItemsToTakeFirst));

            int operationalItemsCount = lastEightItems.Count(item => item.ItemType == ItemTypeEnum.Operational);
            Assert.That(operationalItemsCount, Is.EqualTo(Constants.DefaultOperationalItemsCount));
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