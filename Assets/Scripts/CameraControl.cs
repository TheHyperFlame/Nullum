using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float slip = 1.8f;
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

    private void Update()
    {
        if (player)
        {
            int currentx = Mathf.RoundToInt(player.position.x);
            if (currentx > Lastx) isLeft = false;
            else if (currentx < Lastx) isLeft = true;
            Lastx = Mathf.RoundToInt(player.position.x);
            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, slip * Time.deltaTime);
            transform.position = currentPosition;
        }
    }
}
