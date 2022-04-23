using System.Collections.Generic;

namespace Testlet.RandomizationStrategies
{
    public interface IRandomizationStrategy
    {
        IList<Item> Randomize(IList<Item> items);
    }
}