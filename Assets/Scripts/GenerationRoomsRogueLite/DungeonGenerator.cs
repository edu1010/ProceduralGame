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
        //Aqui crearem les sales al mon de unity
        //hem de recorre el grid i per cada posició diferent de null cridar a que es crei una sala.
        //pista al ser una matriu que no te pq ser cuadrada, podem fer  Grid.GetLength(0) i  Grid.GetLength(1) com a limit de cada bucle anidat
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
        // per cada room creada
        // obtenir la llista de coordenades dels seus veins possibles =>  room.getNeighbourCoordinates()
        // recórrer aquestes coordenades
        // mirar al grid si hi ha una room en aquella posició o es null
        // si n'hi ha una, connectar la room actual amb la room veina => room.Connect(neighbor);
    }

    private void AddRooms(Queue<Room> roomsToCreate, List<Room> createdRooms)
    {
        //mentra roomsTocreate sea > que 0 i el nombre de rooms creades sigui inferior al total
        //hem de treure de la cua una room
        // guardar al grid que en aquelles cordenades hi sera aquesta room
        //afegir a la llista la room que hem tret de la cua.
        //cridar a que s'afegeixen els veins d'aquesta room
    }

    private void AddNeighbours(Room currentRoom, Queue<Room> roomsToCreate)
    {
        //obtenim la llista de cordenades de veïns que encara estiguin lliures.
        //calculem un numero aleatori de veins que tindra aquesta sala entre 1 i el maxim de veïns disponibles.
        //cridem a que es crein els veïns de veritat
    }


    // Retorna coordenades de veïns disponibles (encara no creats).
    private List<Vector2Int> GetAvalibleNeighbors(Room currentRoom)
    {
        //mirem les posicions del voltant de la sala amb currentRoom.getNeighbourCoordinates();
        //fem una llista per guardar les cordinades que estaran disponibles.
        //recorrem els possibles veïns i mirem si aquelles cordinades al grid son null, si ho son afegim a la llista que retornarem

       
        return null;
        
    }

    private void CreateNeighbors(List<Vector2Int> availableNeighbors, int numberOfNeighbors, Queue<Room> roomsToCreate)
    {
        //fem un for per la cuantitat de veïns que volem
        //per ferlo més aleatori, dels availableNeighbors escollim un  a l'atzar
        //afegim a la  cua roomsToCreate una nova room amb les cordenades d'aquest veï
        //per no afegir al grid que es procesara despre´s dos vegades el mateix aquesta selecio l'em de elimianr d'availableNeighbors

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
