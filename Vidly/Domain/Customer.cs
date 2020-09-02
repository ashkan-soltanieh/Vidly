using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the Name.")]
        [StringLength(255)]
        public string Name { get; set; }

        
        [Display(Name = "Date of Birth")]
        [Min18YearsIfACustomer]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; } //Navigation 

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}
