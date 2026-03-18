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
        avalibleRooms = Resources.Load<AvalibleRooms>("Rooms/New Avalible Rooms");//carreguem les possibles habitacions que després instanciarem
        int gridSize = 3 * _numberOfRooms; //fem que el grid tingui un tamany suficient.
        Grid = new Room[gridSize, gridSize];
    }
    void ShowDungeon()
    {
        //Aquí crearem les sales al món de Unity.
        //Hem de recórrer el grid i, per cada posició diferent de null, cridar a que es creï una sala.
        //Pista: en ser una matriu que no té per què ser quadrada, podem fer Grid.GetLength(0) i Grid.GetLength(1) com a límit de cada bucle anidat.
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
        //Per cada room creada,
        //obtenir la llista de coordenades dels seus veïns possibles => room.getNeighbourCoordinates()
        //recórrer aquestes coordenades
        //mirar al grid si hi ha una room en aquella posició o és null
        //si n'hi ha una, connectar la room actual amb la room veïna => room.Connect(neighbor);
    }

    private void AddRooms(Queue<Room> roomsToCreate, List<Room> createdRooms)
    {
        //Mentre roomsToCreate sigui > 0 i el nombre de rooms creades sigui inferior al total,
        //hem de treure de la cua una room.
        //Guardar al grid que en aquelles coordenades hi serà aquesta room.
        //Afegir a la llista la room que hem tret de la cua.
        //Cridar a que s'afegeixin els veïns d'aquesta room.
    }

    private void AddNeighbours(Room currentRoom, Queue<Room> roomsToCreate)
    {
        //Obtenim la llista de coordenades de veïns que encara estiguin lliures.
        //Calculem un número aleatori de veïns que tindrà aquesta sala entre 1 i el màxim de veïns disponibles.
        //Cridem a que es creïn els veïns de veritat.
    }


    //Retorna coordenades de veïns disponibles (encara no creats).
    private List<Vector2Int> GetAvalibleNeighbors(Room currentRoom)
    {
        //Mirem les posicions del voltant de la sala amb currentRoom.getNeighbourCoordinates();
        //Fem una llista per guardar les coordenades que estaran disponibles.
        //Recorrem els possibles veïns i mirem si aquelles coordenades al grid són null; si ho són, les afegim a la llista que retornarem.

       
        return null;
        
    }

    private void CreateNeighbors(List<Vector2Int> availableNeighbors, int numberOfNeighbors, Queue<Room> roomsToCreate)
    {
        //Fem un for per la quantitat de veïns que volem.
        //Per fer-ho més aleatori, dels availableNeighbors escollim un a l'atzar.
        //Afegim a la cua roomsToCreate una nova room amb les coordenades d'aquest veí.
        //Per no afegir al grid que es processarà després dos cops el mateix, aquesta selecció l'hem d'eliminar d'availableNeighbors.

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
