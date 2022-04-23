using Testlet.Models;
using Testlet.RandomizationStrategies;

namespace Testlet
{
    public interface ITestletRandomizer
    {
        ITestletModel Randomize(ITestletModel model, IRandomizationStrategy strategy = null);
    }
}