using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StartMvc.Models
{
    public class TodoItem{
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}