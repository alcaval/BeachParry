using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject EZigZag = null;
    public GameObject EMeteor = null;
    public GameObject EFish = null;

    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
      //  startRoutines();
    }

    IEnumerator ZigZagRoutine()
    {
        while (gameStarted)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-3,3), 11);
            Instantiate(EZigZag, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator MeteorRoutine()
    {
        while (gameStarted)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-3, 3), 11);
            Instantiate(EMeteor, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator FishRoutine()
    {
        while (gameStarted)
        {
            Vector2 spawnPos = new Vector2(Random.Range(-3, 3), -11);
            Instantiate(EFish, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(4);
        }
    }

    public void startRoutines()
    {
        gameStarted = true;
        StartCoroutine(ZigZagRoutine());
        StartCoroutine(MeteorRoutine());
        StartCoroutine(FishRoutine());
    }

    public void stopRoutines()
    {
        gameStarted = false;
        StartCoroutine(DestroyEnemies("EnemyDiagonal"));
        StartCoroutine(DestroyEnemies("EnemyMeteorito"));
        StartCoroutine(DestroyEnemies("EnemyZigZag")); 
    }

    IEnumerator DestroyEnemies(string tag)
	{
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
        if (allObjects != null)
        {
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
            }
        }
        yield return null; 
    }
}
