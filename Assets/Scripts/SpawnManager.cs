using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    bool spawnRequired = true;
    public GameObject spawnPoint;

    private GameObject spawnTarget;
    int count = 0;


    public void SpawnCollectible()
    {
        spawnTarget = Instantiate(spawnPoint);
        spawnTarget.transform.parent = transform;

        MoveCollectible(y1: 0.1f, y2: 0.4f);
    }

    public GameObject GetCollectible() { return spawnTarget; }

    public void MoveCollectible(float x = 13f, float y1 = 0.1f, float y2 = 20f, float z = 13f)
    {
        var position = new Vector3(Random.Range(-x, x), Random.Range(y1, y2), Random.Range(-z, z));
        spawnTarget.transform.localPosition = position;
    }

    //private void Update()
    //{
    //
    //    count++;
    //
    //    if (count == 100)
    //    {
    //        count = 0;
    //        SpawnCollectible();
    //    }
    //}
}
