using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //variables
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<AudioClip> sounds;
    private Queue<bool> hasSound;
    private Queue<bool> hasImage;
    private Queue<Sprite> portraits;
    //UI
    public CanvasGroup group;
    public Text nameText;
    public Text sentenceText;
    public UnityEngine.UI.Image characterPortrait;

    //Images
    public List<Sprite> allPortraits;

    //NPC stuff
    private AudioSource aSource;
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        hasSound = new Queue<bool>();
        sounds = new Queue<AudioClip>();
        hasImage = new Queue<bool>();
        portraits = new Queue<Sprite>();
        aSource = GetComponent<AudioSource>();
    }

    //searches for the correct image
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
    public void StartDialogue(Dialogue[] array)
    {
        GetComponent<CanvasGroup>().alpha = 1.0f;
        Time.timeScale = 0;
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
        hasImage.Clear();
        portraits.Clear();
        foreach (Dialogue dialogue in array)
        {
            sentences.Enqueue(dialogue.sentence);
            names.Enqueue(dialogue.name);
            hasImage.Enqueue(dialogue.hasImage);
            //checking if it has image
            if (dialogue.hasImage)
            {
                portraits.Enqueue(FindSprite(dialogue.imageName));
            }
           //checking if it has sound
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
        if (hasImage.Dequeue())
        {
            characterPortrait.sprite = portraits.Dequeue();
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
        GetComponent<CanvasGroup>().alpha = 0.0f;
        Time.timeScale = 1;
    }
}
