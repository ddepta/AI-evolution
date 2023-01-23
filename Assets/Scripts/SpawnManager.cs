using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    bool spawnRequired = true;
    public GameObject spawnPoint;
    int count = 0;

    public void SpawnCollectible()
    {
        var position = new Vector3(Random.Range(-9, 9), 0.2f, Random.Range(-9, 9));
        var pointObject = Instantiate(spawnPoint);

        pointObject.transform.parent = transform;
        pointObject.transform.localPosition = position;
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
