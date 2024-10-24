using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour, ICollectible
{
    #region uselesse
    /*
    private void JumpChecks()
    {
      if(InputManagerC.JumpWasPressed) //Entrée JumpingState
        {
            JumpReleasedDuringBuffer = false;
        }

      if(InputManagerC.JumpWasReleased) //Sortie Jumping State
        {

            if(JumpBufferTimer> 0)
            {
                JumpReleasedDuringBuffer = true;
            }
            if(_isJumping && VerticalVelocity > 0f)
            {
                if(isPastApexThreshold)
                {
                    isPastApexThreshold = false;
                    _isFastFalling = true ;
                    fastFallTime = TimeForUpwardsCancel;
                    VerticalVelocity = 0;
                } else
                {
                    _isFastFalling = true;
                    fastFallReleaseSpeed = VerticalVelocity;
                }
            } 
        }
      if(JumpBufferTimer>0 && !_isJumping && _isGrounded)
        {
            InitiateJump();
            if(JumpReleasedDuringBuffer)
            {
                _isFastFalling = true;
                fastFallReleaseSpeed = VerticalVelocity;
            }
        }
      if((_isJumping || _isFastFalling) && _isGrounded && VerticalVelocity <= 0)
        {
            _isJumping = false;
            _IsFalling = false;
            _isFastFalling = false;
            fastFallTime = 0;
            isPastApexThreshold = false;
            VerticalVelocity = Physics2D.gravity.y;
        }
    }

    private void Jump()
    {
        if(_isJumping)
        {
            if(_bumpHead)
            {
                _isFastFalling = true;
            }
            if(VerticalVelocity >= 0f)
            {
                _apexPoint = Mathf.InverseLerp(InitialJumpVelocity, 0, VerticalVelocity);
                if (_apexPoint > ApexThreshold)
                {
                    if(!isPastApexThreshold)
                    {
                        isPastApexThreshold = true;
                        _timePastApexThreshold = 0;
                    }
                    if(isPastApexThreshold)
                    {
                        _timePastApexThreshold = Time.fixedDeltaTime;
                        if (_timePastApexThreshold < TimeToReachApex)
                        {
                            VerticalVelocity = 0;
                        } else
                        {
                            VerticalVelocity = -0.01f;
                        }
                    }
                } else
                {
                    VerticalVelocity += Gravity * Time.fixedDeltaTime;
                    if(isPastApexThreshold)
                    {
                        isPastApexThreshold = false;
                    }
                }

            } else if(!_isFastFalling)
            {
                VerticalVelocity += Gravity * GravityOnReleased* Time.fixedDeltaTime;
            } else if(VerticalVelocity < 0f)
            {
                if(!_IsFalling)
                {
                    _IsFalling = true;
                }
            }
        }
        if(_isFastFalling)
        {
            if(fastFallTime >= TimeForUpwardsCancel)
            {
                VerticalVelocity += Gravity * GravityOnReleased* Time.fixedDeltaTime;
            } else if(fastFallTime < TimeForUpwardsCancel)
            {
                VerticalVelocity = Mathf.Lerp(fastFallReleaseSpeed, 0f, (fastFallTime / TimeForUpwardsCancel));
            }
            fastFallTime += Time.fixedDeltaTime;
        }
        if(_isGrounded  && !_isJumping)
        {
            if(!_isFastFalling)
            {
                _isFastFalling = true;
            }
            VerticalVelocity += Gravity * Time.fixedDeltaTime;
        }
        VerticalVelocity = Mathf.Clamp(VerticalVelocity, MaxFallSpeed, 50f);
        rb.velocity = new Vector2(rb.velocity.x, VerticalVelocity);
    }

    private void InitiateJump()
    {
        if(!_isJumping)
        {
            _isJumping = true;
        }
        JumpBufferTimer = 0;
        VerticalVelocity = 5;
    }
    */
    #endregion
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
