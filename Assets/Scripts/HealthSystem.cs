using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
{
    public static int health;
    public GameObject Heart1, Heart2, Heart3;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string[] lines;
    [SerializeField] float TextSpeed;
 private int index;
//private bool shouldStartDialog = false;
    void Start()
    {
        Heart1.SetActive(true);
        Heart2.SetActive(true);
        Heart3.SetActive(true);
        health = 3;
//
//text.text = string.Empty;
    //StartDialog();
    }

    // Update is called once per frame

    void Update()
    {
        switch (health)
        {
            case 3:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(true);
                break;
            case 2:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(false);
                break;
            case 1:
                Heart1.SetActive(true);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
                break;
            case 0:
                Heart1.SetActive(false);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
                break;

        }
//
 /*if (Input.GetMouseButtonDown(0))
        {
            if (text.text == lines[index])
            {
                IsNextLine();
            }
            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }*/
    }
//
 public void StartDialog()
    {
		//shouldStartDialog = true;
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }


    void IsNextLine()
    {
        if(index < lines.Length -1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
