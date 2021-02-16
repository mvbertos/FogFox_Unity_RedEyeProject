using UnityEngine;

[System.Serializable]
public struct Struct_Task
{
    public Struct_Task(int iD, Enum_TaskType taskType, Enum_TaskState state, Enum_TimePeriod period, Transform position, GameObject taskObject)
    {
        ID = iD;
        TaskType = taskType;
        State = state;
        Period = period;
        Position = position;
        TaskObject = taskObject;
    }

    [Header("Task Information")]
    public int ID;
    public Enum_TaskType TaskType;
    public Enum_TaskState State;
    public Enum_TimePeriod Period;

    [Header("Objective Information")]
    public Transform Position;
    public GameObject TaskObject;


}
