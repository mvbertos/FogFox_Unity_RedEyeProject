using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PropEntity : MonoBehaviour, IInteraction
{
    public virtual void OnBeingInteract()
    {
        print("Hello my name is " + gameObject.name);
    }

    public virtual void OnInteract()
    {
        
    }
}
