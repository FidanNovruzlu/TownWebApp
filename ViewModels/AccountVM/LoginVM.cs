using System.ComponentModel.DataAnnotations;

namespace TownWebApp.ViewModels.AccountVM
{
    public class LoginVM
    {
        public string UserName { get; set; } = null!;
        [DataType(DataType.Password), MinLength(8), Required]
        public string Password { get; set; } = null!;
    }
}
