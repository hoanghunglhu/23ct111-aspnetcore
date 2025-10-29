using System;

namespace StudentClassApi.Dtos
{
    public class CreateStudentDto
    {
        public required string MSSV { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        public required DateOnly DateOfBirth { get; set; }
        public required int ClassId { get; set; }
    }
}
