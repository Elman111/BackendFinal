using BackFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace BackFinal.ViewModels
{
    public class ForgetPasswordVM
    {
        

        public AppUser AppUser { get; set; }
        public string Token { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
