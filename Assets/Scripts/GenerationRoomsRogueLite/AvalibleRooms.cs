 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(menuName = "LevelPrefabs/AvalibleRooms")]
public class AvalibleRooms : ScriptableObject
{
    public GameObject[] Room_E;
    public GameObject[] Room_EN;
    public GameObject[] Room_ENS;
    public GameObject[] Room_ENSW;
    public GameObject[] Room_ENW;
    public GameObject[] Room_ES;
    public GameObject[] Room_ESW;
    public GameObject[] Room_EW;
    public GameObject[] Room_N;
    public GameObject[] Room_NS;
    public GameObject[] Room_NSW;
    public GameObject[] Room_NW;
    public GameObject[] Room_S;
    public GameObject[] Room_SW;
    public GameObject[] Room_W;

    public GameObject GetRoom(bool n, bool s, bool e, bool w)
    {
        if (n)
        {
            if (s)
            {
                if (e)
                {
                    if (w)
                    {
                        return Room_ENSW[Random.Range(0, Room_ENSW.Length)];
                    }
                    else
                    {
                        return Room_ENS[Random.Range(0, Room_ENS.Length)];
                    }
                }
                else
                {
                    if (w)
                    {
                        return Room_NSW[Random.Range(0, Room_NSW.Length)];
                    }
                    else
                    {
                        return Room_NS[Random.Range(0, Room_NS.Length)];
                    }
                }
            }
            else
            {
                if (e)
                {
                    if (w)
                    {
                        return Room_ENW[Random.Range(0, Room_ENW.Length)];
                    }
                    else
                    {
                        return Room_EN[Random.Range(0, Room_EN.Length)];
                    }
                }
                else
                {
                    if (w)
                    {
                        return Room_NW[Random.Range(0, Room_NW.Length)];
                    }
                    else
                    {
                        return Room_N[Random.Range(0, Room_N.Length)];
                    }
                }
            }
        }
        else if (s)
        {
            if (e)
            {
                if (w)
                {
                    return Room_ESW[Random.Range(0, Room_ESW.Length)];
                }
                else
                {
                    return Room_ES[Random.Range(0, Room_ES.Length)];
                }
            }
            else
            {
                if (w)
                {
                    return Room_SW[Random.Range(0, Room_SW.Length)];
                }
                else
                {
                    return Room_S[Random.Range(0, Room_S.Length)];
                }
            }
        }
        else if (e)
        {
            if (w)
            {
                return Room_EW[Random.Range(0, Room_EW.Length)];
            }
            else
            {
                return Room_E[Random.Range(0, Room_E.Length)];
            }
        }
        else if (w)
        {
            return Room_W[Random.Range(0, Room_W.Length)];
        }

        return null;
    }

}
