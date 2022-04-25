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

            IList<Item> pretestItems = GetPretestItems(model);
            IList<Item> operationalItems = GetOperationalItems(model);

            IList<Item> randomizedPretestItems = randomizationStrategy.Randomize(pretestItems);

            randomizedPretestItems.Skip(model.PretestItemsToTakeFirst).ToList().ForEach(item => operationalItems.Add(item));

            List<Item> randomizedItems = randomizedPretestItems.Take(model.PretestItemsToTakeFirst).ToList();
            randomizationStrategy.Randomize(operationalItems).ToList().ForEach(item => randomizedItems.Add(item));

            model.Items = randomizedItems;

            return model;
        }

        private IList<Item> GetPretestItems(ITestletModel model)
        {
            return model?.Items?.Where(item => item.ItemType == ItemTypeEnum.Pretest).ToList() ?? throw new ArgumentNullException(nameof(model.Items));
        }

        private IList<Item> GetOperationalItems(ITestletModel model)
        {
            return model?.Items?.Where(item => item.ItemType == ItemTypeEnum.Operational).ToList() ?? throw new ArgumentNullException(nameof(model.Items));
        }
    }
}