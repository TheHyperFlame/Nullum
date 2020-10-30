using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    SpriteRenderer Sr;
    public GameObject particle;
    GameObject player;
    private int range = 15; 
    private int speed = 3;
    private int rangestop = 2;  
    private void Start()
    {
        Sr = this.GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        float Distans = Vector2.Distance(transform.position, player.transform.position);
        if (Distans < range && Distans > rangestop)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (transform.position.x > player.transform.position.x)
                Sr.flipX = true;
            else Sr.flipX = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log ("касание");
        if (collision.gameObject.GetComponent<CharacterController>())
        Death();
    }
    private void Death()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
