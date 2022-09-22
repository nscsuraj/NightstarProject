using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PartnerPortal.Core.Attributes
{
    /// <summary>
    /// Conditional Validation Attribute Class.
    /// <remarks>
    /// Date            Developer               Description.
    /// 06/11/2014      Dwarika                 Created.
    /// </remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ConditionalValidationAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Member Variables.
        /// </summary>
        protected readonly ValidationAttribute InnerAttribute;
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        protected abstract string ValidationName { get; }

        /// <summary>
        /// Gets the extra validation parameters.
        /// </summary>
        /// <returns></returns>
        protected virtual IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalValidationAttribute" /> class.
        /// </summary>
        /// <param name="innerAttribute">The inner attribute.</param>
        /// <param name="dependentProperty">The dependent property.</param>
        protected ConditionalValidationAttribute(ValidationAttribute innerAttribute, string dependentProperty)
        {
            this.InnerAttribute = innerAttribute;
            this.DependentProperty = dependentProperty;           
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);
            if (field != null)
            {
                // get the value of the dependent property
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                // compare the value against the target value
                if (dependentvalue != null)
                {
                    // match => means we should try validating this field
                    if (!InnerAttribute.IsValid(value))
                    {
                        // validation failed - return an error
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// When implemented in a class, returns client validation rules for that class.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = ValidationName,
            };
            string depProp = BuildDependentPropertyId(metadata, context as ViewContext);
            // find the value on the control we depend on; if it's a bool, format it javascript style
            //string targetValue = (this.TargetValue ?? "").ToString();
            //if (this.TargetValue.GetType() == typeof(bool))
            //{
            //    targetValue = targetValue.ToLower();
            //}
            rule.ValidationParameters.Add("dependentproperty", depProp);
           // rule.ValidationParameters.Add("targetvalue", targetValue);
            // Add the extra params, if any
            foreach (var param in GetExtraValidationParameters())
            {
                rule.ValidationParameters.Add(param);
            }
            yield return rule;
        }

        /// <summary>
        /// Builds the dependent property identifier.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="viewContext">The view context.</param>
        /// <returns></returns>
        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.DependentProperty);
            // This will have the name of the current field appended to the beginning, because the TemplateInfo's context has had this fieldname appended to it.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
            {
                depProp = depProp.Substring(thisField.Length);
            }
            return depProp;
        }
    }
}
