using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : RayCastController
{
    
    float maxAngle = 60f;

    public CollisionInfo collisions;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }


   

    public void Move(Vector3 velocity,ref Vector3 velocityOfCaller)
    {
        UpdateRayCastOrigins();
        collisions.reset();
        if (velocity.y > -0.009 && velocity.y < 0.009)
        {
            velocity.y = 0;
        }
        if (velocity.x < 0.001 && velocity.x > -0.001)
        {
            velocity.x = 0;
        }

        if (velocity.x != 0)
        {
            Debug.Log("Verification collisionHorizontale");
            HorizontalCollisions(ref velocity,ref velocityOfCaller);
        }
       if (velocity.y != 0)
            
        {
            Debug.Log("Verification collisionVerticale");
            VerticalCollisions(ref velocity, ref velocityOfCaller);
        }
       
       // transform.position = transform.position +velocity;
        transform.Translate(velocity);
    }

    void VerticalCollisions(ref Vector3 velocity, ref Vector3 velocityOfCaller)
    {
        float directionY = Mathf.Sign(velocity.y);

        float rayLength = Mathf.Abs(velocity.y)+  SKIN_WIDTH;
        for (int i = 0; i < VerticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.up * directionY,rayLength,collisionMask );

            //Debug.Log(rayLength);
            Debug.DrawRay(rayOrigin,Vector2.up *directionY * rayLength, Color.red);
            
            if(hit)
            {
                velocity.y = (hit.distance -SKIN_WIDTH)* directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
                
                //Useless ?
                if (collisions.climbingSlope)
                {
                    velocity.x = velocity.y /Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                }

                if (collisions.below)
                {
                    Debug.Log("Touche le sol");
                }
               
                velocityOfCaller.y = 0;
            }
        }
    }

    void HorizontalCollisions(ref Vector3 velocity,ref Vector3 velocityOfCaller)
    {
        float directionX = Mathf.Sign(velocity.x);

        float rayLength = Mathf.Abs(velocity.x) +  SKIN_WIDTH;
        for (int i = 0; i < HorizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? rayCastOrigins.bottomLeft : rayCastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength,collisionMask) ;

            // Debug.DrawRay(rayCastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.down * 2, Color.red);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX / rayLength, Color.blue);
            if (hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
               
                if(slopeAngle <= maxAngle)
                {
                    float DistanceToSlopeStart = slopeAngle;
                    //Useless ?
                    /*if(DistanceToSlopeStart != collisions.slopeAngle)
                    {
                        DistanceToSlopeStart = hit.distance - SKIN_WIDTH;
                        velocity.x -= DistanceToSlopeStart * directionX;
                    }
                    Debug.Log("SlopeAngle : " + slopeAngle);*/
                    ClimbSlope(ref velocity,slopeAngle);
                   // velocity.x += DistanceToSlopeStart * directionX;
                } else if(!collisions.climbingSlope || slopeAngle > maxAngle) 
                {
                    collisions.climbingSlope = false;
                    velocity.x = (hit.distance - SKIN_WIDTH) * directionX;
                    rayLength = hit.distance;
                    // USeless ?
                    /*if(collisions.climbingSlope)
                    {
                        velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                    }*/
                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                    

                    velocityOfCaller.x = 0;
                }
                

                
                
                //velocityOfCaller.x = 0;
            }
        }
    }
    void ClimbSlope(ref Vector3 velocity, float slopeAngle)
    {
        
        float moveDistance = Mathf.Abs(velocity.x);
        float climbVelocity = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        if(velocity.y <= climbVelocity)
        {
            velocity.y = climbVelocity;
            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
            collisions.below = true;
            collisions.slopeAngle = slopeAngle;
            collisions.climbingSlope = true;
        }
       
    }
    

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public bool climbingSlope;
        public float slopeAngle;

        public void reset()
        {
            above = below = false;
            left = right = false;

        }
    }
}
