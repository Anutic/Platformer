using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTriggerScript : MonoBehaviour
{
    
    public Canvas canvas;
    public Collider2D playerCollider;
    public Image imageToShow;
    public string popupText;
    public float textDelay = 0.08f; // Задержка между символами

    private Text textComponent;
    private bool isInRange = false;

    private void Start()
    {
        imageToShow.enabled = false;
        canvas.enabled = false;
        textComponent = imageToShow.GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            imageToShow.enabled = true;
            canvas.enabled = true;
            isInRange = true;

            StartCoroutine(ShowPopupText(popupText));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            imageToShow.enabled = false;
            canvas.enabled = false;
            isInRange = false;
        }
    }

    private IEnumerator ShowPopupText(string text)
    {
        textComponent.text = "";

        foreach (char c in text)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }
}
/*
public class PopupTriggerScript : MonoBehaviour
{
    public GameObject popupPrefab;
    public string popupText;
    public float delay = 0.05f; // Задержка между символами
    private GameObject popupInstance;
    private Text popupTextComponent;
    private Image popupImage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ShowPopupText(popupText));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (popupInstance != null)
            {
                Destroy(popupInstance);
                popupInstance = null;
                popupTextComponent = null;
                popupImage = null;
            }
        }
    }
    private IEnumerator ShowPopupText(string text)
    {
        if (popupInstance == null)
        {
            popupInstance = Instantiate(popupPrefab, transform.position, Quaternion.identity);
            popupTextComponent = popupInstance.GetComponentInChildren<Text>();
            popupImage = popupInstance.GetComponentInChildren<Image>();
        }

        popupInstance.SetActive(true);

        // Проверяем, что popupTextComponent был проинициализирован
        if (popupTextComponent != null)
        {
            popupTextComponent.text = "";

            foreach (char c in text)
            {
                // Проверяем, что popupTextComponent не равен null перед каждым добавлением символа
                if (popupTextComponent != null)
                {
                    popupTextComponent.text += c;
                    yield return new WaitForSeconds(delay);
                }
                else
                {
                    // Если popupTextComponent равен null, останавливаем процесс отображения текста
                    yield break;
                }
            }
        }
    }
*/
//}
