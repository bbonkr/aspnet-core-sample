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
        [Display(Name ="��ȣ")]
        public int Id { get; set; }

        [Required(ErrorMessage ="* ������ �Է��ϼ���.")]
        [StringLength(200, MinimumLength = 1)]
        [Display(Name="����")]
        public string Title { get; set; }

        [Required(ErrorMessage ="* ������ �Է��ϼ���.")]
        [StringLength(4000, MinimumLength = 1)]
        [Display(Name="����")]
        public string Content { get; set; }

        [Required(ErrorMessage = "* ������ �Է��ϼ���.")]
        [StringLength(30, MinimumLength = 2)]
        [Display(Name = "�ۼ���")]
        public string Name { get; set; }

        [Display(Name="�ۼ��ð�")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode =true)]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; } 

        [Display(Name = "�ۼ� IP Address")]
        public string PostIp { get; set; } 

        [Display(Name = "�����ð�")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]        
        public DateTime ModifyDate { get; set; }

        [Display(Name = "���� IP Address")]
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
