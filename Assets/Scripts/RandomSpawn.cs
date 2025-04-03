using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    private float RandX;
    private Vector2 whereToSpawn;
    [SerializeField] private float spawnRate = 1f;
    private float newSpawn = 0.0f;
    private bool startEvent = false;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
    //public Transform targetPosition;
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if (startEvent)
        {
            if (Time.time > newSpawn)
            {
                newSpawn = Time.time + spawnRate;
                    // RandX = Random.Range(118.28f, 134.5f);
                    RandX = Random.Range(startPoint.position.x, endPoint.position.x);
                    
                    whereToSpawn = new Vector2(RandX, transform.position.y);
                Instantiate(obj, whereToSpawn, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startEvent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startEvent = false;
        }
    }
}
