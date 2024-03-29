﻿using System.Collections.Generic;

namespace Testlet.Models
{
    public interface ITestletModel
    {
        int PretestItemsToTakeFirst { get; }

        int PretestItemsCount { get; }

        int OperationalItemsCount { get; }

        string TestletId { get; set; }

        IEnumerable<Item> Items { get; set; }
    }
}