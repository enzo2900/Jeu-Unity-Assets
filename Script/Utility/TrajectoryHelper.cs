using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TrajectoryHelper 
{
    
    public static  float getGravityOfJumpWithJHTRA(float jumpHeight,float TimeToReachApex)
    {
        return -(2 * jumpHeight) / Mathf.Pow(TimeToReachApex, 2);
    }

    public static float getJumpVelocityOfJumpWithJHTRA(float jumpHeight, float TimeToReachApex)
    {
        return 2*jumpHeight / TimeToReachApex;
    }

    public static float getGravity2OfJumpWithJHDHVX(float jumpHeight,float distanceHeight,float velocityX)
    {
        return (2* jumpHeight *velocityX*velocityX) / (distanceHeight*distanceHeight); ;
    }
    public static float getJumpVelocityOfJumpWithJHDHVX(float jumpHeight, float distanceHeight, float velocityX)
    {
        return 2 * jumpHeight * velocityX / distanceHeight;
    }

}
