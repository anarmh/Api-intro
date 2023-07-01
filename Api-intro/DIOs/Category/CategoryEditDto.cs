using System.ComponentModel.DataAnnotations;

namespace Api_intro.DIOs.Category
{
    public class CategoryEditDto
    {
        [Required]
        public string Name { get; set; }
    }
}
