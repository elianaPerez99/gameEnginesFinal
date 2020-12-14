using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //variables
    private Sentence currentSentence;

    //UI
    public CanvasGroup group;
    public Text nameText;
    public Text sentenceText;
    public Image characterPortrait;

    //Images
    public List<Sprite> allPortraits;

    //NPC stuff
    private AudioSource aSource;
    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    //Searches for the correct image
    private Sprite FindSprite(string name)
    {
        foreach (Sprite sprite in allPortraits)
        {
            if (sprite.name.Equals(name))
            {
                return sprite;
            }
        }
        Debug.Log("Couldn't find sprite with the name: " + name);
        return null;
    }


    //makes the canvas appear and starts the text | no known bugs
    public void StartDialogue(Sentence sentence)
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        Time.timeScale = 0;
        currentSentence = sentence;
        DisplayNextSentence();
    }

    private void Update()
    {
        if (group.alpha == 1 && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

  

    //Displays the next sentence | no known bugs
    private void DisplayNextSentence()
    {
        if (currentSentence == null)
        {
            EndDialogue();
            return;
        }
        
        aSource.Stop();
        nameText.text = currentSentence.dialogue.name;

        if (!currentSentence.dialogue.sound.Equals(null))
        {
            aSource.clip = currentSentence.dialogue.sound;
            aSource.Play();
        }

        if (currentSentence.dialogue.hasImage)
        {
            characterPortrait.sprite = FindSprite(currentSentence.dialogue.imageName);
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentence.dialogue.sentence));
        currentSentence = currentSentence.nextSentence;
    }

    // have the letters in the sentence appear on the screen one by one | no known bugs
    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;
        }
    }

    //ends displaying of dialogue | no known bugs
    private void EndDialogue()
    {
        GetComponent<CanvasGroup>().alpha = 0.0f;
        Time.timeScale = 1;
    }
}
