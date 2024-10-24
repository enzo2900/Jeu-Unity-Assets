using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerC : MonoBehaviour
{
    [SerializeField]
    GameObject inputer;
    InputAction Move;
    InputAction Run;
    InputAction Jump;
    InputAction Interact;

    public  Vector2 Movement;
    public bool JumpWasPressed;
    public bool RunWasPressed;
    public bool JumpWasReleased;
    public float JumpPressedDuration;
    public bool MovementKeyPress;
    public bool DreamWasPressed;
    public bool JumpIsHeld;
    public bool InteractWasPressed;
    InputAction Dream;
    PlayerInput player;
    private void Awake()
    {
        //ntDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = inputer.GetComponent<PlayerInput>();

        Move = player.actions["Move"];
        Run = player.actions["Run"];
        Jump = player.actions["Jump"];
        Dream = player.actions["Dream"];
        Interact = player.actions["Interact"];

        JumpPressedDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MovementKeyPress = Move.IsPressed();
        Movement = Move.ReadValue<Vector2>();
        
        JumpWasPressed = Jump.WasPressedThisFrame();
        RunWasPressed = Run.IsPressed();
        JumpWasReleased = Jump.WasReleasedThisFrame();
        DreamWasPressed = Dream.WasPressedThisFrame();
        InteractWasPressed = Interact.WasPressedThisFrame();
        JumpIsHeld = Jump.IsPressed();
        /*
        if (Jump.IsPressed())
        {
            JumpPressedDuration += Time.fixedDeltaTime;
        } else
        {
            JumpPressedDuration = 0;
        }
        if(JumpWasPressed)
        {
            Debug.Log("WasPressed");
            
        } 
        if(DreamIsHeld)
        {
            Debug.Log("W WasPressed");
        }*/
        

    }
   }
