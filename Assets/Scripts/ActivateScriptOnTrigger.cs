using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScriptOnTrigger : MonoBehaviour
{
    public RandomSpawn randomSpawnScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            randomSpawnScript.enabled = true;
        }
    }
}
