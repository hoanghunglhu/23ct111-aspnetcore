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
    public class ClassesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClassesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classes = await _context.Classes.ToListAsync();
            return Ok(_mapper.Map<List<ClassDto>>(classes));
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass([FromBody] ClassDto classDto)
        {
            var newClass = _mapper.Map<Class>(classDto);
            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClasses), new { id = newClass.Id }, _mapper.Map<ClassDto>(newClass));
        }

        [HttpGet("{classId}/students")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsByClass(int classId)
        {
            var students = await _context.Students
                .Where(s => s.ClassId == classId)
                .ToListAsync();

            return Ok(_mapper.Map<List<StudentDto>>(students));
        }
    }
}



