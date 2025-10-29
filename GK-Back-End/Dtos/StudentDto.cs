namespace StudentClassApi.Dtos
{
    public class StudentDto
    {
        public required int Id { get; set; }
        public string MSSV { get; set; } = string.Empty;
        public required string Name { get; set; } = string.Empty;
        public required DateOnly DateOfBirth { get; set; }
        public required int ClassId { get; set; }
    }
}
