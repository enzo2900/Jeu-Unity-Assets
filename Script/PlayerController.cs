using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    [SerializeField]
    float gravity ;

    [SerializeField]
    float jumpHeight = 10;
    [SerializeField]
    float TimeToJumpApex = 2f;
    float jumpVelocity ;

    float velocityXSmoothing;
    float AccelerationTimeAirborne = .2f;
    float accelerationTImeGrounded = .1f;
    Vector3 velocity;

    [SerializeField]
    DreamManager dreamManager;

    CountdownTimer timer;

    Collider2D collider2D;

    Controller2D controller;
    //Controller2D controller;

    bool isInDreamControl;

    // Start is called before the first frame update
    void Start()
    {
        
        collider2D = GetComponent<Collider2D>();
       controller = GetComponent<Controller2D>();

        timer = new CountdownTimer(12);

        /* timer.OnTimerStop = () =>
         {
             controller.RemoveLayerMask("ObstacleDream");
         };
         controller.AddLayerMask("ObstacleDream");
         timer.Start();*/
        isInDreamControl = false;
        gravity = TrajectoryHelper.getGravityOfJumpWithJHTRA(jumpHeight, TimeToJumpApex);
        jumpVelocity = TrajectoryHelper.getJumpVelocityOfJumpWithJHTRA(jumpHeight, TimeToJumpApex);
        //gravity = -(2 * jumpHeight) / Mathf.Pow(TimeToJumpApex, 2);
        //jumpVelocity = Mathf.Abs(gravity) * TimeToJumpApex;
        //print("Gravity : " + gravity);
    }

    // Update is called once per frame
    void Update()
    {
        //timer.Tick(Time.deltaTime);
        dreamManager.gameObject.transform.position = transform.position;
        //print("Touche sol : "+controller.collisions.below);
        //print("Touche plafond : "+controller.collisions.above);

        /*if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }
        if (controller.collisions.left || controller.collisions.right)
        {
            velocity.x = 0;

        }*/

        HandleControl();

        float xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.Space)  )
        {
             velocity.y = jumpVelocity;
        }

        //velocity.x = xInput * speed;
         velocity.x = Mathf.SmoothDamp(velocity.x, xInput * speed,ref velocityXSmoothing,(controller.collisions.below)? accelerationTImeGrounded: AccelerationTimeAirborne);
       // velocity.x = xInput * speed;
        velocity.y += gravity * Time.deltaTime;
       // transform.Translate(velocity* Time.deltaTime);
        controller.Move( velocity * Time.deltaTime,ref velocity);
    }

    void walk()
    {

    }
    void HandleControl()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("IsInDream" + isInDreamControl);
            isInDreamControl = !isInDreamControl;
            if(isInDreamControl)
            {
                Debug.Log("InDream");
                controller.AddLayerMask("ObstacleDream");
                //dreamManager.EnableVisibility();
            } else
            {
                Debug.Log("NotInDream");
                controller.RemoveLayerMask("ObstacleDream");
                //dreamManager.DisableVisibility();
            }
        }
    }

    
}
