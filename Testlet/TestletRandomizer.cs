using System;
using System.Collections.Generic;
using System.Linq;
using Testlet.Models;
using Testlet.RandomizationStrategies;

namespace Testlet
{
    public class TestletRandomizer : ITestletRandomizer
    {
        private readonly IRandomizationStrategy _randomizationStrategy;

        public TestletRandomizer()
        {
            _randomizationStrategy = new DefaultRandomizationStrategy();
        }

        public ITestletModel Randomize(ITestletModel model, IRandomizationStrategy strategy = null)
        {
            IRandomizationStrategy randomizationStrategy = strategy ?? _randomizationStrategy;

            IEnumerable<Item> firstPart = GetFirstPart(model, randomizationStrategy);
            IEnumerable<Item> secondPart = GetSecondPart(model, firstPart, randomizationStrategy);

            model.Items = firstPart.Concat(secondPart);

            return model;
        }

        private List<Item> GetFirstPart(ITestletModel model, IRandomizationStrategy strategy)
        {
            IEnumerable<Item> items = model?.Items?.Where(item => item.ItemType == ItemTypeEnum.Pretest)
                .Take(model.PretestItemsToTakeFirst) ?? throw new ArgumentNullException(nameof(model.Items));

            return strategy.Randomize(items);
        }

        private List<Item> GetSecondPart(ITestletModel model, IEnumerable<Item> pretestItemsToSkip, IRandomizationStrategy strategy)
        {
            IEnumerable<Item> items = model?.Items?.Where(item => !pretestItemsToSkip.Contains(item)) ?? throw new ArgumentNullException(nameof(model.Items));

            return strategy.Randomize(items);
        }
    }
}