using System.Collections.Generic;
using Testlet.Models;

namespace Testlet
{
    public interface ITestletClient
    {
        ITestletModel CreateTestlet(string testletId, IList<Item> items); 


    }
}