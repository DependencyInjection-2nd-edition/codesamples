using System;
using System.ComponentModel.DataAnnotations;

namespace Ploeh.Samples.Commerce.Domain.Commands
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredGuidAttribute : RequiredAttribute
    {
        public override bool IsValid(object value) => value != null && value is Guid id && id != Guid.Empty;
    }
}