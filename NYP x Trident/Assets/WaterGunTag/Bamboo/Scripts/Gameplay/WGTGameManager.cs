using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bamboo.Utility;
using Mirror;

namespace Bamboo.WGT
{
    // This exists on the server only
    // It can call commands and shit though

    public class WGTGameManager : Singleton<WGTGameManager>
    {
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
            if (!NetworkServer.active && !NetworkClient.active) Destroy(this);
        }

        NetworkIdentity[] GetAllPlayers()
        {
            var conns = NetworkServer.connections.Values;
            List<NetworkIdentity> players = new List<NetworkIdentity>();

            foreach (var conn in conns)
            {
                if (conn.identity != null)
                    players.Add(conn.identity);
            }

            return players.ToArray();
        }

        // This is ballsack bad
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

            Debug.Log("All players are connected and have their players created");
            yield break;
        }

        IEnumerator BeginCountdown()
        {
            while (countdownTime >= 0)
            {
                countdownTime -= Time.deltaTime;
                yield return null;
            }

            // What should happen here is you grab all the players using GetAllPlayers, then proceed to get component to wtv is relevant
            // Then you RPC to them to update their UI, but I lazy do it now
            // I believe in u tho so ez clap
            // !!!!

            yield break;
        }

        IEnumerator GameStarted()
        {
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
            // Do whatever you need here
            yield break;
        }
    }
}
