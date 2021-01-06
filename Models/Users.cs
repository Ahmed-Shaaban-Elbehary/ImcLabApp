using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImcLabApp.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك ادخل اسم المستخدم")]
        [DisplayName("إسم المستخدم")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك أدخل الرقم السري")]
        [DataType(DataType.Password)]
        [DisplayName("الرقم السري")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " من فضلك إختر القسم المختص ")]
        [DisplayName(" القسم المختص")]
        public string Departments { get; set; }

    }
}