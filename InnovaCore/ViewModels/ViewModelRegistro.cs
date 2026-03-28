using System.ComponentModel.DataAnnotations;

namespace InnovaCore.ViewModels
{
    public class ViewModelRegistro
    {
        [Required(ErrorMessage = "O nome de usuario é obrigatório")]
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }
}
