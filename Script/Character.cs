using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public abstract class InteractionActor : MonoBehaviour
{
    [SerializeField]
    AssetLoader loader;


    [SerializeField]
    private float interactionRange;

    GameObject InstantiatedButton;

    public abstract Interactible interactionTarget();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterControllerNew>(out CharacterControllerNew player))
        {
            GameObject obj = Instantiate(loader.InteractButton);
            obj.transform.position = transform.position;
            obj.GetComponent<InteractButton>().target = interactionTarget();
            //obj.GetComponent<InteractButton>().target = this;
            loader.button = obj.GetComponent<InteractButton>();

            InstantiatedButton = obj;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(InstantiatedButton);
        loader.button = null;
    }
}


