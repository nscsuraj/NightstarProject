using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Core.Validation
{
    /// <summary>
    /// Ensures a maxlength attribute gets placed on views bound to 
    /// view models.
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class NumberStringLengthAttribute : ValidationAttribute
    {
        public int MaximumLength { get; set; }

        public NumberStringLengthAttribute(int maximumLength)
        {
            MaximumLength = maximumLength;
        }

        /// <summary>
        /// Determines whether the specified value of the object is valid. 
        /// </summary>
        /// <returns>
        /// true if the specified value is valid; otherwise, false. 
        /// </returns>
        /// <param name="value">The value of the specified validation object on which the <see cref="T:System.ComponentModel.DataAnnotations.ValidationAttribute"/> is declared.
        /// </param>
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
