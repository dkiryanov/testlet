using System.Collections.Generic;

namespace Testlet.Models
{
    public class TestletModel : ITestletModel
    {
        public int PretestItemsToTakeFirst => Constants.DefaultPretestItemsToTakeFirst;

        public int PretestItemsCount => Constants.DefaultPretestItemsCount;

        public int OperationalItemsCount => Constants.DefaultOperationalItemsCount;

        public string TestletId { get; set; }

        public IEnumerable<Item> Items { get; set; }
    }
}