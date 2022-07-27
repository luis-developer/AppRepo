using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternshipProj.Models
{
    public class ClientModel
    {
        [Required(ErrorMessage = "ID e klientit eshte nje fushe e detyrueshme")]
        [Display(Name = "Client ID")]
        [StringLength(maximumLength: 5, MinimumLength = 5,
        ErrorMessage = "ID e klientin permban vetem 5 karaktere")]
        public string ClientId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

    }
}