using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystemActor : InteractionActor, Interactible

{
    [SerializeField]
    private string DialogText;

    [SerializeField]
    AssetLoader loader;

    [SerializeField]
    private float interactionRange;

    GameObject Instantiated;

    public void Interact()
    {
        Debug.Log("interact");
        DialogSystem.instance.show(DialogText);
    }
    
    public override Interactible interactionTarget()
    {
        return this;
    }
}

public interface Interactible
{
    public void Interact();
}
