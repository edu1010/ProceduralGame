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
       
    }

    private void AddRooms(Queue<Room> roomsToCreate, List<Room> createdRooms)
    {
    }

    private void AddNeighbours(Room currentRoom, Queue<Room> roomsToCreate)
    {
    }


    // Retorna coordenades de veïns disponibles (encara no creats).
    private List<Vector2Int> GetAvalibleNeighbors(Room currentRoom)
    {
        // Totes les coordenades candidates al voltant.

        // Filtra les que encara no existeixen al grid.
        return null;
        
    }

    private void CreateNeighbors(List<Vector2Int> availableNeighbors, int numberOfNeighbors, Queue<Room> roomsToCreate)
    {
        // Afegeix sales veïnes aleatòries a la cua.
        
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
        GameManager.Instance.Player.transform.position = _parent.transform.GetChild(0).transform.position;//Podem agafar de room el startplayer tambe

    }


}
