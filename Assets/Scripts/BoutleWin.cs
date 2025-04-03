using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutleWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
