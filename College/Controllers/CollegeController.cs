using College.Models;
using College.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace College.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly ExamDbContext _context;

        public CollegeController(ExamDbContext context)
        {
            _context = context;
        }

        [HttpGet("marks/{rollNo}")]
        public async Task<IActionResult> GetMarksByRollNo(string rollNo)
        {
            var candidate = await _context.CandidateExams
                .Include(c => c.Marks)
                    .ThenInclude(m => m.Subject)
                .FirstOrDefaultAsync(c => c.CRollNo == rollNo);

            if (candidate == null)
                return NotFound(new { Message = $"Candidate with Roll No {rollNo} not found." });

            var result = candidate.Marks.Select(m => new
            {
                candidate.CRollNo,
                candidate.CName,
                m.Subject.SubTitle,
                m.MarksObtained,
                m.Subject.FullMarks,
                m.SubCode
            });

            return Ok(result);
        }


        [HttpPost("marks")]
        public async Task<IActionResult> AddMark([FromBody] MarkInsertDto newMarkDto)
        {
            if (!await _context.CandidateExams.AnyAsync(c => c.CRollNo == newMarkDto.CRollNo))
                return BadRequest("Invalid Roll No.");

            if (!await _context.Subjects.AnyAsync(s => s.SubCode == newMarkDto.SubCode))
                return BadRequest("Invalid Subject Code.");

            var existingMark = await _context.Marks
                .FirstOrDefaultAsync(m => m.CRollNo == newMarkDto.CRollNo && m.SubCode == newMarkDto.SubCode);

            if (existingMark != null)
                return BadRequest("Mark for this Roll No and Subject already exists. Use PUT to update.");


            var newMark = new Marks
            {
                CRollNo = newMarkDto.CRollNo,
                SubCode = newMarkDto.SubCode,
                MarksObtained = newMarkDto.MarksObtained
            };

            _context.Marks.Add(newMark);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Mark inserted successfully." });
        }


        [HttpPut("marks/{rollNo}/{subCode}")]
        public async Task<IActionResult> UpdateMark(string rollNo, string subCode, [FromBody] decimal marksObtained)
        {
            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.CRollNo == rollNo && m.SubCode == subCode);

            if (mark == null)
                return NotFound("Mark entry not found.");

            mark.MarksObtained = marksObtained;
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Mark updated successfully." });
        }


        [HttpDelete("marks/{rollNo}/{subCode}")]
        public async Task<IActionResult> DeleteMark(string rollNo, string subCode)
        {
            var mark = await _context.Marks
                .FirstOrDefaultAsync(m => m.CRollNo == rollNo && m.SubCode == subCode);

            if (mark == null)
                return NotFound("Mark entry not found.");

            _context.Marks.Remove(mark);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Mark deleted successfully." });
        }
    }
}
