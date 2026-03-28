using System.ComponentModel.DataAnnotations;

namespace InnovaCore.ViewModels
{
    public class ViewModelLogin
    {
       

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; }

        public bool rememberme { get; set; }

    }
}
