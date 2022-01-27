using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bamboo.Utility;
using TMPro;
using Mirror;
using DG.Tweening;
using System.Security.Cryptography;
using UnityEngine.Events;

namespace Bamboo.WGT
{
    public class WGTPointManager : Singleton<WGTPointManager>
    {
        [SerializeField] GameObject pointPrefab;
        [SerializeField] int minNumPointsPerSpawn = 1;
        [SerializeField] int maxNumPointsPerSpawn = 5;
        [SerializeField] float minSpawnDuration = 3.0f;
        [SerializeField] float maxSpawnDuration = 8.0f;

        [SerializeField] float pointMinPoints = 10.0f;
        [SerializeField] float pointMaxPoints = 50.0f;

        Transform[] WGTPoints = new Transform[0];

        void Start()
        {
            if (!NetworkServer.active)
            {
                Destroy(this);
                return;
            }

            WGTPoints = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                WGTPoints[i] = transform.GetChild(i);
            }
        }

        public void StartSpawningCoroutine()
        {
            StartCoroutine(peepeepoopoo());

            IEnumerator peepeepoopoo()
            {
                while(true)
                {
                    int numToSpawn = UnityEngine.Random.Range(minNumPointsPerSpawn, maxNumPointsPerSpawn + 1);

                    for (int i = 0; i < numToSpawn; i++)
                        SpawnPoint();


                    yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDuration, maxSpawnDuration));
                }
            }
        }

        void SpawnPoint()
        {
            bool canSpawn = false;
            Transform spawnTransform = null;
            WGTPoints.ShuffleMe();

            for (int i = 0; i < WGTPoints.Length; i++)
            {
                canSpawn = WGTPoints[i].childCount == 0;
                if (canSpawn)
                {
                    spawnTransform = WGTPoints[i];
                    break;
                }
            }

            if (!canSpawn) return;

            GameObject pointObj = Instantiate(pointPrefab, spawnTransform);
            pointObj.GetComponent<WGTPointScript>().Init(pointMinPoints, pointMaxPoints, UnityEngine.Random.Range(pointMinPoints, pointMaxPoints));
            NetworkServer.Spawn(pointObj);
        }
    }
}
