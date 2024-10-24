using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DreamCollectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
       
    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public interface ICollectible
{
    public void Collect();
}