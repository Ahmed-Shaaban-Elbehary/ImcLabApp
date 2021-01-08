using System.ComponentModel.DataAnnotations;

namespace ImcLabApp.Models
{
    public class PatientsRegisteration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "من فضلك ادخل اسم المستخدم")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "من فضلك أدخل الرقم السري")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}