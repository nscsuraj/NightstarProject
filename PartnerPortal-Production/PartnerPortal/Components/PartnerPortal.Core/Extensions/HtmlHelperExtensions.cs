using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using PartnerPortal.Core.Validation;


namespace System
{
    /// <summary>
    /// HTML Helper Extension Methods
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Creates the a password field using the inline style.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlinePasswordFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                              Expression<Func<TModel, TValue>>
                                                                                  expression,
                                                                              RouteValueDictionary htmlAttributes)
        {
            const string format = "<span class='inlineContent'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      html.PasswordFor(expression, htmlAttributes),
                                                      html.ValidationMessageFor(expression, "*")));
        }

        /// <summary>
        /// Creates the inline  Checkbox for model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlineCheckboxFor<TModel>(HtmlHelper<TModel> html,
                                                                      Expression<Func<TModel, bool>> expression,
                                                                      object htmlAttributes)
        {
            const string format = "<span class='inlineContent'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      html.CheckBoxFor(expression, htmlAttributes),
                                                      html.ValidationMessageFor(expression, "*")));
        }

        /// <summary>
        /// Create an inline datepicker for the given model/value.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The model expression.</param>
        /// <param name="htmlAttributes">Additional HTML attributes.</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlineDatePickerFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                                Expression<Func<TModel, TValue>>
                                                                                    expression,
                                                                                RouteValueDictionary htmlAttributes)
        {
            htmlAttributes.Add("class", string.Format("input-text {0}", "NHDDatePicker"));
            const string format = "<span class='inlineContent'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      html.TextBoxFor(expression, htmlAttributes),
                                                      html.ValidationMessageFor(expression, "*")));
        }

        /// <summary>
        /// Creates the inline drop down list. if displayOneItemAsLabel is set to true and the select list only has one value, the text will be displayed as a label instead
        /// of a dropdown. The value will be in a hidden field on the page. Since the label and the dropdown will have the same id, the value will be posted back to your model
        /// the same way the dropdown would have.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="displayOneItemAsLabel">Determines if the dropdown will be displayed as a label if only one value exists in the dropdown.</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlineDropDownListFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                                  Expression<Func<TModel, TValue>>
                                                                                      expression,
                                                                                  IEnumerable<SelectListItem> selectList,
                                                                                  string optionLabel,
                                                                                  object htmlAttributes,
                                                                                  bool displayOneItemAsLabel)
        {
            const string format = "<span class='inlineContent'>{0}{1}{2}</span>";

            object content = null;
            object hiddenField = null;
            if (selectList != null && selectList.Count() == 1 && displayOneItemAsLabel)
            {
                content = InlineContentLabel(html, "", selectList.First().Text);

                var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

                hiddenField = html.Hidden(metadata.PropertyName, selectList.First().Value,
                                          ((htmlAttributes == null) ? new { id = metadata.PropertyName } : htmlAttributes));
            }
            else
            {
                content = html.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
                hiddenField = null;
            }

            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      content, hiddenField));
        }

        /// <summary>
        /// Creates an inline label and displays the data value for a model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML atrributes.</param>
        /// <returns>Returns an HTML string for an inline label and field for the value of the model.</returns>
        internal static MvcHtmlString CreateInlineLabelFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                           Expression<Func<TModel, TValue>> expression,
                                                                           string spanCssClass,
                                                                           RouteValueDictionary htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            spanCssClass = spanCssClass ?? "inlineContent";

            var field = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            const string format = "<span class='{0}'>{1}<span class='noBold'>{2}</span></span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      spanCssClass,
                                                      html.LabelFor(expression),
                                                      field));
        }

        /// <summary>
        /// Creates the inline text box for model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="cssSpanClass">css class for span</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlineTextBoxFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                             Expression<Func<TModel, TValue>> expression,
                                                                             RouteValueDictionary htmlAttributes,
                                                                             string cssSpanClass,
                                                                             bool addValidationMessage)
        {
            const string format = "<span class='{3}'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      html.TextBoxFor(expression, htmlAttributes),
                                                      addValidationMessage? html.ValidationMessageFor(expression, "*"):html.Raw(""),
                                                      cssSpanClass));
        }

        /// <summary>
        /// Creates the inline editor for model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="cssSpanClass">css class for span</param>
        /// <returns></returns>
        internal static MvcHtmlString CreateInlineEditorFor<TModel, TValue>(HtmlHelper<TModel> html,
                                                                            Expression<Func<TModel, TValue>> expression,
                                                                            RouteValueDictionary htmlAttributes,
                                                                            string cssSpanClass)
        {
            const string format = "<span class='{3}'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(expression),
                                                      html.EditorFor(expression, htmlAttributes),
                                                      html.ValidationMessageFor(expression, "*"),
                                                      cssSpanClass));
        }


        /// <summary>
        /// Creates a CSS link string that is appended with a timestamp of the last modified time. 
        /// This will cause browsers to reload the css automatically if the file is changed.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="cssUrl">The CSS URL.</param>
        /// <returns></returns>
        public static MvcHtmlString CssVersioned(this HtmlHelper html, string cssUrl)
        {
            return
                MvcHtmlString.Create(String.Format("\n<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\"/>",
                                                   Versioned(cssUrl)));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a div for a Google Map.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="mapID">The map ID.</param>
        /// <returns>A div for a Google Map.</returns>
        public static MvcHtmlString GoogleMap(this HtmlHelper html, string mapID)
        {
            return MvcHtmlString.Create(String.Format("<div id={0} class='gmap'></div>", mapID));
        }

        /// <summary>
        /// Renders a tenant-specific image located on the filesystem according 
        /// to the following convention:
        /// 
        /// Content/_Tenants/{tenant}/img/{imageFileName}
        /// </summary>
        /// <param name="html">this</param>
        /// <param name="imageFileName">Name of image stored on the filesystem at Content/_Tenants/{tenant}/img/{imageFileName}.</param>
        /// <param name="attributes">Collection of KV pairs for adding attributes to the rendered HTML element.</param>
        /// <param name="defaultQuoteType">Optional argument: Type of quote in case you need to use ' over " (" is the default).</param>
        /// <returns>An HTML image tag referencing a relative path to a tenant-specific image file stored on the filesystem at Content/_Tenants/{tenant}/img/{imageFileName}</returns>
        public static HtmlString Img(this HtmlHelper html, string imageFileName, Dictionary<string, string> attributes,
                                     char defaultQuoteType = '"')
        {
            // Start the tag
            var imageTag = new StringBuilder();
            imageTag.AppendFormat("<img src=\"{0}\"", imageFileName);

            // Add any attributes
            foreach (var attribute in attributes)
            {
                imageTag.AppendFormat(" {0}={2}{1}{2}", attribute.Key, attribute.Value, defaultQuoteType);
            }

            // Finish the tag
            return new MvcHtmlString(imageTag.Append("/>").ToString());
        }

        /// <summary>
        /// Creates an inline datepicker/textbox for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expressions.</param>
        /// <param name="attributes">Special attributes on the HTML.</param>
        /// <returns>HTML to render an inline datepicker.</returns>
        public static MvcHtmlString InlineDatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                        Expression<Func<TModel, TValue>> expression,
                                                                        IDictionary<string, object> attributes = null)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return CreateInlineDatePickerFor(html, expression, htmlAttributes);
        }

        /// <summary>
        /// Generates an html password field for use on MVC views.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">extension method for HtmlHelper</param>
        /// <param name="expression">The expression that defines what field to display a password field for.</param>
        /// <returns></returns>
        public static MvcHtmlString InlinePasswordFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                      Expression<Func<TModel, TValue>> expression)
        {
            return html.InlinePasswordFor(expression, null);
        }
        /// <summary>
        /// Generates an html password field for use on MVC views.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">extension method for HtmlHelper</param>
        /// <param name="expression">The expression that defines what field to display a password field for.</param>
        /// <param name="attributes">The html attributes to be applied to the resultant html password field.</param>
        /// <returns></returns>
        public static MvcHtmlString InlinePasswordFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                      Expression<Func<TModel, TValue>> expression,
                                                                      IDictionary<string, object> attributes)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return CreateInlinePasswordFor(html, expression, htmlAttributes);
        }
        /// <summary>
        /// Creates an inline label including the data for the label.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns>Html string for the label and label data value.</returns>
        public static MvcHtmlString InlineLabelFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                   Expression<Func<TModel, TValue>> expression,
                                                                   string spanCssClass,
                                                                   IDictionary<string, object> attributes)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return CreateInlineLabelFor(html, expression, spanCssClass, htmlAttributes);
        }

        /// <summary>
        /// Creates an inline label including the data for the label.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>Html string for the label and label data value.</returns>
        public static MvcHtmlString InlineLabelFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                   Expression<Func<TModel, TValue>> expression)
        {
            return html.InlineLabelFor(expression, null, null);
        }

        /// <summary>
        /// Creates an inline label including the data for the label.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>Html string for the label and label data value.</returns>
        public static MvcHtmlString InlineLabelFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                   Expression<Func<TModel, TValue>> expression,
                                                                   string spanCssClass)
        {
            return html.InlineLabelFor(expression, spanCssClass, null);
        }

        /// <summary>
        /// Creates an inline text box for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                     Expression<Func<TModel, TValue>> expression)
        {
            return html.InlineTextBoxFor(expression, null);
        }

        /// <summary>
        /// Creates an inline text box for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                     Expression<Func<TModel, TValue>> expression,
                                                                     IDictionary<string, object> attributes)
        {
            return html.InlineTextBoxFor(expression, attributes, "inlineContent");
        }

        /// <summary>
        /// Creates an inline text box for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="cssSpanClass">The css class for span</param>
        /// <returns></returns>
        public static MvcHtmlString InlineTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                     Expression<Func<TModel, TValue>> expression,
                                                                     IDictionary<string, object> attributes,
                                                                     string cssSpanClass)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return html.InlineTextBoxFor(expression, htmlAttributes, cssSpanClass,false);
        }

        /// <summary>
        /// Creates an inline text box for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="cssSpanClass">The css class for span</param>
        /// <returns></returns>
        public static MvcHtmlString InlineTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                     Expression<Func<TModel, TValue>> expression,
                                                                     IDictionary<string, object> attributes,
                                                                     string cssSpanClass,
                                                                     bool addValidationMessage)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return CreateInlineTextBoxFor(html, expression, htmlAttributes, cssSpanClass, addValidationMessage);
        }

        /// <summary>
        /// Creates an inline editor for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                    Expression<Func<TModel, TValue>> expression)
        {
            return html.InlineEditorFor(expression, null);
        }

        /// <summary>
        /// Creates an inline editor for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                    Expression<Func<TModel, TValue>> expression,
                                                                    IDictionary<string, object> attributes)
        {
            return html.InlineEditorFor(expression, attributes, "inlineContent");
        }

        /// <summary>
        /// Creates an inline editor for the model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="cssSpanClass">The css class for span</param>
        /// <returns></returns>
        public static MvcHtmlString InlineEditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                    Expression<Func<TModel, TValue>> expression,
                                                                    IDictionary<string, object> attributes,
                                                                    string cssSpanClass)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException("The expression's Body property must be a MemberExpression.", "expression");

            var htmlAttributes = (attributes != null)
                                     ? new RouteValueDictionary(attributes)
                                     : new RouteValueDictionary();
            SetupMaxLengthAttribute(member, htmlAttributes);

            return CreateInlineEditorFor(html, expression, htmlAttributes, cssSpanClass);
        }

        /// <summary>
        /// Inline text box with drop down list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="textBoxExpression">The text box expression.</param>
        /// <param name="dropDownExpression">The drop down expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineTextBoxWithDropDownFor<TModel, TValue, TValue2>(this HtmlHelper<TModel> html,
                                                                                          Expression
                                                                                              <Func<TModel, TValue>>
                                                                                              textBoxExpression,
                                                                                          Expression
                                                                                              <Func<TModel, TValue2>>
                                                                                              dropDownExpression,
                                                                                          IEnumerable<SelectListItem>
                                                                                              selectList)
        {
            var textBoxMember = textBoxExpression.Body as MemberExpression;
            if (textBoxMember == null)
                throw new ArgumentException("The text box expression's Body property must be a MemberExpression.",
                                            "expression");
            var dropDownMember = dropDownExpression.Body as MemberExpression;
            if (dropDownMember == null)
                throw new ArgumentException("The text box expression's Body property must be a MemberExpression.",
                                            "expression");

            const string format = "<span class='inlineContent'>{0}{1}{2}</span>";
            return MvcHtmlString.Create(string.Format(format,
                                                      html.LabelFor(textBoxExpression),
                                                      html.TextBoxFor(textBoxExpression,
                                                                      new { @class = "inlineContent-with-ddl-textbox" }),
                                                      html.DropDownListFor(dropDownExpression, selectList,
                                                                           new
                                                                           {
                                                                               @class =
                                                                           "inlineContent-with-ddl-dropdown"
                                                                           })));
        }

        /// <summary>
        /// Creates an inline Checkbox.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineCheckboxFor<TModel>(this HtmlHelper<TModel> helper,
                                                              Expression<Func<TModel, bool>> expression)
        {
            return helper.InlineCheckboxFor(expression, null);
        }

        /// <summary>
        /// Creates an inline Checkbox.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineCheckboxFor<TModel>(this HtmlHelper<TModel> helper,
                                                              Expression<Func<TModel, bool>> expression,
                                                              object htmlAttributes)
        {
            return CreateInlineCheckboxFor(helper, expression, htmlAttributes);
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a span with the specified CSS class, enclosing a LabelFor the property that is 
        /// represented by the specified expression along with any control sent in by the user.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="cssSpanClass">The CSS class to be assigned to the span.</param>
        /// <param name="controlHtml">The control HTML.</param>
        /// <returns>A span with the given css class, enclosing a label and a user specified control for a given property.</returns>
        public static MvcHtmlString InlineContent<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                  Expression<Func<TModel, TValue>> expression,
                                                                  string cssSpanClass, MvcHtmlString controlHtml)
        {
            return MvcHtmlString.Create(String.Format("<span class='{0}'>{1}{2}</span>", cssSpanClass,
                                                      html.LabelFor(expression), controlHtml));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a span with the specified CSS class, enclosing a LabelFor the property that is 
        /// represented by the specified expression along with any control sent in by the user and a ValidationMessageFor the property 
        /// represented by the specified expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="cssSpanClass">The CSS class to be assigned to the span.</param>
        /// <param name="controlHtml">The control HTML.</param>
        /// <param name="validationMessage">The validation message.</param>
        /// <returns>A span with the given css class, enclosing a label, a user specified control, and a validation message for a specified property.</returns>
        public static MvcHtmlString InlineContent<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                  Expression<Func<TModel, TValue>> expression,
                                                                  string cssSpanClass, MvcHtmlString controlHtml,
                                                                  string validationMessage)
        {
            return MvcHtmlString.Create(String.Format("<span class='{0}'>{1}{2}{3}</span>", cssSpanClass,
                                                      html.LabelFor(expression), controlHtml,
                                                      html.ValidationMessageFor(expression, validationMessage)));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a label with the specified CSS class and text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="cssClassLabel">The label CSS class.</param>
        /// <param name="labelText">The label text.</param>
        /// <returns>A label with the specified class and text.</returns>
        public static MvcHtmlString InlineContentLabel(this HtmlHelper html, string cssClassLabel, string labelText)
        {
            return MvcHtmlString.Create(String.Format("<label class='{0}'>{1}</label>", cssClassLabel, labelText));
        }

        /// <summary>
        /// Creates an inline drop down list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                                                                          Expression<Func<TModel, TValue>> expression,
                                                                          IEnumerable<SelectListItem> selectList)
        {
            return helper.InlineDropDownListFor(expression, selectList, null, null, false);
        }

        /// <summary>
        /// Creates an inline drop down list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                                                                          Expression<Func<TModel, TValue>> expression,
                                                                          IEnumerable<SelectListItem> selectList,
                                                                          string optionLabel)
        {
            return helper.InlineDropDownListFor(expression, selectList, optionLabel, null, false);
        }


        /// <summary>
        /// Creates an inline drop down list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                                                                          Expression<Func<TModel, TValue>> expression,
                                                                          IEnumerable<SelectListItem> selectList,
                                                                          object htmlAttributes)
        {
            return helper.InlineDropDownListFor(expression, selectList, null, htmlAttributes, false);
        }

        /// <summary>
        /// Creates an inline drop down list.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="optionLabel">The option label.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static MvcHtmlString InlineDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
                                                                          Expression<Func<TModel, TValue>> expression,
                                                                          IEnumerable<SelectListItem> selectList,
                                                                          string optionLabel, object htmlAttributes,
                                                                          bool displayOneItemAsLabel)
        {
            return CreateInlineDropDownListFor(helper, expression, selectList, optionLabel, htmlAttributes,
                                               displayOneItemAsLabel);
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a label with the specified id, CSS class, and text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="labelID">The label ID.</param>
        /// <param name="cssClassLabel">The label CSS class.</param>
        /// <param name="labelText">The label text.</param>
        /// <returns>A label with the specified id, CSS class, and text.</returns>
        public static MvcHtmlString InlineContentLabel(this HtmlHelper html, string labelID, string cssClassLabel,
                                                       string labelText)
        {
            return
                MvcHtmlString.Create(String.Format("<label id='{0}' class='{1}'>{2}</label>", labelID, cssClassLabel,
                                                   labelText));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a span with the specified CSS class, enclosing a label with the specified label text
        /// and a second label with the specified id. This is to be used when two labels are needed. The first one just displays some given static text
        /// and the second needs to have an ID because it's text value will be dynamic.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="cssSpanClass">The CSS class to be assigned to the span.</param>
        /// <param name="leadingLabelText">The leading label text.</param>
        /// <param name="trailingLabelID">The trailing label ID.</param>
        /// <returns>A span with two labels.</returns>
        public static MvcHtmlString InlineContentReadonlyField(this HtmlHelper html, string cssSpanClass,
                                                               string leadingLabelText, string trailingLabelID)
        {
            return MvcHtmlString.Create(String.Format("<span class={0}>{1}<label id={2}></label></span>", cssSpanClass,
                                                      html.Label(leadingLabelText), trailingLabelID));
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor the property represented by the specified expression,
        /// along with any other user defined control and an only link. Ideally, this should be used on search pages.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="controlHtml">The user defined control HTML.</param>
        /// <param name="linkOnclickJS">The link onclick JavaScript.</param>
        /// <returns>A span that encloses a LabelFor, along with a user defined control and an only link.</returns>
        public static MvcHtmlString InlineContentSearch<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                        Expression<Func<TModel, TValue>> expression,
                                                                        MvcHtmlString controlHtml, string linkOnclickJS)
        {
            return InlineContentSearch(html, expression, controlHtml, linkOnclickJS, null, null);
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor the property represented by the specified expression,
        /// along with any other user defined control and an only link. Ideally, this should be used on search pages.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="controlHtml"></param>
        /// <param name="linkOnclickJS"></param>
        /// <param name="spanClass"></param>
        /// <param name="onlyLinkClass"></param>
        /// <returns></returns>
        public static MvcHtmlString InlineContentSearch<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                        Expression<Func<TModel, TValue>> expression,
                                                                        MvcHtmlString controlHtml, string linkOnclickJS,
                                                                        string spanClass, string onlyLinkClass)
        {
            return MvcHtmlString.Create(String.Format(
                "<span class=\"inlineContentWithOnly {0}\">{1}{2}<span tabindex=\"-1\" onclick=\"{3}\" class=\"inlineContentOnlyLink {4}\" data-name=\"{5}\">only</span></span>",
                spanClass,
                html.LabelFor(expression),
                controlHtml,
                linkOnclickJS,
                onlyLinkClass,
                html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression))));
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor the property represented by the specified expression,
        /// along with any other user defined control. Ideally, this should be used on search pages when an only link is not needed.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="controlHtml">The control HTML.</param>
        /// <returns>A span that encloses a LabelFor, along with a user defined control.</returns>
        public static MvcHtmlString InlineContentSearchNoOnlyLink<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                                  Expression<Func<TModel, TValue>>
                                                                                      expression,
                                                                                  MvcHtmlString controlHtml)
        {
            return
                MvcHtmlString.Create(
                    String.Format(
                        "<span class=\"inlineContentWithOnly\">{0}{1}<span tabindex=\"-1\">&nbsp</span></span>",
                        html.LabelFor(expression),
                        controlHtml));
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor and a TextBoxFor the property represented by 
        /// the specified expression, along with an only link. Ideally, this should be used on search pages.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="linkOnclickJS">The link onclick JavaScript.</param>
        /// <returns>A span that encloses a LabelFor, TextBoxFor, and only link.</returns>
        public static MvcHtmlString InlineContentSearchTextbox<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                               Expression<Func<TModel, TValue>>
                                                                                   expression, string linkOnclickJS)
        {
            return InlineContentSearchTextbox(html, expression, linkOnclickJS, null, null, null);
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor and a TextBoxFor the property represented by 
        /// the specified expression, along with an only link. Ideally, this should be used on search pages.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="linkOnclickJS"></param>
        /// <param name="spanClass"></param>
        /// <param name="textboxClass"></param>
        /// <param name="onlyLinkClass"></param>
        /// <returns></returns>
        public static MvcHtmlString InlineContentSearchTextbox<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                               Expression<Func<TModel, TValue>>
                                                                                   expression, string linkOnclickJS,
                                                                               string spanClass, string textboxClass,
                                                                               string onlyLinkClass)
        {
            return InlineContentSearchTextbox(html, expression, linkOnclickJS, spanClass, textboxClass, onlyLinkClass,
                                              null, false);
        }

        /// <summary>
        /// Creates a MvcHtmlString, containing a span that encloses a LabelFor and a TextBoxFor the property represented by 
        /// the specified expression, along with an only link. Ideally, this should be used on search pages.
        /// This method adds a validation message span which is used by jquery validator
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="html"></param>
        /// <param name="expression"></param>
        /// <param name="linkOnclickJS"></param>
        /// <param name="spanClass"></param>
        /// <param name="textboxClass"></param>
        /// <param name="onlyLinkClass"></param>
        /// <returns></returns>
        public static MvcHtmlString InlineContentSearchTextbox<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                               Expression<Func<TModel, TValue>>
                                                                                   expression, string linkOnclickJS,
                                                                               string spanClass, string textboxClass,
                                                                               string onlyLinkClass,
                                                                               IDictionary<string, object> attributes,
                                                                               bool addValidator)
        {
            if (addValidator)
            {
                var member = expression.Body as MemberExpression;
                if (member == null)
                    throw new ArgumentException("The expression's Body property must be a MemberExpression.",
                                                "expression");

                var htmlAttributes = (attributes != null)
                                         ? new RouteValueDictionary(attributes)
                                         : new RouteValueDictionary();
                SetupMaxLengthAttribute(member, htmlAttributes);

                htmlAttributes.Add("class", string.Format("input-text {0}", textboxClass));

                return MvcHtmlString.Create(String.Format(
                    "<span class=\"inlineContentWithOnly {0}\">{1}{2}<span tabindex=\"-1\" onclick=\"{3}\" data-name=\"{4}\" class=\"inlineContentOnlyLink {5}\">only</span></span>",
                    spanClass,
                    html.LabelFor(expression),
                    html.TextBoxFor(expression, htmlAttributes),
                    linkOnclickJS,
                    html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                        ExpressionHelper.GetExpressionText(expression)),
                    onlyLinkClass,
                    html.ValidationMessageFor(expression, "*")));
            }
            else
            {
                return MvcHtmlString.Create(String.Format(
                    "<span class=\"inlineContentWithOnly {0}\">{1}{2}<span tabindex=\"-1\" onclick=\"{3}\" data-name=\"{4}\" class=\"inlineContentOnlyLink {5}\">only</span></span>",
                    spanClass,
                    html.LabelFor(expression),
                    html.TextBoxFor(expression, new { @class = String.Format("input-text {0}", textboxClass) }),
                    linkOnclickJS,
                    html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                        ExpressionHelper.GetExpressionText(expression)),
                    onlyLinkClass));
            }
        }

        /// <summary>
        /// Generates a "Territory Codes" link and textbox to be used on MVC views. Created to support Travelers territories.
        /// The "Territory Codes" link launches the territory chooser.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>A "Territory Codes" chooser link and a textbox for entering territory codes.</returns>
        public static MvcHtmlString InlineContentTerritoryTextbox<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                                  Expression<Func<TModel, TValue>>
                                                                                      expression, string textboxID)
        {
            return MvcHtmlString.Create(String.Format(
                "<label class='fauxLink' onclick='Territories.launchTerritoryChooser(\"" + textboxID + "\"); "
                + "Territories.TerritoryChooser.fixOverlayPosition();'>Territory Codes</label>{0}{1}",
                html.TextBoxFor(expression, new { maxlength = 1000 }),
                html.ValidationMessageFor(expression, "*")
                                            ));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a span with the specified CSS class, enclosing a LabelFor and a TextBoxFor the property  
        /// represented by the specified time expression, along with a DropDownListFor the property represented by the specified AMPM
        /// expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="timeExpression">The time expression.</param>
        /// <param name="AMPMexpression">The AMPM expression.</param>
        /// <param name="cssSpanClass">The CSS class to be assigned to the span.</param>
        /// <returns>An inline textbox and AMPM dropdown list for entering time.</returns>
        public static MvcHtmlString InlineContentTimeTextbox<TModel, TValue>(this HtmlHelper<TModel> html,
                                                                             Expression<Func<TModel, TValue>>
                                                                                 timeExpression,
                                                                             Expression<Func<TModel, TValue>>
                                                                                 AMPMexpression, string cssSpanClass)
        {
            var AMPMItems = new List<SelectListItem>();
            AMPMItems.Add(new SelectListItem { Text = "AM", Value = "AM" });
            AMPMItems.Add(new SelectListItem { Text = "PM", Value = "PM" });

            return MvcHtmlString.Create(String.Format("<span class='{0}'>{1}{2}{3}</span>", cssSpanClass,
                                                      html.LabelFor(timeExpression),
                                                      html.TextBoxFor(timeExpression, new { maxlength = 5 }),
                                                      html.DropDownListFor(AMPMexpression, AMPMItems)));
        }

        /// <summary>
        /// Creates a JavaScript link string that is appended with a timestamp of the last modified time. 
        /// This will cause browsers to reload the JavaScript automatically if the file is changed.
        /// </summary>
        /// <param name="html">The HTML helper.</param>
        /// <param name="scriptUrl">The script URL.</param>
        /// <returns></returns>
        public static MvcHtmlString ScriptVersioned(this HtmlHelper html, string scriptUrl)
        {
            return
                MvcHtmlString.Create(String.Format("\n<script type=\"text/javascript\" src=\"{0}\"></script>",
                                                   Versioned(scriptUrl)));
        }

        /// <summary>
        /// Creates a div and span combination for jquery validation message
        /// </summary>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="message">message to display</param>
        /// <param name="htmlAttributes">error messages</param>
        /// <returns>a html string</returns>
        public static MvcHtmlString ValidationSummaryJQuery(this HtmlHelper htmlHelper, string message,
                                                            IDictionary<string, object> htmlAttributes)
        {
            if (!htmlHelper.ViewData.ModelState.IsValid)
                return htmlHelper.ValidationSummary(message, htmlAttributes);


            var sb = new StringBuilder(Environment.NewLine);

            var divBuilder = new TagBuilder("div");
            divBuilder.MergeAttributes(htmlAttributes);
            divBuilder.AddCssClass(HtmlHelper.ValidationSummaryValidCssClassName); // intentionally add VALID css class

            if (!string.IsNullOrEmpty(message))
            {
                //--------------------------------------------------------------------------------
                // Build an EMPTY error summary message <span> tag
                //--------------------------------------------------------------------------------
                var spanBuilder = new TagBuilder("span");
                spanBuilder.SetInnerText(message);
                sb.Append(spanBuilder.ToString(TagRenderMode.Normal)).Append(Environment.NewLine);
            }

            divBuilder.InnerHtml = sb.ToString();
            return MvcHtmlString.Create(divBuilder.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Returns an MvcHtmlString, containing a span with the specified CSS class and text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="cssClassLabel">The label CSS class.</param>
        /// <param name="labelText">The label text.</param>
        /// <returns>A label with the specified class and text.</returns>
        public static MvcHtmlString InlineContentSpan(this HtmlHelper html, string cssClassLabel, string formatter,
                                                      object spanText)
        {
            var valueString = string.IsNullOrEmpty(formatter) ? spanText.ToString() : string.Format(formatter, spanText);
            return MvcHtmlString.Create(String.Format("<span class='{0}'>{1}</span>", cssClassLabel, valueString));
        }

        /// <summary>
        /// Setups the max length attribute.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        internal static void SetupMaxLengthAttribute(MemberExpression member, RouteValueDictionary htmlAttributes)
        {
            if (member == null)
                throw new ArgumentException("member cannot be null.", "member");
            if (htmlAttributes == null)
                throw new ArgumentException("htmlAttributes cannot be null.", "htmlAttributes");

            var stringLength = member.Member
                                   .GetCustomAttributes(typeof(StringLengthAttribute), false)
                                   .FirstOrDefault() as StringLengthAttribute;
            if (stringLength != null)
            {
                htmlAttributes.Add("maxlength", stringLength.MaximumLength);
            }

            var numberLength = member.Member
                                   .GetCustomAttributes(typeof(NumberStringLengthAttribute), false)
                                   .FirstOrDefault() as NumberStringLengthAttribute;
            if (numberLength != null)
            {
                htmlAttributes.Add("maxlength", numberLength.MaximumLength);
            }
        }

        /// <summary>
        /// Converts DateTime to unix timestamp.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        /// <summary>
        /// Converts Unix timestamp to DateTime.
        /// </summary>
        /// <param name="unixTimestamp">The unix timestamp.</param>
        /// <returns></returns>
        private static DateTime UnixTimestampToDateTime(int unixTimestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixTimestamp);
        }

        /// <summary>
        /// Creates a string that is appended with a timestamp of the last modified time for a file. 
        /// This will cause browsers to reload the file automatically if the file is changed.
        /// </summary>
        /// <param name="url">The path to the file.</param>
        /// <returns>Unique string path to file</returns>
        public static String Versioned(string url)
        {
            var versioned = url;
            var serverPath = HttpContext.Current.Server.MapPath(url);
            DateTime? lastModified = null;
            try
            {
                lastModified = File.GetLastWriteTime(serverPath);
            }
            catch (Exception)
            {
            }
            // Some file paths are incorrect but still return a time stamp with year 1600.
            // Check for this and adjust path
            if (lastModified.Value.Year == 1600)
            {
                try
                {
                    lastModified = File.GetLastWriteTime(HttpContext.Current.Server.MapPath(@"..\" + url));
                }
                catch (Exception)
                {
                }
            }
            versioned += (lastModified.HasValue) ? "?" + DateTimeToUnixTimestamp(lastModified.Value) : "";
            return versioned;
        }
    }
}