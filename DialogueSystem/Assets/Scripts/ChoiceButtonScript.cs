using UnityEngine;

public class ChoiceButtonScript : MonoBehaviour
{
    public int index;

    public void OnClicked()
    {
        FindObjectOfType<DialogueManager>().OnClickChoice(index);
    }

}
