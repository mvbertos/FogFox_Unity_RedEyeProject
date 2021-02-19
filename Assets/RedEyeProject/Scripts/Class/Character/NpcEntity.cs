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
    private TaskManager _TaskManager;

    private void Start()
    {
        //Initializing Components
        _AIPath = gameObject.GetComponent<AIPath>();
        _AIDestinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        _TaskManager = gameObject.GetComponent<TaskManager>();

        //AIPAth Configuration
        _AIPath.maxSpeed = moveSpeed;
        _AIPath.enableRotation = false;
        _AIPath.orientation = OrientationMode.YAxisForward;

        //Tasks System
        StartCoroutine(ExecuteCurrentTask(_TaskManager.GetCurrentTask()));
    }

    private IEnumerator ExecuteCurrentTask(Struct_Task task)
    {
        do
        {
            switch (task.TaskType)
            {
                //Interact With Something
                case Enum_TaskType.InteractWith:

                    _AIDestinationSetter.target = task.Position;


                    do // While task state != completed do:
                    {
                        if (task.TaskObject == null) { yield return null; } //TaskObject is null
                        if (!task.TaskObject.TryGetComponent<IInteraction>(out IInteraction io)) { yield return null; }//Task object has IInteraction interface

                        if (InteractableObjectOnRange(interactionPoint, interactionRange) == io)
                        {
                            io.OnBeingInteract();
                            task.State = Enum_TaskState.Completed;
                            _TaskManager.TaskValidation(task);
                        }

                        yield return new WaitForEndOfFrame();

                    } while (task.State != Enum_TaskState.Completed);
                    break;
                case Enum_TaskType.Idle:
                    _AIDestinationSetter.target = task.Position;

                    do
                    {

                        yield return new WaitForEndOfFrame();

                    } while (_TaskManager.GetCurrentTask().TaskType == Enum_TaskType.Idle);
                    break;
                default:
                    break;
            }

            task = _TaskManager.GetCurrentTask();
            yield return new WaitForEndOfFrame();
        } while (true);
    }

    public override void OnMove(Vector2 direction)
    {
        throw new System.NotImplementedException();
    }

    public override void OnUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDash(Skill dashSkill)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator OnAttack(float coolDown, float Damage)
    {
        throw new System.NotImplementedException();
    }
}
