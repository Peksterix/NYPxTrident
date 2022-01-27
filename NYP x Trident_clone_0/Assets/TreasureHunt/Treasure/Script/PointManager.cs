using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int ID;
    public string Name;
    public int Score;
    public string Result;
    public Player(int ID, string Name, int Score, string Result)
    {
        this.ID = ID;
        this.Name = Name;
        this.Score = Score;
        this.Result = Result;
    }
}
public class PointManager : MonoBehaviour
{
    public List<Player> PlayerList = new List<Player>();

    private void Update()
    {
        foreach (var chara in PlayerList)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log(chara.ID);
            }
        }
    }
}
