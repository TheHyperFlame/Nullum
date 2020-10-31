using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SGWeapon : MonoBehaviour
{
    public float offset;
    public GameObject ammo;
    public Transform shotDir;
    private float timeShot;
    public float startTime;
    bool isFacingRight = true;
    public float minX = -30f;
    public float maxX = 30f;
    public float jumpDer = 0.0f;
    public float heatRate;
    public int currentAmmo = 1;
    public int magAmmo = 1;
    public int totalAmmo = 12;
    public float reloadDelay = 4f;
    public bool currentReloading = false;
    public bool overHeat = false;
    public float heatDelay;

    [SerializeField]
    private Text ammoCount;
    [SerializeField]
    private Text heatCount;

    public CharacterControllerScript ccs;
    public GameObject OverHeat_Panel;
    public AimingCircle aim;

    void Start()
    {
        OverHeat_Panel.GetComponent<Image>().fillAmount = 0;
    }

    void Update()
    {
        ShootAuto();
        Rotate();
        CursorFlip();
        Shutdown();
        JumpCheck();
        GroundedCheck();
        CountAmmo();
        Reload();
        heatDrop();
        Aiming();
        AimingShutdown();
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

            if (Input.GetMouseButton(0) && (currentAmmo > 0) && (currentReloading == false))
            {
                GameObject bul1 = Instantiate(ammo, shotDir.position, shotDir.rotation);
                GameObject bul2 = Instantiate(ammo, shotDir.position, shotDir.rotation);
                GameObject bul3 = Instantiate(ammo, shotDir.position, shotDir.rotation);
                GameObject bul4 = Instantiate(ammo, shotDir.position, shotDir.rotation);
                bul1.transform.Rotate(0, 0, UnityEngine.Random.Range(minX, maxX));
                bul2.transform.Rotate(0, 0, UnityEngine.Random.Range(minX, maxX));
                bul3.transform.Rotate(0, 0, UnityEngine.Random.Range(minX, maxX));
                bul4.transform.Rotate(0, 0, UnityEngine.Random.Range(minX, maxX));
                timeShot = startTime;
                currentAmmo--;
                if (heatRate < 100)
                {
                    heatRate += 60.0f;
                    Invoke("heatBuild", 0f);
                }
                
                
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
            minX = -30.0f;
            maxX = 30.0f;

        }
    }
    void JumpCheck()
    {
        if ((ccs.isGrounded == false) && (jumpDer < 1))
        {
            jumpDer += 1.0f;
        }
    }
    void GroundedCheck()
    {
        if ((ccs.isGrounded == true))
        {
            jumpDer = 0.0f;
        }
    }

    void CountAmmo()
    {
        if (currentReloading == false)
        {
            ammoCount.text = currentAmmo + " / " + magAmmo + " /// " + totalAmmo;
        }
    }

    void Reload()
    {
        if (currentReloading == false)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (currentAmmo == magAmmo)
                {
                    currentReloading = true;
                    ammoCount.text = "Full mag ejected.";
                    Invoke("reloadCheck", 3);
                }
                else
                {
                    if (totalAmmo > 0)
                    {
                        if ((totalAmmo - (magAmmo - currentAmmo)) >= 0)
                        {
                            currentReloading = true;
                            ammoCount.text = "Reloading...";
                            Invoke("reloadCheck", 3);
                            totalAmmo = totalAmmo - magAmmo;
                            currentAmmo = magAmmo;
                        }
                        else
                        {
                            currentReloading = true;
                            ammoCount.text = "Reloading...";
                            Invoke("reloadCheck", 3);
                            currentAmmo = totalAmmo;
                            totalAmmo = 0;
                        }
                    }
                    else
                    {
                        currentReloading = true;
                        ammoCount.text = "No ammo left.";
                        Invoke("reloadCheck", 3);
                    }
                }
            }
        }
    }
    void heatDrop()
    {
        if ((heatRate > 0) && (timeShot <= 0))
        {
            overHeat = false;
            heatCount.text = "";
            heatRate -= 0.4f;
            OverHeat_Panel.GetComponent<Image>().fillAmount = heatRate / 100;
        }

    }
    void reloadCheck()
    {
        currentReloading = false;
    }

    void Aiming()
    {
        if (Input.GetMouseButton(1) && (ccs.isGrounded == true))
        {
            minX = -15f;
            maxX = 15f;
            ccs.moveSpeed = 2.0f;
        }
    }
    void AimingShutdown()
    {
        if (Input.GetMouseButtonUp(1) || (ccs.isGrounded == false))
        {
            minX = -30f;
            maxX = 30.0f;
            ccs.moveSpeed = 10.0f;

        }
    }
    void heatBuild()
    {
        if(heatRate > 100)
        {
            overHeat = true;
            heatCount.text = "OVERHEAT!!!";
            timeShot = 2;
            ccs.health -= 30;
}
        OverHeat_Panel.GetComponent<Image>().fillAmount = heatRate / 100;
    }


}