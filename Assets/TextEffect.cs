using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextEffect : MonoBehaviour
{
    private TMP_Text text;
    private Vector3[] verts;

    public float amp, freq, shakeAmount, delay;

    [SerializeField] private Style style;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (style == Style.style1)
        { 
            text.ForceMeshUpdate();

            TMP_TextInfo textInfo = text.textInfo;

            verts = text.mesh.vertices;

            //<---------Style 2----------->
            StartCoroutine(Shaker(delay));        
        }
    }

    private void Update()
    {
        if (style == Style.style2)
        {
            text.ForceMeshUpdate();

            verts = text.mesh.vertices;
            //<---------Style 1--------->
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] += new Vector3((Mathf.Sin((Time.time * freq * 3f + verts[i].x)) * amp), (Mathf.Sin((Time.time * freq + verts[i].x)) * amp), 0);
            }
        }
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }

    IEnumerator Shaker(float delay)
    {
        while (true)
        {
            text.ForceMeshUpdate();

            verts = text.mesh.vertices;

            int vertCount = 0;
            for (int i = 0; i < verts.Length; i += 4)
            {
                float shakeX = Random.Range(-shakeAmount, shakeAmount);
                float shakeY = Random.Range(-shakeAmount, shakeAmount);

                verts[vertCount + 0] += new Vector3(shakeX, shakeY, 0);
                verts[vertCount + 1] += new Vector3(shakeX, shakeY, 0);
                verts[vertCount + 2] += new Vector3(shakeX, shakeY, 0);
                verts[vertCount + 3] += new Vector3(shakeX, shakeY, 0);

                vertCount += 4;
            }

            text.mesh.vertices = verts;
            text.canvasRenderer.SetMesh(text.mesh);

            yield return new WaitForSeconds(delay);        
        }
    }

    public enum Style
    { 
        style1,
        style2
    };
}
