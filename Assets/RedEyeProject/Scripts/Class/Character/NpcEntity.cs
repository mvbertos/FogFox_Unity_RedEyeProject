using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(TaskManager))]
public class NpcEntity : CharacterEntity
{
    private AIPath _AIPath;
    private AIDestinationSetter _AIDestinationSetter;
    private TaskManager _taskManager;

    private void Start()
    {
        _AIPath = gameObject.GetComponent<AIPath>();
        _AIDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _taskManager = gameObject.GetComponent<TaskManager>();
        
        //TODO: create sequence to get next task after the first is done.
        StartCoroutine(ExecuteCurrentTask(_taskManager.GetCurrentTask()));
    }

    private IEnumerator ExecuteCurrentTask(Struct_Task task)
    {
        switch (task.TaskType)
        {
            case Enum_TaskType.InteractWith:
                _AIDestinationSetter.target = task.Position;
                do
                {
                    if (!task.TaskObject.TryGetComponent<IInteraction>(out IInteraction io)) { yield return null; }

                    if (InteractableObjects(interactionPoint, interactionRange) == io)
                    {
                        io.OnBeingInteract();
                        task.State = Enum_TaskState.Completed;
                    }

                    yield return new WaitForEndOfFrame();

                } while (task.State != Enum_TaskState.Completed);
                break;
            default:
                break;
        }
    }
    protected override void OnAttack()
    {
        throw new System.NotImplementedException();
    }


    protected override IEnumerator OnDash(Vector2 direction, float actionTime)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnMove(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnUseSkill()
    {
        throw new System.NotImplementedException();
    }
}
