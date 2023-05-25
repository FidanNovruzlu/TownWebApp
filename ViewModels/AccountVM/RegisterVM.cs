using System.ComponentModel.DataAnnotations;

namespace TownWebApp.ViewModels.AccountVM
{
    public class RegisterVM
    {
        public string Name { get; set; } = null!;
        [EmailAddress,Required]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password),MinLength(8),Required]
        public string Password { get; set; } = null!;
        public string Surname { get; set; } = null!;
        [DataType(DataType.Password), MinLength(8), Required,Compare(nameof(Password))]
        public string ConfrimPassword { get; set; } = null!;
        [MaxLength(20),Required]
        public string UserName { get; set; } = null!;
    }
}
