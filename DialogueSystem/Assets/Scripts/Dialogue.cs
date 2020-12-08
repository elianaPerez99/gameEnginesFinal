using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    public string name;
    [SerializeField]
    [TextArea(3,10)]
    public string sentence;
    [SerializeField]
    public AudioClip sound;
}
