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
    public float jumpDer = 0.0f;
    public float heatRate;
    public int currentAmmo = 1;
    public int magAmmo = 1;
    public int totalAmmo = 12;
    public float reloadDelay = 4f;
    public bool currentReloading = false;
    public bool overHeat = false;
    public float heatDelay;
    public bool needToCorrectAim = false;

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
                GameObject bul5 = Instantiate(ammo, shotDir.position, shotDir.rotation);
                bul1.transform.Rotate(0f, 0f, UnityEngine.Random.Range(-aim.radius * 2.5f, aim.radius * 2.5f));
                bul2.transform.Rotate(0f, 0f, UnityEngine.Random.Range(-aim.radius * 2.5f, aim.radius * 2.5f));
                bul3.transform.Rotate(0f, 0f, UnityEngine.Random.Range(-aim.radius * 2.5f, aim.radius * 2.5f));
                bul4.transform.Rotate(0f, 0f, UnityEngine.Random.Range(-aim.radius * 2.5f, aim.radius * 2.5f));
                bul5.transform.Rotate(0f, 0f, UnityEngine.Random.Range(-aim.radius * 2.5f, aim.radius * 2.5f));
                timeShot = startTime;
                currentAmmo--;
                if (aim.radius <= 8)
                {
                    aim.radius += 2f;
                }
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
        if ((!Input.GetMouseButton(0) || (overHeat == true) || (currentReloading == true) || (timeShot > 0)) && (ccs.isGrounded == true) && (ccs.move == 0))
        {
            if ((aim.radius > 3 * (jumpDer + 1)))
            {
                aim.radius -= 0.01f;
            }
        }
    }
    void JumpCheck()
    {
            if ((ccs.isGrounded == false) && (jumpDer < 1))
            {
                jumpDer += 1.0f;
                aim.radius *= (jumpDer + 1);
                needToCorrectAim = true;
            }
        }
    void GroundedCheck()
    {
            if ((ccs.isGrounded == true) && (needToCorrectAim == true))
            {
                jumpDer = 0.0f;
                while (aim.radius > 3)
                {
                    aim.radius -= 0.01f;
                }
                needToCorrectAim = false;
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
        if ((heatRate > 0))
        {
            overHeat = false;
            heatCount.text = "";
            heatRate -= 0.2f;
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
                if (aim.radius > 1.5f)
                {
                    aim.radius *= 0.9f;
                }
                ccs.moveSpeed = 3.0f;
            }
        }
    void AimingShutdown()
    {
            if (!Input.GetMouseButton(1) || (ccs.isGrounded == false))
            {
                if (aim.radius < jumpDer + 3)
                {
                    aim.radius = jumpDer + 3;
                }
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