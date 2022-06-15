using System;
using System.Collections.Generic;
using System.Text;

namespace Common.NavModels
{
    public class UserNavModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Location { get; set; }
    }
}
