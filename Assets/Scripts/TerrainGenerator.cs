using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject Cell;
    public Transform Zero;
    public int Height, Width;
    void Start()
    {
        Generate();
    }
    public void Generate()
    {
        int groundHeigth = 5;
        for(int x = 0; x < Width; x++)
        {
            if (x % 3 == 0)
            {
                groundHeigth += Random.Range(-1, 2);
            }

            for(int y = groundHeigth; y > 0; y--)
            {
                var cell = Instantiate(Cell, Zero);
                cell.transform.localPosition = new Vector3(x, y, 0);
            }
        }
    }

}
