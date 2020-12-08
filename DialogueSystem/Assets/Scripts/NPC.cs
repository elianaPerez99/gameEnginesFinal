/*Eliana Perez | NPC| Holds a dialogue sequence and acts as a trigger*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //dialogue stuff
    [SerializeField]
    private Dialogue[] dialogues;



    /*triggers the dialogue to start | when you play the dialogue a second time, it skips the first sentence*/
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues);
    }
}
