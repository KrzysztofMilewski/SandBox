using System.ComponentModel.DataAnnotations;

namespace SandBox.ViewModels
{
    public class PostFormViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Contents")]
        public string Contents { get; set; }

        public string ActionName { get; set; }

        public string PageHeading { get; set; }

        public string ErrorMessage { get; set; }

        public bool PubliclyVisible { get; set; }
    }
}