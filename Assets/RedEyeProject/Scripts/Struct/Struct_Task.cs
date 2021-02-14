using UnityEngine;

[System.Serializable]
public class Struct_Task
{
    [Header("Task Information")]
    public Enum_TaskType TaskType;
    public Enum_TaskState State;
    
    [Header("Objective Information")]
    public Transform Position;
    public GameObject TaskObject;

}
