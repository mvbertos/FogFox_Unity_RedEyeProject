using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TaskManager : MonoBehaviour
{
    public List<Struct_Task> m_TaskList = new List<Struct_Task>();

    /// <summary>
    /// Return the first task in queue
    /// </summary>
    /// <returns></returns>
    public Struct_Task GetCurrentTask()
    {
        Struct_Task currentTask = m_TaskList.ElementAt(0);
        currentTask.State = Enum_TaskState.Executing;
        return currentTask;
    }
    /// <summary>
    /// checks if the current task is completed
    /// </summary>
    public void TaskValidation()
    {
        Struct_Task currentTask = m_TaskList.ElementAt(0);
        if (currentTask.State == Enum_TaskState.Completed)
        {
            NextTask(currentTask);
        }
    }
    /// <summary>
    /// If task exists on m_taskList it will update task list
    /// TODO: Verify if task period match with the current period, if dont try get next
    /// </summary>
    /// <param name="currentTask"></param>
    public void NextTask(Struct_Task currentTask)
    {
        if (m_TaskList.Contains(currentTask))
            m_TaskList.Remove(currentTask);
    }
}
