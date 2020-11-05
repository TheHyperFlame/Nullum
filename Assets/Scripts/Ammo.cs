using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float destroyTime = 1;
    bool hitObject = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (hitObject == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        hitObject = true;
        Invoke("Destroy", 1);
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
