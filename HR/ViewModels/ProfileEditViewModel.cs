using System.ComponentModel.DataAnnotations;

namespace Form.ViewModels
{
    public class ProfileEditViewModel
    {
        //[Required(ErrorMessageResourceName = "Error", ErrorMessageResourceType = typeof(Resource1))]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        //[Required(ErrorMessageResourceName = "Error", ErrorMessageResourceType = typeof(Resource1))]
        [EmailAddress]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور القديمة")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "كلمة المرور يجب أن تتجاوز ال 6 أحرف", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessage = "عدم تطابق كلمة المرور الجديدة وتأكيدها")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "صورة المستخدم")]
        public string ImagePath { get; set; }
    }
}