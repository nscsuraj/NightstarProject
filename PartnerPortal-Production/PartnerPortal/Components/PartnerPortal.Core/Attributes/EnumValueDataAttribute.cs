using System;
using System.ComponentModel;

namespace PartnerPortal.Core.Attributes
{
    /// <summary>
    /// String value attribute
    /// </summary>
    /// <remarks>
    ///    Date            Developer   Description
    ///    07/04/2013      Dwarika     Created   
    /// </remarks>
    
    public class EnumValueDataAttribute : Attribute
    {
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string[] ProcedureInformation { get; set; }  

    }
}
