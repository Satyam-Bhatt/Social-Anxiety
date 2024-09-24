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

    public string heading;

    [TextArea(5, 10)]
    public string beforeBW;
    [TextArea(5, 10)]
    public string afterBW;

    public AudioCaptionMix[] auddioCaption = new AudioCaptionMix[2];
}
