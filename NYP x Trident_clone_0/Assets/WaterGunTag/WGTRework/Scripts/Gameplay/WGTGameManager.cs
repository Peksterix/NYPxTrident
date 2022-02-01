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


namespace WGTRework
{
    // This exists on the server only
    // It can call commands and shit though

    public static class ListExtension
    {
        public static void ShuffleMe<T>(this IList<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class WGTGameManager : Singleton<WGTGameManager>
    {
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] float gameTime = 120.0f;
        [SerializeField] float countdownTime = 3.0f;

        public enum GameState
        {
            STARTING,
            ONGOING,
            ENDED
        }

        public GameState currGameState;

        void Start()
        {
            if (!NetworkServer.active)
            {
                Destroy(this);
                return;
            }
            StartCoroutine(RunGameLoop());
        }

        NetworkIdentity[] GetAllPlayers()
        {
            var conns = NetworkRoomManagerExt.Instance.inGamePlayerList;
            return conns.ToArray();
        }

        IEnumerator RunGameLoop()
        {
            yield return StartCoroutine(WaitForPlayers());
            yield return StartCoroutine(BeginCountdown());
            yield return StartCoroutine(GameStarted());
            yield return StartCoroutine(GameEnded());
        }

        IEnumerator WaitForPlayers()
        {
            while (GetAllPlayers().Length != NetworkServer.connections.Count)
                yield return null;

            // Give them random unique spawn points
            Transform[] temp = spawnPoints;
            temp.ShuffleMe();

            var players = GetAllPlayers();
            int index = 0;
            foreach (var player in players)
            {
                player.transform.position = temp[index].position;
                player.GetComponent<NetworkTransform>().RpcTeleport(temp[index].position);
                index++;
            }

            // When all players are connected
            Debug.Log("All players are connected and have their players created and spawned!");
            yield break;
        }

        IEnumerator BeginCountdown()
        {
            // Begin the countdown on all clients
            var players = GetAllPlayers();
            foreach (var player in players)
            {
                player.GetComponent<NetworkTransform>().clientAuthority = false;
                player.GetComponent<WGTPlayerController>().RpcInitPlayer(false);
                player.GetComponent<WGTPlayerUIHandle>().BeginCountdown(countdownTime, (float)NetworkTime.time);
            }

            while (countdownTime >= 0)
            {
                countdownTime -= Time.deltaTime;
                yield return null;
            }

            // Give all players back their ability to move
            foreach (var player in players)
            {
                player.GetComponent<NetworkTransform>().clientAuthority = true;
                player.GetComponent<WGTPlayerController>().RpcInitPlayer(true);
                player.GetComponent<WGTPlayerUIHandle>().BeginGameTime(gameTime, (float)NetworkTime.time);
            }

            yield break;
        }

        IEnumerator GameStarted()
        {
            var players = GetAllPlayers();
            players[UnityEngine.Random.Range(0, players.Length)].GetComponent<WGTPlayerController>().TurnPlayerIntoCatcher(true);
            WGTPointManager.Instance.StartSpawningCoroutine();

            while (gameTime >= 0)
            {
                gameTime -= Time.deltaTime;
                yield return null;
            }
            yield break;
        }

        IEnumerator GameEnded()
        {
            Debug.Log("Game Ended!");
            var players = GetAllPlayers();
            foreach (var player in players)
            {
                player.GetComponent<WGTPlayerUIHandle>().GameEnd();
            }
            yield break;
        }
    }
}
