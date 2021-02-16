using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TaskManager : MonoBehaviour
{
    public List<Struct_Task> m_TaskList = new List<Struct_Task>();
    private Struct_Task managerCurrentTask = new Struct_Task();

    /// <summary>
    /// Return the first task in queue
    /// </summary>
    /// <returns></returns>
    public Struct_Task GetCurrentTask()
    {
        try
        {
            Struct_Task currentTask = m_TaskList.ElementAt(0);

            currentTask.State = Enum_TaskState.Executing;
            managerCurrentTask = currentTask;

            return managerCurrentTask;
        }
        catch
        {
            return TaskNotValid();
        }

    }

    /// <summary>
    /// checks if the current task is completed
    /// </summary>
    public void TaskValidation(Struct_Task currentTask)
    {
        if (currentTask.State == Enum_TaskState.Completed) //If task state is completed
        {
            NextTask(currentTask);
        }
    }

    /// <summary>
    /// If task exists on m_taskList it will update task list
    /// </summary>
    /// <param name="currentTask"></param>
    private Struct_Task NextTask(Struct_Task currentTask)
    {
        try
        {
            foreach (Struct_Task task in m_TaskList) //Verify if list has same task id
            {
                if (task.ID == currentTask.ID)
                {

                    m_TaskList.Remove(task);
                    return m_TaskList.ElementAt(0);//return new task


                }
            }
        }
        catch
        {
            return TaskNotValid();
        }
        return TaskNotValid();
    }
    private Struct_Task TaskNotValid()
    {
        return new Struct_Task(-1, Enum_TaskType.Idle, Enum_TaskState.Queue, Enum_TimePeriod.Mornign, this.transform, this.gameObject);//return Idle task
    }
}
