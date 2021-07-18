namespace ContractManager.ViewModels.Paragraphs
{
    using System.ComponentModel.DataAnnotations;

    public class ParagraphVM
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Content")]
        [MaxLength(1000, ErrorMessage = "The maximum size of content is {1}")]
        public string Content { get; set; }
    }
}
