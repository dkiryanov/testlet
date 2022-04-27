using System.Collections.Generic;

namespace Testlet.RandomizationStrategies
{
    public interface IRandomizationStrategy
    {
        List<Item> Randomize(IEnumerable<Item> items);
    }
}