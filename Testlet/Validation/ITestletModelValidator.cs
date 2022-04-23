using Testlet.Models;

namespace Testlet.Validation
{
    public interface ITestletModelValidator
    {
        void Validate(ITestletModel model);
    }
}