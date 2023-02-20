using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

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

        MoveCollectible();
    }

    public GameObject GetCollectible() { return spawnTarget; }

    public void MoveCollectible()
    {
        float offset = Academy.Instance.EnvironmentParameters.GetWithDefault("collectible_offset", 1f);

        var xSign = (Random.Range(0, 2) * 2 - 1);
        var zSign = (Random.Range(0, 2) * 2 - 1);

        var x = Random.Range(2, (3f + offset)) * xSign;
        var z = Random.Range(2, (3f + offset)) * zSign;

        Vector3 position = new Vector3(x, offset, z);

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
