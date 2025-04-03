using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    /*public void TriggerDialogue()
    {
       FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }*/

public void TriggerDialogue()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

        if (dialogueManager != null && !dialogueManager.dialogueActive) // Проверьте, активен ли уже диалог
        {
            dialogueManager.StartDialogue(dialogue);
        }
    }
}
