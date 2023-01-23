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
        var position = new Vector3(Random.Range(-9, 9), 0.2f, Random.Range(-9, 9));
        spawnTarget = Instantiate(spawnPoint);

        spawnTarget.transform.parent = transform;
        spawnTarget.transform.localPosition = position;
    }

    public GameObject GetCollectible() { return spawnTarget; }

    public void MoveCollectible()
    {
        var position = new Vector3(Random.Range(-9, 9), 0.2f, Random.Range(-9, 9));
        spawnTarget.transform.localPosition = position;
    }

    //private void Update()
    //{
    //
    //    count++;
    //
    //    if(count == 100)
    //    {
    //        count = 0;
    //        SpawnCollectible();
    //    }
    //}
}
