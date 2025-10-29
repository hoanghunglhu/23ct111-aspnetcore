using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentClassApi.Entity;         // Sửa đúng namespace Entity
using StudentClassApi.Models;
using StudentClassApi.Dtos;         // Sửa đúng namespace DTO
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentClassApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents(int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Students.AsQueryable();

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var students = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            return Ok(studentDtos);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent([FromBody] CreateStudentDto createDto)
        {
            var student = _mapper.Map<Student>(createDto);

            var classExists = await _context.Classes.AnyAsync(c => c.Id == createDto.ClassId);
            if (!classExists)
                return BadRequest("Class not found");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var studentDto = _mapper.Map<StudentDto>(student);
            return CreatedAtAction(nameof(GetStudents), new { id = studentDto.Id }, studentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentDto studentDto)
        {
            if (id != studentDto.Id)
                return BadRequest();

            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            // Không cho đổi ClassId
            student.Name = studentDto.Name;
            student.DateOfBirth = studentDto.DateOfBirth;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

