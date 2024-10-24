using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class DreamManager : MonoBehaviour
{
    [SerializeField]
    CharacterControllerNew player;

    CircleCollider2D circleCollider;
    List<Renderer> renderers;
    bool IsSetOnVisible;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        IsSetOnVisible = false;
        renderers = new List<Renderer>(); 
        circleCollider = GetComponent<CircleCollider2D>();
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detecter avec dream objetct");
        if(collision.TryGetComponent<Renderer>(out Renderer renderer))
        {
            renderers.Add(renderer);
            renderer.enabled = IsSetOnVisible;
        }

    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Renderer>(out Renderer renderer))
        {
            if (renderers.Contains(renderer))
            {
                 renderers.Remove(renderer);
            }
        }
    }

    private bool IsInDream;
    public void ChangeDreamState()
    {
        Debug.Log("Dream");
        //Gizmos.DrawSphere(player.transform.position, 4);
        RaycastHit2D raycast = Physics2D.CircleCast(player.transform.position, 10, Vector2.zero,2,LayerMask.NameToLayer("ObstacleDream"));
        if(!IsInDream && raycast.collider != null)
        {
            return;
        }
        if (!IsInDream)
        {
            player.AddLayerMask();
            IsInDream = true;
            EnableVisibility();
        }
        else
        {
            IsInDream = false;
            player.RemoveLayerMask();
            DisableVisibility();
        }
    }
    public void RemoveAll()
    {
        renderers.RemoveAll((e) => true);
    }

     void EnableVisibility()
    {
        IsSetOnVisible = true;
        foreach(Renderer renderer in renderers) {
            renderer.enabled = true;
        }
    }

     void DisableVisibility()
    {
        IsSetOnVisible = false;
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(circleCollider != null)
        {
            Gizmos.DrawSphere(transform.position, circleCollider.radius);
        }
       
    }
}
