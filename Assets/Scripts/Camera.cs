using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform Player;
    private Vector3 initialPlayerPos;
    private Vector3 initialCameraPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPlayerPos = Player.position;
        initialCameraPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var playerOffset = Player.position - initialPlayerPos;
        transform.position = initialCameraPos + playerOffset;
    }
}
