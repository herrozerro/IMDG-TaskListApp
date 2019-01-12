using System;
using System.ComponentModel.DataAnnotations;
using IMDG_TaskListApp.Models;

public class TaskItem
{
    public int ItemId {get;set;}
    public int TaskId {get;set;}
    public string Description {get;set;}
    public bool Completed {get;set;}
    public DateTime CompletedDateTime{get;set;}

    public TaskList TaskList{get;set;}
}