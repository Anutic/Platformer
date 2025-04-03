using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Popup2 : MonoBehaviour
{
    public Canvas canvas;
    public Collider2D playerCollider;
    public Image imageToShow;
    public string[] popupTexts;
    public float textDelay = 0.05f; // Задержка между символами

    private Text textComponent;
    private bool isInRange = false;
    private bool isWaitingForInput = false; // Флаг ожидания ввода
    private Coroutine popupCoroutine; // Ссылка на текущую корутину вывода текста

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

            popupCoroutine = StartCoroutine(ShowPopupTexts(popupTexts));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider)
        {
            imageToShow.enabled = false;
            canvas.enabled = false;
            isInRange = false;

            if (popupCoroutine != null)
            {
                StopCoroutine(popupCoroutine); // Останавливаем текущую корутину вывода текста
            }
        }
    }

    private void Update()
    {
        // Проверяем нажатие ЛКМ только если находимся в зоне коллайдера и ожидаем ввода
        if (isInRange && isWaitingForInput && Input.GetMouseButtonDown(0))
        {
            isWaitingForInput = false;

            // Вызываем метод для перехода к следующему сообщению или завершения
            NextPopupText();
        }
    }

    private IEnumerator ShowPopupTexts(string[] texts)
    {
        foreach (string text in texts)
        {
            textComponent.text = "";

            foreach (char c in text)
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textDelay);
            }

            isWaitingForInput = true; // Ожидаем ввода после вывода каждого сообщения

            // Ждем, пока пользователь нажмет ЛКМ
            while (isWaitingForInput)
            {
                yield return null;
            }

            yield return new WaitForSeconds(3f); // Задержка между строками
        }

        // После завершения вывода всех сообщений
        imageToShow.enabled = false;
        canvas.enabled = false;
    }

    private void NextPopupText()
    {
        // Останавливаем текущую корутину вывода текста
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        // Проверяем, есть ли еще сообщения в массиве
        if (popupTexts.Length > 0)
        {
            // Удаляем текущее сообщение из массива
            string[] remainingTexts = new string[popupTexts.Length - 1];
            Array.Copy(popupTexts, 1, remainingTexts, 0, remainingTexts.Length);
            popupTexts = remainingTexts;

            // Запускаем новую корутину для вывода следующего сообщения
            popupCoroutine = StartCoroutine(ShowPopupTexts(popupTexts));
        }
        else
        {
            // Если сообщений больше нет, завершаем вывод и скрываем изображение
            imageToShow.enabled = false;
            canvas.enabled = false;
        }
    }
}