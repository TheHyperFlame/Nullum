using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AimingCircle : MonoBehaviour
{
    int guiDepth = -2;
    public float radius = 1f;
    public GameObject shotDir;
    public GameObject AimCircle;
    public float startingRadius = 1f;
    public float distance;
    public float startingDistance;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        GUI.depth = guiDepth;
        startingDistance = (Vector3.Distance(shotDir.transform.position, AimCircle.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
        CircleScale();
    }
    void Follow()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z); // переменной записываються координаты мыши по иксу и игрику
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
        transform.position = objPosition;
    }
    void CircleScale()
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

}
