using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public enum  Type { 
        Notes,
        Awards
    };

    public Type type;
    
    public bool confidneceIncreased = false;

    [TextArea(3, 10)]
    public string beforeBW;
    [TextArea(3, 10)]
    public string afterBW;

    public AudioCaptionMix[] auddioCaption = new AudioCaptionMix[2];

    public AudioClip audio_BeforeBW;
    public AudioClip audio_AfterBW;
}
