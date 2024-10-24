using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatformController : MonoBehaviour
{
    [SerializeField]
    Vector3 Move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Move * Time.deltaTime); 
    }

    // Appelée lorsque l'objet entre en collision avec la plateforme
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si l'objet qui entre en collision est un joueur ou un objet mobile
        if (collision.gameObject.CompareTag("Player"))
        {
            // On attache l'objet à la plateforme
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        // On attache l'objet à la plateforme
        collision.transform.Translate(Move);
        
    }

    // Appelée lorsque l'objet quitte la collision avec la plateforme
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Si l'objet qui quitte la collision est un joueur ou un objet mobile
        if (collision.gameObject.CompareTag("Player"))
        {
            // On détache l'objet de la plateforme
            collision.transform.SetParent(null);
        }
    }
}
