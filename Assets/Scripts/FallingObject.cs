using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("у червяка " );
        if (collision.CompareTag("Destr"))
        {
            Destroy(gameObject);
        }

        /*if (collision.CompareTag("Player"))
        {
            Hero.Instance.GetDamage();
            HealthSystem.health -= 1;
           // Destroy(gameObject);
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            //OnTrigger
        }
    }
}
