using Common.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public Guid? MangaId { get; set; }

        [MinLength(ValidationConstant.CommentMinLength)]
        [MaxLength(ValidationConstant.CommentMaxLength)]
        [Required]
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("ParentComment")]
        public int? ParentCommentId { get; set; }

        #region Relations

        public User User { get; set; }
        public Manga Manga { get; set; }
        public Comment ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }

        #endregion
    }
}
