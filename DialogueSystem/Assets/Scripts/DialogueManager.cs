using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    //variables
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<AudioClip> sounds;
    private Queue<bool> hasSound;
    //UI
    public CanvasGroup group;
    public Text nameText;
    public Text sentenceText;

    //NPC stuff
    private AudioSource aSource;
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        hasSound = new Queue<bool>();
        sounds = new Queue<AudioClip>();
        aSource = GetComponent<AudioSource>();
    }

    //makes the canvas appear and starts the text | no known bugs
    public void StartDialogue(Dialogue[] array)
    {
        LoadDialogue(array);
        DisplayNextSentence();
    }

    private void Update()
    {
        if (group.alpha == 1 && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    //loads dialogue array into the correct places | no known bugs
    public void LoadDialogue(Dialogue[] array)
    {
        sentences.Clear();
        names.Clear();
        hasSound.Clear();
        sounds.Clear();
        foreach (Dialogue dialogue in array)
        {
            sentences.Enqueue(dialogue.sentence);
            names.Enqueue(dialogue.name);
            if (!dialogue.sound.Equals(null))
            {
                hasSound.Enqueue(true);
                sounds.Enqueue(dialogue.sound);
            }
            else
            {
                hasSound.Enqueue(false);
            }
        }
    }

    //displays the next sentence | no known bugs
    private void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        aSource.Stop();
        nameText.text = names.Dequeue();
        if (hasSound.Dequeue())
        {
            aSource.clip = sounds.Dequeue();
            aSource.Play();
        }
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentences.Dequeue()));
        
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
        //we can put something here later
    }
}
