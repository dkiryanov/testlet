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

            List<Item> firstPart = GetFirstPart(model, randomizationStrategy);
            List<Item> secondPart = GetSecondPart(model, firstPart, randomizationStrategy);

            firstPart.AddRange(secondPart);
            model.Items = firstPart;

            return model;
        }

        private List<Item> GetFirstPart(ITestletModel model, IRandomizationStrategy strategy)
        {
            IEnumerable<Item> items = model?.Items?.Where(item => item.ItemType == ItemTypeEnum.Pretest) ?? throw new ArgumentNullException(nameof(model.Items));

            return strategy.Randomize(items);
        }

        private List<Item> GetSecondPart(ITestletModel model, List<Item> pretestItemsToSkip, IRandomizationStrategy strategy)
        {
            IEnumerable<Item> items = model?.Items?.Where(item => !pretestItemsToSkip.Contains(item)) ?? throw new ArgumentNullException(nameof(model.Items));

            return strategy.Randomize(items);
        }
    }
}