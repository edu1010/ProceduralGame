using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
// Generador de masmorres amb sales connectades.
public class DungeonGenerator :MonoBehaviour
{
    private Room[,] Grid;
    [SerializeField]
    private int _numberOfRooms;
    GameObject _parent;
    AvalibleRooms avalibleRooms;
    void InitGrid()
    {
        avalibleRooms = Resources.Load<AvalibleRooms>("Rooms/New Avalible Rooms");//carreguem les posibles habitacions que després instanciarem
        int gridSize = 3 * _numberOfRooms; //fem que el grid tingui un tamany suficient.
        Grid = new Room[gridSize, gridSize];
    }
    void ShowDungeon()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (Grid[i, j] != null)
                {
                    CreateRoom(Grid[i, j]);
                }
            }
        }
    }

    public void CreateRoom(Room actualRoom)
    {
        bool n = false, s = false, e = false, w = false;
        foreach (directions dir in actualRoom.Neighbours.Keys)
        {
            switch (dir)
            {
                case directions.N:
                    n = true;
                    break;
                case directions.S:
                    s = true;
                    break;
                case directions.E:
                    e = true;
                    break;
                case directions.W:
                    w = true;
                    break;
                default:
                    break;
            }
        }
        GameObject room = Instantiate(avalibleRooms.GetRoom(n, s, e, w), new Vector2(actualRoom.x * 10, actualRoom.y * 10), Quaternion.identity);
        room.transform.parent = _parent.transform;
    }


    void GenerateDungeon()
    {
        InitGrid();
        // Centra la primera sala al mig del grid.
        Vector2Int coord = new Vector2Int(Grid.GetLength(0) / 2 - 1, Grid.GetLength(1) / 2 - 1);
        Queue<Room> roomsToCreate = new Queue<Room>();

        Room firstRoom = new Room(coord);
        roomsToCreate.Enqueue(firstRoom);

        // Llista de sales creades.
        List<Room> createdRooms = new List<Room>();
        //createdRooms.Add(firstRoom);

        AddRooms(roomsToCreate, createdRooms);
        ConnectNeighbours(createdRooms);
    }

    private void ConnectNeighbours(List<Room> createdRooms)
    {
        foreach(var room in createdRooms)
        {
            //Cada room pot retornar les cordenades del grid del seu voltant
            List<Vector2Int> neighborCoordinates = room.getNeighbourCoordinates();
            foreach(Vector2Int coordinate in neighborCoordinates)
            {
                Room neighbor = this.Grid[coordinate.x, coordinate.y];
                if(neighbor != null)
                {
                    // Connecta amb els veïns existents (1 a 4 possibles).
                    room.Connect(neighbor);
                }
            }
        }
    }

    private void AddRooms(Queue<Room> roomsToCreate, List<Room> createdRooms)
    {
        while(roomsToCreate.Count>0 && createdRooms.Count < _numberOfRooms)
        {
            // Treu la primera sala de la cua per processar-la.
            Room currentRoom = roomsToCreate.Dequeue();
            // Marca la sala al grid per consultar veïns després.
            Grid[currentRoom.x, currentRoom.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbours(currentRoom, roomsToCreate);
        }
    }

    private void AddNeighbours(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> availableNeighbors = GetAvalibleNeighbors(currentRoom);

        int numberOfNeighbors = (int)UnityEngine.Random.Range(1, availableNeighbors.Count);

        CreateNeighbors(availableNeighbors, numberOfNeighbors, roomsToCreate);
    }


    // Retorna coordenades de veïns disponibles (encara no creats).
    private List<Vector2Int> GetAvalibleNeighbors(Room currentRoom)
    {
        // Totes les coordenades candidates al voltant.
        List<Vector2Int> possibleNeighboursCoords = currentRoom.getNeighbourCoordinates();
        // Filtra les que encara no existeixen al grid.
        List<Vector2Int> availableNeighboursCoords = new List<Vector2Int>();
        foreach(Vector2Int coordinate in possibleNeighboursCoords)
        {
            if(this.Grid[coordinate.x,coordinate.y] == null)
            {
                availableNeighboursCoords.Add(coordinate);
            }
        }
        return availableNeighboursCoords;
    }

    private void CreateNeighbors(List<Vector2Int> availableNeighbors, int numberOfNeighbors, Queue<Room> roomsToCreate)
    {
        // Afegeix sales veïnes aleatòries a la cua.
        for(int i = 0; i < numberOfNeighbors; i++)
        {
            int chosen = Random.Range(0, availableNeighbors.Count);
            Vector2Int choosenNeighbour = availableNeighbors[chosen];
            roomsToCreate.Enqueue( new Room(choosenNeighbour));
            availableNeighbors.Remove(choosenNeighbour);

        }
    }


    private void Start()
    {
        CreateDungeon(_numberOfRooms);
    }
    public void CreateDungeon(int numberOfRooms)
    {
        _parent = new GameObject("levelDungeon");
        _numberOfRooms = numberOfRooms;
        GenerateDungeon();
        ShowDungeon();
    }

    
}
