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

        
        [HttpGet]
        public async Task<ActionResult<List<TaskList>>> GetTaskLists()
        {
            var lists = await _taskListContext.TaskLists
                                                .Include(x=>x.Items)
                                                .ToListAsync();

            return Ok(lists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskList>> GetTaskList(int id)
        {
            var list = await _taskListContext.TaskLists
                                                .Include(x=>x.Items)
                                                .FirstOrDefaultAsync(x=>x.TaskId == id);

            return Ok(list);
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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
    }

}