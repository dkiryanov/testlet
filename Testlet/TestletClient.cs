using System.Collections.Generic;
using Testlet.Models;
using Testlet.RandomizationStrategies;
using Testlet.Validation;

namespace Testlet
{
    public class TestletClient : ITestletClient
    {
        private readonly ITestletModelValidator _validator;
        private readonly ITestletRandomizer _randomizer;
        private readonly IRandomizationStrategy _randomizationStrategy;

        public TestletClient(
            ITestletModelValidator validator, 
            ITestletRandomizer randomizer, 
            IRandomizationStrategy randomizationStrategy = null)
        {
            _validator = validator;
            _randomizer = randomizer;
            _randomizationStrategy = randomizationStrategy;
        }

        public ITestletModel CreateTestlet(string testletId, IList<Item> items)
        {
            ITestletModel testlet = new TestletModel()
            {
                TestletId = testletId,
                Items = items
            };

            _validator.Validate(testlet);

           return _randomizer.Randomize(testlet, _randomizationStrategy);
        }
    }
}