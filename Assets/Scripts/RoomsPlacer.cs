﻿using System.Collections;
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
        spawnedRooms = new Room[20, 20];
        spawnedRooms[5, 5] = StartingRoom;

        for(int i = 0; i < 21; i++)
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

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacandPlace.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacandPlace.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacandPlace.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacandPlace.Add(new Vector2Int(x, y + 1));
            }
        }
        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacandPlace.ElementAt(Random.Range(0, vacandPlace.Count));

            if(ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, position.y - 5, 0) * 26;
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selecetedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if(selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selecetedRoom.DoorD.SetActive(false);
        } 
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selecetedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selecetedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selecetedRoom.DoorR.SetActive(false);
        }
        return true;
    }

}
