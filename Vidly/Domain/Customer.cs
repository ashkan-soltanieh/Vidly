using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Range(typeof(DateTime), "1900-01-01", "2099-12-31")]
        [Display(Name = "Date of Birth")]
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; } //Navigation Property
        public byte MembershipTypeId { get; set; }
    }
}
