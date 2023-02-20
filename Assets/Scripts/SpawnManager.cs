using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawnPoint;

    private GameObject spawnTarget;

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
}
