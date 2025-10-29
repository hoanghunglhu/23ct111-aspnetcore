using System;

namespace StudentClassApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string MSSV { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        public required DateOnly DateOfBirth { get; set; }

        public required int ClassId { get; set; }
        public required Class Class { get; set; }
    }
}
