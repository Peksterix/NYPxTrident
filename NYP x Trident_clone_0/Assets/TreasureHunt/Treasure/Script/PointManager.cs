using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int ID;
    public string Name;
    public int Score;
    public Player(int ID, string Name, int Score)
    {
        this.ID = ID;
        this.Name = Name;
        this.Score = Score;
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
                Debug.Log(chara.Name);
            }
        }
    }
}
