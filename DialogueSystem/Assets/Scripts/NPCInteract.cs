using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCInteract : NPCTrigger
{
    public CanvasGroup NPCCanvas;
    private bool inRange = false;

    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.Q))
        {
            TriggerDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToInitiateDialogue))
        {
            NPCCanvas.alpha = 1;
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToInitiateDialogue))
        {
            NPCCanvas.alpha = 0;
            inRange = false;
        }
    }
}
