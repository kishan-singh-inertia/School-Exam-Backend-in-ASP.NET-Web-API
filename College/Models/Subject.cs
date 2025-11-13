using System.ComponentModel.DataAnnotations;

namespace College.Models
{
    public class Subject
    {
        [Key]
        public string SubCode { get; set; }   

        public string SubTitle { get; set; }  
        public int FullMarks { get; set; }   
    }
}
