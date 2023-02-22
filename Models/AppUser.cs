using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BackFinal.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
