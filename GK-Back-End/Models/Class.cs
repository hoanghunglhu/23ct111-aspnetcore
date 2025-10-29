using System.Collections.Generic;

namespace StudentClassApi.Models
{
    public class Class
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
