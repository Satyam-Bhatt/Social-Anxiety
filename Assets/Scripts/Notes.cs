using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public bool confidneceIncreased = false;

    [TextArea(3, 10)]
    public string beforeBW;
    [TextArea(3, 10)]
    public string afterBW;
}
