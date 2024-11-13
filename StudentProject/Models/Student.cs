using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    [Table("tbl_student")]
    public class Student
    {
        

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public String? Name { get; set; }
        [Column("email")]
        public string? Email { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber {  get; set; }
    }
}
