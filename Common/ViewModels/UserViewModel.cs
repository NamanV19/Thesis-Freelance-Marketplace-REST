using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
    }
}
