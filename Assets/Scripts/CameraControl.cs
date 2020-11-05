using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float slip = 1.4f;
    public Vector2 offset = new Vector2(2f, 2f);
    public bool isLeft;
    private Transform player;
    private int Lastx;

    private void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    public void FindPlayer(bool playerIsLeft)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Lastx = Mathf.RoundToInt(player.position.x);
        if (playerIsLeft) transform.position = new Vector3(player.position.x - offset.x, player.position.y - offset.y, transform.position.z);
        else transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }

    Vector3 target, currentPosition;

    private void Update()
    {
        if (player)
        {
            int currentx = Mathf.RoundToInt(player.position.x);
            if (currentx > Lastx) isLeft = false;
            else if (currentx < Lastx) isLeft = true;
            Lastx = Mathf.RoundToInt(player.position.x);
            
            target = isLeft ? new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z) : new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            float dist = transform.position.y - player.transform.position.y;
            Vector3 currentPosition = new Vector3(0, 0, 0);

            currentPosition = (Mathf.Abs(dist) > 6) ? Vector3.Lerp(transform.position, target, slip * Time.deltaTime * 4.6f): Vector3.Lerp(transform.position, target, slip * Time.deltaTime * 2);
               
            transform.position = currentPosition;

        }
    }
}
