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

    private void Start()
    {
       StartCoroutine(Spawner());
    }

    private int RandomPicker()
    {
        return Random.Range(0, eye_SpawnPoint.Length);
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            int spawnPoint_1 = RandomPicker();
            int spawnPoint_2 = RandomPicker();
            while (spawnPoint_1 == spawnPoint_2) spawnPoint_2 = RandomPicker();
            Instantiate(eye, eye_SpawnPoint[spawnPoint_1].position, Quaternion.identity);
            Instantiate(eye, eye_SpawnPoint[spawnPoint_2].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    /*    private async void Spawner()
        {
            while (true)
            {
                int spawnPoint_1 = RandomPicker();
                int spawnPoint_2 = RandomPicker();
                while(spawnPoint_1 == spawnPoint_2) spawnPoint_2 = RandomPicker();
                Instantiate(eye, eye_SpawnPoint[spawnPoint_1].position, Quaternion.identity);
                Instantiate(eye, eye_SpawnPoint[spawnPoint_2].position, Quaternion.identity);

                await Task.Delay((int)(spawnTime * 1000f));
            }
        }*/
}
