using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GroundedState : State
{
    Action<float> setterVelocity;
    Animator animator;
    public GroundedState(Action<float> setterVelocity,Animator animator) : base("GroundedState")
    {
        this.setterVelocity = setterVelocity;
        this.animator = animator;
    }

    public override void FixedUpdate()
    {
       
    }

    public override void OnEnter()
    {
        setterVelocity(0);
        animator.Play("Idle");
    }

    public override void OnInitialize()
    {
       
    }

    public override void Update()
    {
        
    }
}
public class RunState : State
{
    private Animator animator;
    public RunState(Animator animator) : base("RunState")
    {
        this.animator = animator;
    }

    public override void FixedUpdate()
    {
    }

    public override void OnEnter()
    {
        animator.Play("RunAnima");
    }

    public override void OnInitialize()
    {

    }

    public override void Update()
    {
        
    }
}

public class VelocityWrapper
{
    public Vector2 velocity;
    
    public VelocityWrapper(Vector2 vector2 )
    {
        this.velocity = vector2; 
    }
}
public class JumpingState : State
{
    Action<float> ApplyGravityMutilplier;

    float jumpVelocity;

    CountdownTimer timerBeforeJump;
    Action<float> addGravity;
    CountdownTimer timerTOApex;
    Animator Animator;
    public JumpingState(Action<float> ApplyGravityMutilplier,float jumpVelocity,CountdownTimer timerBeforeJump,CountdownTimer timerToApex,Action<float>  addGravity,Animator animator) : base("JumpingState")
    {
        this.ApplyGravityMutilplier = ApplyGravityMutilplier;
        this.jumpVelocity = jumpVelocity;
        this.timerBeforeJump = timerBeforeJump;
        this.addGravity = addGravity;
        this.timerTOApex = timerToApex;
        Animator = animator;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void OnEnter()
    {
        ApplyGravityMutilplier(jumpVelocity);
        timerTOApex.Start();
        timerBeforeJump.Start();
        Animator.Play("JumpBegin");
        
    }

    public override void OnInitialize()
    {
       
    }

    public override void Update()
    {
        timerTOApex.Tick(Time.deltaTime);
        timerBeforeJump.Tick(Time.deltaTime);
        addGravity(1);
        
    }
}
public class FastFallingState : State
{
    CountdownTimer timerBeforeENd;
    Action<float> ApplyGravityMutilplier;
    Animator Animator;
    public FastFallingState(CountdownTimer timer, Action<float> changeY,Animator animator) : base("FastFallingState")
    {
        timerBeforeENd = timer;
        ApplyGravityMutilplier = changeY;
        this.Animator = animator;
    }

    public override void FixedUpdate()
    {
        
    }

    public override void OnEnter()
    {
        timerBeforeENd.Start();
        Animator.Play("Fall");
    }

    public override void OnInitialize()
    {
       
    }

    public override void Update()
    {
        timerBeforeENd.Tick(Time.deltaTime);
        ApplyGravityMutilplier(2f);
    }
}
public class FallingState : State
{
    Action<float> ApplyGravityMutilplier;
    Animator Animator;

    public FallingState( Action<float> setter,Animator animator) : base("FallingState")
    {
        ApplyGravityMutilplier = setter;
        this.Animator = animator;
    }
    public override void FixedUpdate()
    {
        
    }
    public override void OnEnter()
    {
        Animator.Play("Fall");
    }

    public override void OnInitialize()
    {
        
    }

    public override void Update()
    {
       ApplyGravityMutilplier(1);
    }
}
public class CoyoteTimeState : State
{
    CountdownTimer timerEndState;
    float gravity;
    Action<float> ApplyGravityMutilplier;
    Animator Animator;
    public CoyoteTimeState(CountdownTimer timer,Action<float> setter,Animator animator) : base("CoyoteTimeState")
    {
        this.timerEndState = timer;
        this.ApplyGravityMutilplier = setter;
        this.gravity=gravity;
        this.Animator = animator;
    }

    public override void FixedUpdate()
    {
        
    }
    public override void OnEnter()
    {
       timerEndState.Start();
       Animator.Play("Fall");
    }

    public override void OnInitialize()
    {
        
    }

    public override void Update()
    {
        timerEndState.Tick(Time.deltaTime);
        ApplyGravityMutilplier(1);
    }
}