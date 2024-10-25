using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EyeSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] eye_SpawnPoint;
    [SerializeField] private GameObject eye;
    [SerializeField] private float spawnTime = 2f;

    private GameObject obj;

    private void OnEnable()
    {
        if (transform.Find("Eye Keepes") == null)
        { 
            obj = new GameObject("Eye Keepes")
            {
                transform =
                {
                    position = new Vector3(0, 0, 0)
                }
            };

            obj.transform.SetParent(this.transform.parent);        
        }

        StartCoroutine(Spawner());
        StartCoroutine(SpawnSpeed());
        StartCoroutine(DeleteGameObject());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Destroy(obj);
    }

    private int RandomPicker()
    {
        return Random.Range(0, eye_SpawnPoint.Length);
    }

    private int numberToSpawn()
    {
        var percentNum = Random.Range(0, 101);

        return percentNum <= 20 ? 3 : 2;
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            if (numberToSpawn() == 3)
            {
                int spawnPoint_1 = RandomPicker();
                int spawnPoint_2 = RandomPicker();
                int spawnPoint_3 = RandomPicker();

                while (spawnPoint_1 == spawnPoint_2 || spawnPoint_2 == spawnPoint_3 || spawnPoint_1 == spawnPoint_3)
                { 
                    spawnPoint_2 = RandomPicker();
                    spawnPoint_3 = RandomPicker();
                }

                GameObject g1 = Instantiate(eye, eye_SpawnPoint[spawnPoint_1].position, Quaternion.identity);
                GameObject g3 = Instantiate(eye, eye_SpawnPoint[spawnPoint_3].position, Quaternion.identity);
                GameObject g2 = Instantiate(eye, eye_SpawnPoint[spawnPoint_2].position, Quaternion.identity);

                g1.transform.SetParent(obj.transform);
                g2.transform.SetParent(obj.transform);
                g3.transform.SetParent(obj.transform);
            }
            else 
            { 
                int spawnPoint_1 = RandomPicker();
                int spawnPoint_2 = RandomPicker();
                while (spawnPoint_1 == spawnPoint_2)
                {
                    spawnPoint_2 = RandomPicker();
                }
            
                GameObject g1 = Instantiate(eye, eye_SpawnPoint[spawnPoint_1].position, Quaternion.identity);
                GameObject g2 = Instantiate(eye, eye_SpawnPoint[spawnPoint_2].position, Quaternion.identity);

                g1.transform.SetParent(obj.transform);
                g2.transform.SetParent(obj.transform);
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }

    private IEnumerator SpawnSpeed()
    { 
        while (true)
        {
            yield return new WaitForSeconds(10f);

            if(spawnTime > 0.6f)
            {
                spawnTime -= 0.2f;
            }
        }
    }

    private IEnumerator DeleteGameObject()
    {
        while (true)
        { 
            yield return new WaitForEndOfFrame();

            if (obj.transform.childCount > 40) {
                for (int i = 0; i <= 10; i++)
                {
                    Destroy(obj.transform.GetChild(i).gameObject);
                }
            }
     
        }
    }
}
