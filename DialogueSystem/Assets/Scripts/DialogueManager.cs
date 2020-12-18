using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //variables
    private Sentence currentSentence;

    //UI

    [Header("Canvas Objects")]

    // Canvas
    [Tooltip("Set custom Canvas group to hold your dialogue system")]
    [SerializeField] private CanvasGroup group;

    [Tooltip("Set custom Canvas group to hold your choice UI")]
    [SerializeField] private CanvasGroup ChoiceCanvas;

    [Header("Text Objects")]

    // Text
    [Tooltip("Set Text object that will display your name")]
    [SerializeField] private Text nameText;

    [Tooltip("Set Text object that will display your sentance")]
    [SerializeField] private Text sentenceText;

    [Header("Custom Text Settings")]

    // Text Customization
    [Tooltip("Set font fot text")]
    [SerializeField] private Font dialogueFont;

    [Tooltip("Set size for text")]
    [SerializeField] private int dialogueTextSize;

    [Tooltip("Set color for text")]
    [SerializeField] private Color dialogueColor;

    [Header("Other")]

    // Button that contimues dialogue
    [Tooltip("Set custom Key to continue through the dailogue")]
    [SerializeField] private KeyCode dialogueButton;

    [Tooltip("Set character portrait")]
    [SerializeField] private Image characterPortrait;


    //Images
    [Tooltip("Set array of character portraits")]
    public List<Sprite> allPortraits;

    //NPC stuff
    private AudioSource aSource;

    [Tooltip("Set array of buttons for branching dialogue")]
    [SerializeField] private Button[] choiceButtons;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChoiceCanvas.alpha = 0f;
        ChoiceCanvas.blocksRaycasts = false;
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
        if (group.alpha == 1 && Input.GetKeyDown(dialogueButton))
        {
            if (currentSentence != null && currentSentence.HasOptions()) return;

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
        CustomText(nameText);
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


        if (!currentSentence.HasOptions())
            currentSentence = currentSentence.nextSentence;
        else
            DisplayChoices();
    }

    // have the letters in the sentence appear on the screen one by one | no known bugs
    IEnumerator TypeSentence(string sentence)
    {
        CustomText(sentenceText);
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
    private void CustomText(Text text)
    {
        text.color = dialogueColor;
        text.font = dialogueFont;
        text.fontSize = dialogueTextSize;
    }

    private void DisplayChoices()
    {
        ChoiceCanvas.alpha = 1f;
        ChoiceCanvas.blocksRaycasts = true;

        if (currentSentence.options.Count <= choiceButtons.Length)
        {
            for (int i = 0; i < currentSentence.options.Count; i++)
            {
                choiceButtons[i].transform.GetChild(0).GetComponent<Text>().text = currentSentence.options[i].text;
                choiceButtons[i].gameObject.SetActive(true);
            }
        }

    }

    private void HideChoices()
    {
        foreach (Button b in choiceButtons)
        {
            b.gameObject.SetActive(false);
        }

        ChoiceCanvas.alpha = 0f;
        ChoiceCanvas.blocksRaycasts = false;

    }

    public void OnClickChoice(int index)
    {
        HideChoices();

        currentSentence = currentSentence.options[index].nextSentence;
        DisplayNextSentence();
    }
}
