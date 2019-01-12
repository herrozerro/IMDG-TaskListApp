using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IMDG_TaskListApp.Models
{
    public class TaskList
    {
        public int TaskId {get;set;}
        public string Name {get;set;}
        public List<TaskItem> Items {get;set;}
    }
}