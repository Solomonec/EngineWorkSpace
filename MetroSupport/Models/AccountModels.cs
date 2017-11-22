using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using MetroSupport.ViewModels;

namespace MetroSupport.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("MetroAccountConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public string Slugba { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        
    }

   public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "*")]
        [StringLength(25, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
        [Display(Name = "Должность")]
        public string Job { get; set; }
        [Display(Name = "Служба")]
        public string Slugba { get; set; }
         [Required(ErrorMessage = "*")]
        [Display(Name = "Отдел")]
        public string Department { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Вы неверно ввели почтовый адрес")]
        public string Email { get; set; }
      
        public List<RoleViewModel> Roles { get; set; }
    }

    public class ManageModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(25, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
        [Display(Name = "Должность")]
        public string Job { get; set; }
        [Display(Name = "Служба")]
        public string Slugba { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Отдел")]
        public string Department { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Вы неверно ввели почтовый адрес")]
        public string Email { get; set; }

        [Display(Name = "Тел")]
        public string Tel { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Активен")]
        public bool Active { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }

  
}
