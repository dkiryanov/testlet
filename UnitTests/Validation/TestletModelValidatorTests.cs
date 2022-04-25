using System;
using NUnit.Framework;
using Testlet.Exceptions;
using Testlet.Models;
using Testlet.Validation;

namespace UnitTests.Validation
{
    [TestFixture]
    public class TestletModelValidatorTests : TestBase
    {
        private ITestletModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new TestletModelValidator();
        }

        [TestCase("")]
        [TestCase(null)]
        public void Validate_TestletIdIsNullOrEmpty_ThrowsArgumentNullException(string testletId)
        {
            // Arrange
            ITestletModel testlet = new TestletModel()
            {
                TestletId = testletId,
                Items = GetTestletItems(Constants.DefaultPretestItemsCount, Constants.DefaultOperationalItemsCount)
            };

            // Assert
            Assert.Throws<ArgumentNullException>(() => _validator.Validate(testlet));
        }

        [Test]
        public void Validate_TestletIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITestletModel testlet = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() => _validator.Validate(testlet));
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(5)]
        public void Validate_TestletHasWrongNumberOfPretestItems_ThrowsTestletValidationException(int pretestItemsCount)
        {
            // Arrange
            ITestletModel testlet = GetTestletModel(pretestItemsCount, Constants.DefaultOperationalItemsCount);

            // Assert
            TestletValidationException ex = Assert.Throws<TestletValidationException>(() => _validator.Validate(testlet));
            Assert.That(ex.Message, Is.EqualTo($"Pretest items count should be {testlet.PretestItemsCount}"));
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(7)]
        public void Validate_TestletHasWrongNumberOfOperationalItems_ThrowsTestletValidationException(int operationalItemsCount)
        {
            // Arrange
            ITestletModel testlet = GetTestletModel(Constants.DefaultPretestItemsCount, operationalItemsCount);

            // Assert
            TestletValidationException ex = Assert.Throws<TestletValidationException>(() => _validator.Validate(testlet));
            Assert.That(ex.Message, Is.EqualTo($"Operational items count should be {testlet.OperationalItemsCount}"));
        }

        [Test]
        public void Validate_TestletItemsIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ITestletModel testlet = new TestletModel()
            {
                Items = null
            };

            // Assert
            Assert.Throws<ArgumentNullException>(() => _validator.Validate(testlet));
        }

        [Test]
        public void Validate_TestletIsWellFormed_DoesNotThrowException()
        {
            // Arrange
            ITestletModel testlet = GetTestletModel(Constants.DefaultPretestItemsCount, Constants.DefaultOperationalItemsCount);

            // Assert
            Assert.DoesNotThrow(() => _validator.Validate(testlet));
        }
    }
}