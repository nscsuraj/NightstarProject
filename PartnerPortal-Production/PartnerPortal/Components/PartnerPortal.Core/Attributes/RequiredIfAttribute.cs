using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace PartnerPortal.Core.Attributes
{
    /// <summary>
    /// Required If Attribute Class.
    /// <remarks>
    /// Date            Developer               Description
    /// 06/11/2014      Dwarika                 Created.
    /// </remarks>
    /// </summary>
    public class RequiredIfAttribute : ConditionalValidationAttribute
    {
        /// <summary>
        /// Gets the name of the validation.
        /// </summary>
        /// <value>
        /// The name of the validation.
        /// </value>
        protected override string ValidationName
        {
            get { return "requiredif"; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="dependentProperty">The dependent property.</param>
        /// <param name="targetValue">The target value.</param>
        public RequiredIfAttribute(string dependentProperty)
            : base(new RequiredAttribute(), dependentProperty)
        {
        }

        /// <summary>
        /// Gets the extra validation parameters.
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object> 
            { 
                { "rule", "required" }
            };
        }
    }
}
