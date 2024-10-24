using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public Interactible target;
    public void SetTarget(Interactible target)
    {
        Debug.Log(target);
        this.target =  target;
        Debug.Log(this.target);
    }

    public void Interact()
    {
        target.Interact();
    }
}
