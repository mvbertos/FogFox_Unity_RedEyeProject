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
        Struct_Task currentTask = m_TaskList.ElementAt(0);
        currentTask.State = Enum_TaskState.Executing;
        managerCurrentTask = currentTask;
        return currentTask;
    }
    /// <summary>
    /// checks if the current task is completed
    /// </summary>
    public void TaskValidation(Struct_Task currentTask)
    {
        if (currentTask.State == Enum_TaskState.Completed)
        {
            print(managerCurrentTask + "/" + currentTask);
            if (managerCurrentTask.ID == currentTask.ID) { print("It's Equal"); }

            NextTask(currentTask);
        }
    }
    /// <summary>
    /// If task exists on m_taskList it will update task list
    /// TODO: Verify if task period match with the current period, if dont try get next
    /// </summary>
    /// <param name="currentTask"></param>
    private Struct_Task NextTask(Struct_Task currentTask)
    {

        foreach (Struct_Task task in m_TaskList)
        {
            if (task.ID == currentTask.ID)
            {
                m_TaskList.Remove(currentTask);

                print(task.TaskObject + "/" + m_TaskList.IndexOf(task).ToString() + "/" + task.State);

                return m_TaskList.ElementAt(0);
            }
        }

        return new Struct_Task();
    }
}
