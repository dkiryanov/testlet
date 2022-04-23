using System;
using System.Linq;
using Testlet.Exceptions;
using Testlet.Models;

namespace Testlet.Validation
{
    public class TestletModelValidator : ITestletModelValidator
    {
        public void Validate(ITestletModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.TestletId))
            {
                throw new ArgumentNullException(nameof(model.TestletId));
            }

            if (model.Items == null)
            {
                throw new ArgumentNullException(nameof(model.Items));
            }

            int pretestItemsCount = model.Items.Count(item => item.ItemType == ItemTypes.Pretest);

            if (pretestItemsCount != model.PretestItemsCount)
            {
                throw new TestletValidationException($"Pretest items count should be {model.PretestItemsCount}");
            }

            int operationalCount = model.Items.Count(item => item.ItemType == ItemTypes.Operational);

            if (operationalCount != model.OperationalItemsCount)
            {
                throw new TestletValidationException($"Operational items count should be {model.OperationalItemsCount}");
            }
        }
    }
}
