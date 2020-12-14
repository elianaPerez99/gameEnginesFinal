/*Eliana Perez | NPC| Holds a dialogue sequence and acts as a trigger*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    //dialogue stuff
    [SerializeField]
    private Sentence sentence;

    //triggers the dialogue to start
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(sentence);
    }
}
