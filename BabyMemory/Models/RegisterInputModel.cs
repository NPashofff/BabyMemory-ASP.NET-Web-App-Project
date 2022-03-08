namespace BabyMemory.Models
{
    using System.ComponentModel.DataAnnotations;
    using SharedTrip.Shared;

    public class RegisterInputModel
    {
        [Required]
        [StringLength(GlobalConstants.UserNameMAxLen,
            ErrorMessage = GlobalConstants.NameErrorMsg,
            MinimumLength = GlobalConstants.UserNameMinLen)]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(GlobalConstants.UserNameMAxLen,
            ErrorMessage = GlobalConstants.NameErrorMsg,
            MinimumLength = GlobalConstants.UserNameMinLen)]
        [Display(Name = "Full Name ex.: 'Jon Smith'")]
        public string UserFullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(GlobalConstants.PasswordMaxLen,
            ErrorMessage = GlobalConstants.NameErrorMsg,
            MinimumLength = GlobalConstants.PasswordMinLen)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = GlobalConstants.PasswordNotMach)]
        public string ConfirmPassword { get; set; }
    }
}
