using System.Collections.Generic;
using Testlet;
using Testlet.Models;

namespace UnitTests
{
    public abstract class TestBase
    {
        public virtual ITestletModel GetTestletModel(int pretestItemsCount, int operationalItemsCount)
        {
            return new TestletModel()
            {
                TestletId = "123",
                Items = GetTestletItems(pretestItemsCount, operationalItemsCount)
            };
        }

        public virtual IList<Item> GetTestletItems(int pretestItemsCount, int operationalItemsCount)
        {
            IList<Item> items = new List<Item>();

            for (int i = 0; i < operationalItemsCount; i++)
            {
                items.Add(new Item()
                {
                    ItemId = $"operational-{i}",
                    ItemType = ItemTypes.Operational
                });
            }

            for (int i = 0; i < pretestItemsCount; i++)
            {
                items.Add(new Item()
                {
                    ItemId = $"pretest-{i}",
                    ItemType = ItemTypes.Pretest
                });
            }

            return items;
        }
    }
}
