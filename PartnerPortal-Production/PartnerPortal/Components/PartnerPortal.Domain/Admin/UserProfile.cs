using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Admin
{
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public String LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public String Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public String Phone { get; set; }

        /// <summary>
        /// Gets or sets the login identifier.
        /// </summary>
        /// <value>
        /// The login identifier.
        /// </value>
        public String LoginId { get; set; }

        /// <summary>
        /// Gets or sets the login password.
        /// </summary>
        /// <value>
        /// The login password.
        /// </value>
        public String LoginPassword { get; set; }

        public String Additional { get; set; }

        public int? UserLevel { get; set; } 
    }
}
