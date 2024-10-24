using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance;
    [SerializeField]
    CharacterControllerNew player;
    
    private string currentMessage;
    Coroutine currentCoroutine;
    [SerializeField]
    private TextMeshProUGUI dialog;
    bool isActive;
    bool displaying;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            // TODO : Stop player movement
            if(!displaying)
            {
                dialog.text = "";
                currentCoroutine = StartCoroutine(showCurrentText());
            }
            
            if (Input.GetKeyUp(KeyCode.N))
            {
                next();
            }

        }
      
    }

    public void show(string message)
    {
        Debug.Log("Showed");
       displaying = false;
        dialog.enabled = true;
        currentMessage = message;
       isActive = true;
        player.StopInput();
    }

    public void next()
    {
        displaying = false;
        StopCoroutine(currentCoroutine);
        hide();
    }

    public void hide() {
        isActive = false;
        dialog.enabled = false;
        player.startInput();
    }


    IEnumerator showCurrentText()
    {
        displaying = true;
        for(int i = 0; i < currentMessage.Length; i++)
        {
            Debug.Log(currentMessage);
            Debug.Log(dialog.text);
            //TODO appear text
            dialog.text+=currentMessage[i];
            yield return new WaitForSeconds(0.1f);
            //StartCoroutine(Wait(0.1f);
        }
        yield return null;
    }

    IEnumerator Wait(float time)
    {   
        yield return new WaitForSeconds(time);
    }
}
