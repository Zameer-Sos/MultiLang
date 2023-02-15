using System.ComponentModel.DataAnnotations;

namespace MultiLang.Models
{
    public class Testing
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "This field is requried")]
        public string Name { get; set; }
    }
}
