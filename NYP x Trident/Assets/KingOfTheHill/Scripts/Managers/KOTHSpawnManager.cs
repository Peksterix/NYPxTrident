using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;

public class KOTHSpawnManager : Singleton<KOTHSpawnManager>
{
    [SerializeField]
    public List<GameObject> SpawnPoints = new List<GameObject>();
}
