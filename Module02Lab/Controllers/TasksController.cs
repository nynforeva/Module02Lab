using Microsoft.AspNetCore.Mvc;
using Module02Lab.Models;

namespace Module02Lab.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> tasks = new();

        // GET: api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll()
        {
            return Ok(tasks);
        }

        // GET: api/tasks/1
        [HttpGet("{id:int}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem task)
        {
            if (task.Id <= 0 || string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest();
            }

            if (tasks.Any(t => t.Id == task.Id))
            {
                return BadRequest();
            }

            tasks.Add(task);

            return CreatedAtAction(
                nameof(GetById),
                new { id = task.Id },
                task
            );
        }

        // PUT: api/tasks/1
        [HttpPut("{id:int}")]
        public ActionResult<TaskItem> Update(int id, TaskItem updatedTask)
        {
            if (id != updatedTask.Id ||
                string.IsNullOrWhiteSpace(updatedTask.Title))
            {
                return BadRequest();
            }

            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.IsCompleted = updatedTask.IsCompleted;

            return Ok(task);
        }

        // DELETE: api/tasks/1
        [HttpDelete("{id:int}")]
        public ActionResult<TaskItem> Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            tasks.Remove(task);

            return Ok(task);
        }
    }
}