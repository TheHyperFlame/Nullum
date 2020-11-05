using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraC : MonoBehaviour
{
    private Transform Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        UpdateCamPos();
    }

    void UpdateCamPos()
    {
        Vector3 newPos = Player.position;
        transform.position = newPos;
    }

}
