using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace College.Models
{
    public class Marks
    {
        [Key]
        public int MarksId { get; set; }

        [ForeignKey("CandidateExam")]
        public string CRollNo { get; set; }

        [ForeignKey("Subject")]
        public string SubCode { get; set; }

        public decimal MarksObtained { get; set; }

        public CandidateExam CandidateExam { get; set; }
        public Subject Subject { get; set; }
    }
}
