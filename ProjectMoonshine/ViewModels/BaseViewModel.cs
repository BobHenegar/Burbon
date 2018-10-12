using System.ComponentModel.DataAnnotations;

namespace ProjectMoonshine.ViewModels
{
    public class BaseViewModel
    {
        [Display(AutoGenerateField = false, Name = "RefAction")]
        public string RefAction { get; set; }
        [Display(AutoGenerateField = false, Name = "RefId")]
        public string RefId { get; set; }
        [Display(AutoGenerateField = false, Name = "RefController")]
        public string RefController { get; set; }
    }
}
