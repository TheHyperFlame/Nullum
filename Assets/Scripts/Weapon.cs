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
    bool isFacingRight = true;
    public float minX = 0.0f;
    public float maxX = 0.0f;

    public CharacterControllerScript ccs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootAuto();
        Rotate();
        CursorFlip();
        Shutdown();
    }

    void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y, rotateZ + offset);
    }
   
    void ShootAuto()
    {
        if (timeShot <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bul = Instantiate(ammo, shotDir.position, shotDir.rotation);
                bul.transform.Rotate(0, 0, UnityEngine.Random.Range(minX * (Mathf.Abs(ccs.move) * 2 + 1), maxX * (Mathf.Abs(ccs.move) * 2 + 1)));
                if ((minX >=-10.0f) || (maxX <=10.0f))
                    {
                    minX -= 0.5f;
                    maxX += 0.5f;
                    }
                timeShot = startTime;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }
    void CursorFlip()
    {
        if ((Input.mousePosition.x > Screen.width * 0.5f) != isFacingRight)
        {
            transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            isFacingRight = !isFacingRight;
        }
    }
    void Shutdown()
    {
        if (Input.GetMouseButtonUp(0))
        {
            minX = 0.0f;
            maxX = 0.0f;
        }
    }


}
