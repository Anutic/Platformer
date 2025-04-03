using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [SerializeField] private float timerDuration = 20f;
    [SerializeField] private GameObject door;
    private bool isInZone = false;
    private float timer = 0f;
    [SerializeField] private Transform startPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInZone = true;
            timer = timerDuration;
        }
    }

    private void Update()
    {
        if (isInZone)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                OpenDoor();
            }
        }
    }

    private void OpenDoor()
    {
        Destroy(door);
       // door.SetActive(true);
        isInZone = false;
    }
}