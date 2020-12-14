using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sentence : ScriptableObject
{
    public Dialogue dialogue;

    [Tooltip("Available only when Sentence has no options")]
    public Sentence nextSentence;

    public List<Choice> options = new List<Choice>();

    public bool HasOptions()
    {
        //If options length is 0 then there are no options
        return options.Count != 0;
    }

}

[System.Serializable]
public class Choice
{
    [TextArea(3, 10)]
    public string text;
    public Sentence nextSentence;
}

// Sentence.dialogue shows up on screen
// Then sets currentSentence to Sentence.nextSentence
// CurrentDialogue  = Sentence.dialogue
// Keeps doing that until CurrentDialogue == null

// If options.Count > 0