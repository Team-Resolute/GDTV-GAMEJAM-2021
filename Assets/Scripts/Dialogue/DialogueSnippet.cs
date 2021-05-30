using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speaker {None=0, Player=1, Sandman=2, Mother=3}
public class DialogueSnippet
{
    
    public Speaker speaker;
    public string speech;

    public DialogueSnippet(Speaker speaker, string speech = "Default", DialogueSnippet nextSnippet = null)
    {
        this.speaker = speaker;
        this.speech = speech;
    }

}
