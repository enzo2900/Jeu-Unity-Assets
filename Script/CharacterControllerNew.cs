using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerNew : MonoBehaviour
{
    #region var
    [Range(1f, 100f)] public float MaxWalkSpeed = 12.5f;
    [Range(0.25f, 50f)] public float GroundAcceleration = 0.01f;
    [Range(0.25f, 50f)] public float GroundDeceleration = 20f;
    [Range(0.25f, 50f)] public float AirAcceleration = 5;
    [Range(0.25f, 50f)] public float AirDecceleration= 5;

    [Range(1f, 100f)] public float MaxRunSpeed = 20f;
    public float MaxFallSpeed = -20;
    public LayerMask GroundLayer;
    public float GroundDetectionRayLength = 0.02f;
    public float JumpBufferTimer = 0.125f;
    public float JumpHeight = 6.5f;
    public float TimeToReachApex = 0.35f;
    private float _coyoteTimer = 0.2f;

    #endregion
   
    private RaycastHit2D HitFloor;
    private RaycastHit2D HitCeiling;

    [SerializeField]
    private string currentState;

    private float Gravity;
    private float InitialJumpVelocity;

    private bool _isFacingRight;
    private bool _isGrounded;
    private bool IsInDream;

    Collider2D feet;
    private Collider2D collided;

    private Rigidbody2D rb;
    private InputManagerC InputManagerC;
    private StateManager stateManager;
    [SerializeField]
    public DreamManager dreamManager;
    public Animator animator;

    Vector2 velocity;
    Vector2 moveVelocity;

    CountdownTimer timerJumpBefore;
    CountdownTimer timerCoyoteTime;
    CountdownTimer timerToApex;

    [SerializeField]
    AssetLoader loader;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        stateManager = new StateManager();
        timerJumpBefore = new CountdownTimer(0.1f);
        timerCoyoteTime = new CountdownTimer(_coyoteTimer);
        timerToApex = new CountdownTimer(TimeToReachApex);

        Gravity = TrajectoryHelper.getGravityOfJumpWithJHTRA(JumpHeight, TimeToReachApex);
        InitialJumpVelocity = TrajectoryHelper.getJumpVelocityOfJumpWithJHTRA(JumpHeight, TimeToReachApex);

        InputManagerC = GetComponent<InputManagerC>();
        _isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        feet =GetComponent<Collider2D>();
        
        GroundedState groundedState = new GroundedState(SetVelocityY,animator);
        RunState runState = new RunState(animator);
        JumpingState jumpingState = new JumpingState(SetVelocityY, InitialJumpVelocity,timerJumpBefore,timerToApex, MultiplyGravity,animator);
        FallingState fallingState = new FallingState(MultiplyGravity,animator);
        CoyoteTimeState coyoteTimeState = new CoyoteTimeState(timerCoyoteTime,MultiplyGravity,animator);
        FastFallingState fastFallingState = new FastFallingState(timerJumpBefore, MultiplyGravity, animator);

        groundedState.AddTransition(new StateT(() => InputManagerC.JumpWasPressed ,jumpingState));
        groundedState.AddTransition(new StateT(() => !_isGrounded,coyoteTimeState));
        groundedState.AddTransition(new StateT(() => InputManagerC.MovementKeyPress,runState));

        runState.AddTransition(new StateT(() => !InputManagerC.MovementKeyPress, groundedState));
        runState.AddTransition(new StateT(() => InputManagerC.JumpWasPressed, jumpingState));
        runState.AddTransition(new StateT(() => !_isGrounded, coyoteTimeState));

        coyoteTimeState.AddTransition(new StateT(() => InputManagerC.JumpWasPressed, jumpingState));
        coyoteTimeState.AddTransition(new StateT(() => timerCoyoteTime.isFinished, fallingState));
        coyoteTimeState.AddTransition(new StateT(() => _isGrounded, groundedState));

        jumpingState.AddTransition(new StateT(() => timerJumpBefore.isFinished && (!timerToApex.isFinished && !InputManagerC.JumpIsHeld), fastFallingState));
        jumpingState.AddTransition(new StateT(() => timerJumpBefore.isFinished &&  timerToApex.isFinished,fastFallingState));
        jumpingState.AddTransition(new StateT(() => timerJumpBefore.isFinished && _isGrounded, groundedState));

        fallingState.AddTransition(new StateT(() => _isGrounded, groundedState));

        fastFallingState.AddTransition(new StateT(() => timerJumpBefore.isFinished &&  _isGrounded, groundedState));

        stateManager.SetInitialState(groundedState);
    }

    private void MultiplyGravity(float value)
    {
        velocity.y += Gravity * value * Time.deltaTime ;
    }
   
    private void Update()
    {
        stateManager.Update();
        Debug.Log(stateManager.getName());
        currentState = stateManager.getName();

        if(InputManagerC.DreamWasPressed)
        {
            dreamManager.ChangeDreamState();
        }
        if(InputManagerC.InteractWasPressed)
        {
            loader.button?.Interact();
        }
        
    }
    public void StopInput()
    {
        InputManagerC.enabled = false;
        InputManagerC.MovementKeyPress = false;
        InputManagerC.InteractWasPressed = false;
        InputManagerC.Movement = new Vector2(0, 0);
    }

    public void startInput  ()
    {
        InputManagerC.enabled = true;
    }
    public void Die()
    {

    }
    public void AddLayerMask()
    {
        GroundLayer |= (1 << LayerMask.NameToLayer("ObstacleDream"));
        this.gameObject.layer = LayerMask.NameToLayer("PlayerInDream");
    }
    private void SetVelocityY(float newY)
    {
        velocity = new Vector2(velocity.x, newY);
    }

    public void RemoveLayerMask()
    {
        GroundLayer &= ~(1 << LayerMask.NameToLayer("ObstacleDream"));
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            //collectible.Collect();
        }

    }
    private void FixedUpdate()
    {
       dreamManager.transform.position = transform.position;
        //JumpChecks();
        CollisionChecks();
        stateManager.FixedUpdate();

        if (_isGrounded )
        {   
            Debug.Log("IsGrounded");
            Move(GroundAcceleration, GroundDeceleration,InputManagerC.Movement);
        } else
        {
            Move(AirAcceleration, AirDecceleration, InputManagerC.Movement);
        }

        ApplyGravityOnGameObject();
    }
    void ApplyGravityOnGameObject()
    {
        velocity.y = Mathf.Clamp(velocity.y, MaxFallSpeed, 100);
        rb.velocity = new Vector2(rb.velocity.x, velocity.y);
    }


    private void Move(float acceleration,float deceleration, Vector2 moveInput)
    {
        if(moveInput != Vector2.zero)
        {
            
            TurnCheck(moveInput);
            Vector2 targetVelocity = Vector2.zero;
            if (InputManagerC.RunWasPressed)
            {
                targetVelocity = new Vector2(moveInput.x, 0) * MaxRunSpeed;
            } else
            {
                targetVelocity = new Vector2(moveInput.x, 0) * MaxWalkSpeed;
            }
            moveVelocity = Vector2.Lerp(moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);
        }else
        {
            moveVelocity = Vector2.Lerp(moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            rb.velocity = new Vector2(moveVelocity.x, rb.velocity.y);

        }
        
    }

   
    private void TurnCheck(Vector2 moveInput)
    {
        if(_isFacingRight && moveInput.x <0) {
            Turn(false);
        }
        else if (!_isFacingRight && moveInput.x >0)
        {
            Turn(true);             
        }
    }

    private void Turn(bool turnRight)
    {
        if(turnRight )
        {
            _isFacingRight = true;
            transform.Rotate(0, 180, 0);
        } else
        {
            _isFacingRight = false;
            transform.Rotate(0, -180, 0);
        }
    }
    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(feet.bounds.center.x, feet.bounds.min.y);
        Vector2 boxCastSize = new Vector2(feet.bounds.size.y/4, GroundDetectionRayLength);
        HitFloor = Physics2D.BoxCast(boxCastOrigin, boxCastSize,0,Vector2.down, GroundDetectionRayLength, GroundLayer);
        if(HitFloor.collider!= null && !HitFloor.collider.isTrigger)
        {
            collided = HitFloor.collider;
            _isGrounded = true;
        } else
        {
            _isGrounded = false;
        }
        Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * GroundDetectionRayLength, Color.blue);
    }
    private void IsTouchingHead()
    {
        Vector2 boxCastOrigin = new Vector2(feet.bounds.center.x, feet.bounds.max.y);
        Vector2 boxCastSize = new Vector2(feet.bounds.size.x, GroundDetectionRayLength);
      
        HitCeiling = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0, Vector2.up, GroundDetectionRayLength, GroundLayer);
        if(HitCeiling.collider != null && !HitCeiling.collider.isTrigger)
        {
            
            if(collided != null ) {
                velocity.y = -2;
                collided = HitCeiling.collider;
            }
            
                
        }
        Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * GroundDetectionRayLength, Color.blue);

    }
    private void CollisionChecks()
    {
        IsGrounded();
        IsTouchingHead();
    }
}
