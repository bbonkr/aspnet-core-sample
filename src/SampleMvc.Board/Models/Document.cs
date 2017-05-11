using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleMvc.Board.Models
{
    public class Document
    {

        public Document()
        {
            //var now = DateTime.Now;
            //PostDate = now;
            //ModifyDate = now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="번호")]
        public int Id { get; set; }

        [Required(ErrorMessage ="* 제목을 입력하세요.")]
        [StringLength(200, MinimumLength = 1)]
        [Display(Name="제목")]
        public string Title { get; set; }

        [Required(ErrorMessage ="* 내용을 입력하세요.")]
        [StringLength(4000, MinimumLength = 1)]
        [Display(Name="내용")]
        public string Content { get; set; }

        [Required(ErrorMessage = "* 성명을 입력하세요.")]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "작성자")]
        public string Name { get; set; }

        [Display(Name="작성시각")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode =true)]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; } 

        [Display(Name = "작성 IP Address")]
        public string PostIp { get; set; } 

        [Display(Name = "수정시각")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]        
        public DateTime ModifyDate { get; set; }

        [Display(Name = "수정 IP Address")]
        public string ModifyIp { get; set; }

        public int Ref { get; set; }

        public int Step { get; set; }

        public int RefOrder { get; set; }

        public int AnswerNum { get; set; }

        public int ParentNum { get; set; }

        public int ReadCount { get; set; }

        public int CommentCount { get; set; }
    }
}
