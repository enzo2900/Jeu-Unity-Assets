using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastController : MonoBehaviour
{
    [SerializeField]
    public LayerMask collisionMask;

    protected const float SKIN_WIDTH = 0.015f;

    protected float horizontalRaySpacing;
    protected float verticalRaySpacing;

    public int HorizontalRayCount = 4;
    public int VerticalRayCount = 4;

    protected BoxCollider2D boxCollider2D;
    protected RayCastOrigins rayCastOrigins;
    // Start is called before the first frame update
    public virtual void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    protected void UpdateRayCastOrigins()
    {
        Bounds bounds = boxCollider2D.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    protected void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider2D.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
        VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (HorizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
    }
    protected struct RayCastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public void AddLayerMask(string nameLayer)
    {
        collisionMask |= (1 << LayerMask.NameToLayer(nameLayer));
    }
    public void RemoveLayerMask(string nameLayer)
    {
        collisionMask &= ~(1 << LayerMask.NameToLayer(nameLayer));
    }
}
