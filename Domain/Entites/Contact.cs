using Shared.Dtos.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public string UserId { get; set; }  // Foreign Key

        public ApplicationUser User { get; set; }
    }

}