using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioCaptionMix
{
    public AudioClip clip;
    [TextArea(3,10)]
    public string caption;
}
