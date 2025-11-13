using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class CandidateExam
    {
        [Key]
        public string CRollNo { get; set; } 

        public string CName { get; set; }    

        public ICollection<Marks> Marks { get; set; } = new List<Marks>();
    }
}
