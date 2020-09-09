using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovCustMVCAppWithAuthen.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Customer Name is Mandatory")]
        [StringLength(40,ErrorMessage ="Customer name must be of 40")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [Min18YrsIfMember]
        public DateTime BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        public MembershipType MembershipType { get; set; }
        
        public int MembershipTypeId { get; set; }
    }
}