using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.ForceMeshUpdate();

        TMP_TextInfo textInfo = text.textInfo;

        Vector3[] verts = text.mesh.vertices;

        int vertCount = 0;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            verts[0 + i] = textInfo.characterInfo[i].bottomLeft;
            verts[1 + i] = textInfo.characterInfo[i].topLeft;
            verts[2 + i] = textInfo.characterInfo[i].topRight;
            verts[3 + i] = textInfo.characterInfo[i].bottomRight;
        }
        
    }
}
