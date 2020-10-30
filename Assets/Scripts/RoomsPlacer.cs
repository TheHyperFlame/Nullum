using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs;
    public Room StartingRoom;

    private Room[,] spawnedRooms;


    private void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        for(int i = 0; i < 12; i++)
        {
            PlaceOneRoom();
        }

    }
    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacandPlace = new HashSet<Vector2Int>();
        for(int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for(int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0);
                int maxY = spawnedRooms.GetLength(1);

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacandPlace.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacandPlace.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacandPlace.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacandPlace.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        Vector2Int position = vacandPlace.ElementAt(Random.Range(0, vacandPlace.Count));
        newRoom.transform.position = new Vector3(position.x - 5, position.y - 5, 0) * 12;

        spawnedRooms[position.x, position.y] = newRoom;
    }

}
