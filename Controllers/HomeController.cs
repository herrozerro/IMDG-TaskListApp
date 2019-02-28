using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IMDG_TaskListApp.Models;
using Microsoft.EntityFrameworkCore;
using IMDG_TaskListApp.Repos;

namespace IMDG_TaskListApp.Controllers
{
    public class HomeController : Controller
    {
        private TaskListContext _taskListContext = null;
        public HomeController(TaskListContext taskListContext)
        {
            _taskListContext = taskListContext;
        }

        public IActionResult Index()
        {
            _taskListContext.Database.Migrate();

            if(!_taskListContext.TaskLists.Any())
            {
                _taskListContext.TaskLists.Add(new TaskList(){Name = "test"});
                _taskListContext.SaveChanges();
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
