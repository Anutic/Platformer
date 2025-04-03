using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    public Animator boxAnim;
    public Animator startAnim;
    private Queue<string> sentences;
//

 public bool dialogueActive = false; // Добавьте новую переменную

    /*private bool playerInRange;
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            // вызываем метод StartDialogue() только если игрок находится рядом с NPC
            StartDialogue(dialogue);
        }
    }*/
  //  
    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {//
 if (dialogueActive) // Проверьте, активен ли уже диалог
        {
            return; // Если диалог уже активен, просто выйдите из метода
        }   
 dialogueActive = true;
//   
 boxAnim.SetBool("boxOpen", true);
        startAnim.SetBool("startOpen", false);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {//
        dialogueActive = false;
//       
 boxAnim.SetBool("boxOpen", false);
    }
    
}
