using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject ammo;
    public Transform shotDir;
    private float timeShot;
    public float startTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootAuto();
        Rotate();
    }

    void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        
    }
   
    void ShootAuto()
    {
        if (timeShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(ammo, shotDir.position, transform.rotation);
                timeShot = startTime;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }

}
