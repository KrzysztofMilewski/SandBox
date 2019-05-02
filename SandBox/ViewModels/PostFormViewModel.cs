using System.ComponentModel.DataAnnotations;

namespace SandBox.ViewModels
{
    public class PostFormViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Treść")]
        public string Contents { get; set; }

        public string ActionName { get; set; }

        public string PageHeading { get; set; }

        public string ErrorMessage { get; set; }
    }
}