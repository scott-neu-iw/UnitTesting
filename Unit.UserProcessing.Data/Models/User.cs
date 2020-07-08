using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unit.UserProcessing.Data.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(500)]
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
