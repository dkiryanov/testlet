using System;
using System.Collections.Generic;
using System.Linq;

namespace Testlet.RandomizationStrategies
{
    public class DefaultRandomizationStrategy : IRandomizationStrategy
    {
        public List<Item> Randomize(IEnumerable<Item> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.OrderBy(item => Guid.NewGuid()).ToList();
        }
    }
}
