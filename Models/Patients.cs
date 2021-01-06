using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImcLabApp.Models
{
    public class Patients
    {

        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك أدخل الرقم الطبي")]
        [DisplayName("الرقم الطبي")]
        public string medicalNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك أدخل الرقم القومي أو الباسبور")]
        [DisplayName("الرقم القومي - باسبور")]
        public string NationalID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك أدخل اسم المريض")]
        [DisplayName("إسم المستخدم")]
        public string userName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "من فضلك أدخل رقم الموبيل")]
        [DisplayName("رقم هاتف المحمول")]
        [StringLength(20, ErrorMessage = "رقم المحمول لا يمكن أن يقل عن 20 رقم ")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; }
        public virtual ICollection<Labs> Tests { get; set; }
        public virtual ICollection<Radios> Radios { get; set; }
        public virtual ICollection<Tumors> Tumors { get; set; }
    }
}