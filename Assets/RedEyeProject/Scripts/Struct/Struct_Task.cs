using UnityEngine;

[System.Serializable]
public struct Struct_Task
{
    [Header("Task Information")]
    public Enum_TaskType TaskType;
    public Enum_TaskState State;
    public Enum_TimePeriod Period;

    [Header("Objective Information")]
    public Transform Position;
    public GameObject TaskObject;

}
