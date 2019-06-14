using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CambiaUnaVida.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este explorador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        //Iván: Agrego las propiedades de mis usuarios del sistema       
        [Display(Name = "Rol")]
        [StringLength(20)]
        public string RolesUsuario { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(20)]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [StringLength(30)]
        public string Apellidos { get; set; }

        [Display(Name = "Direccion")]
        [StringLength(80)]
        public string Direccion { get; set; }

        [Display(Name = "Telefono")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Display(Name = "Sexo")]
        [StringLength(20)]
        public string Sexo { get; set; }

        [Display(Name = "Edad")]        
        public int Edad { get; set; }

        [Display(Name = "Ocupacion")]
        [StringLength(30)]
        public string Ocupacion { get; set; }

        [Display(Name = "NombreReferencia")]
        [StringLength(20)]
        public string NombreReferencia { get; set; }

        [Display(Name = "ApellidosReferencia")]
        [StringLength(30)]
        public string ApellidosReferencia { get; set; }

        [Display(Name = "TelefonoReferencia")]
        [StringLength(20)]
        public string TelefonoReferencia { get; set; }

        [Display(Name = "DireccionReferencia")]
        [StringLength(80)]
        public string DireccionReferencia { get; set; }

        [Display(Name = "EmailReferencia")]
        [StringLength(30)]
        public string EmailReferencia { get; set; }

        [Display(Name = "CedulaProfesional")]
        [StringLength(30)]
        public string CedulaProfesional { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
