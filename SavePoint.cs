
using Newtonsoft.Json;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class SavePoint : InteractionActor, Interactible
{

    public void Interact()
    {
        saveGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void saveGame()
    {
        string json = JsonConvert.SerializeObject("az",Newtonsoft.Json.Formatting.Indented);
        //File.WriteAllText("", json);
        Debug.Log("Saving game");
        
        //TODO : Save 
    }

    public override Interactible interactionTarget()
    {
        return this;
    }
}
