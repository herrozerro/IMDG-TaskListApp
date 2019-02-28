using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IMDG_TaskListApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDG_TaskListApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListApi : ControllerBase 
    {
        private Repos.TaskListContext _taskListContext = null;

        public TaskListApi(Repos.TaskListContext taskListContext)
        {
            _taskListContext = taskListContext;
        }

        
        [HttpGet("tasklists")]
        public async Task<ActionResult<List<TaskList>>> GetTaskLists()
        {
            var lists = await _taskListContext.TaskLists
                                                .Include(x=>x.Items)
                                                .ToListAsync();

            return Ok(lists);
        }

        [HttpGet("tasklists/{id}")]
        public async Task<ActionResult<TaskList>> GetTaskList(int id)
        {
            var list = await _taskListContext.TaskLists
                                                .Include(x=>x.Items)
                                                .FirstOrDefaultAsync(x=>x.TaskId == id);

            return Ok(list);
        }

        [HttpPost("tasklist")]
        public async Task<ActionResult> PostTaskList([FromBody] TaskList taskList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Error in model.");
            }

            await _taskListContext.TaskLists.AddAsync(taskList);

            await _taskListContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("tasklist/{id}")]
        public async Task<ActionResult> PutTaskList([FromBody] TaskList taskList)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Error in model.");
            }

            _taskListContext.Attach(taskList);

            await _taskListContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("taskitems")]
        public async Task<ActionResult<List<TaskItem>>> GetTaskItems()
        {
            var lists = await _taskListContext.TaskItems
                                                .Include(x=>x.TaskList)
                                                .ToListAsync();

            return Ok(lists);
        }

        [HttpGet("taskitem/{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var list = await _taskListContext.TaskItems
                                                .Include(x=>x.TaskList)
                                                .FirstOrDefaultAsync(x=>x.ItemId == id);

            return Ok(list);
        }

        [HttpPost("taskitem")]
        public async Task<ActionResult> PostTaskItem([FromBody] TaskItem taskItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Error in model.");
            }

            var task = new TaskItem();
            task.Completed = false;
            //task.CompletedDateTime = new DateTime();
            task.Description = taskItem.Description;
            task.TaskList = _taskListContext.TaskLists.FirstOrDefault();
            task.TaskId = task.TaskList.TaskId;
            

            await _taskListContext.TaskItems.AddAsync(task);

            await _taskListContext.SaveChangesAsync();

            return Ok(task);
        }

        [HttpPut("taskitem/{id}")]
        public async Task<ActionResult> PutTaskItem([FromBody] TaskItem taskItem)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Error in model.");
            }

            _taskListContext.Attach(taskItem);

            await _taskListContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("taskitem/{id}")]
        public async Task<ActionResult> DeleteTaskItem(int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Error in model.");
            }

            // Get the Task from the database.
            var task = _taskListContext.TaskItems.Where(x=>x.ItemId == id).FirstOrDefault();
            if (task != null)
            {
                _taskListContext.TaskItems.Remove(task);
                await _taskListContext.SaveChangesAsync();
            }

            return Ok();
        }
    }

}