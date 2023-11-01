using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Dtos;
using WebApplication7.Entities;

namespace WebApplication7.Controllers
{
    [ApiController]
    [Route("api/new-student-wf-4")]
    public class AddNewStudentWf4Controller : ControllerBase
    {
        private readonly MnccContext _context;
        public AddNewStudentWf4Controller(MnccContext context)
        {
            _context = context;
        }

        [HttpGet("init")]
        public IActionResult Init(int id, string name)
        {
            var student = new Student { Id = id, Name = name };
            _context.Students.Add(student);

            var wf = WorkflowInstance.Init("hassan", "fftsikj", id, WfType.PrepareStudent);

            _context.WorkflowInstances.Add(wf);
            _context.SaveChanges();

            return Ok(new
            {
                wfStatus = Enum.GetName(wf.Status),
                wfCurrentStep = Enum.GetName(wf.CurrentStep),
                tasks = wf
                        .UserTasks
                        .Select(t =>
                            new
                            {
                                wfId = wf.Id,
                                taskId = t.Id,
                                desicion = t.Desicion
                            })
                        .ToList()
            });
        }


        [HttpPost("steps")]
        public IActionResult Step2([FromBody] StudentTaskCommand command)
        {
            var task = _context.UserTasks.FirstOrDefault(c => c.Id == command.TaskId);
            var wf = _context.WorkflowInstances.Include(c => c.UserTasks).FirstOrDefault(c => c.Id == command.WfId);

            wf.MoveNext(task, command.Desicion, command.Data);
            _context.SaveChanges();

            return Ok(new
            {
                wfStatus = Enum.GetName<WfStatus>(wf.Status),
                wfCurrentStep = Enum.GetName<WfStep>(wf.CurrentStep),
                tasks = wf
                        .UserTasks
                        .Select(t =>
                            new
                            {
                                wfId = wf.Id,
                                taskId = t.Id,
                                desicion = t.Desicion
                            })
                        .ToList()
            });
        }
    }
}
