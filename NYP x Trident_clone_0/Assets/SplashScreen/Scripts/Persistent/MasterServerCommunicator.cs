using System;
using System.Text;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Bamboo.Utility;

public class MasterServerCommunicator : Singleton<MasterServerCommunicator>
{
    public static class IPGetter
    {
        public static string GetIP()
        {
            string externalIpString = new WebClient().DownloadString("https://ipv4.icanhazip.com/").Replace("\\r\\n", "").Replace("\\n", "").Trim();
            var externalIp = IPAddress.Parse(externalIpString);
            return externalIp.ToString();
        }
    }
    /// <summary>
    /// Master server url
    /// </summary>
    public string MasterServerUrl;

    /// <summary>
    /// The IP returned when registering as a server
    /// </summary>
    public string ServerIP;

    /// <summary>
    /// Sets whether or not GetIP is called, or the network address is automatically set to localhost
    /// </summary>
    public bool isServerIPLocal = true;

    /// <summary>
    /// Called when the server registers successfully, invokes an event that passes in the returned server code as a parameter
    /// </summary>
    public UnityEvent<string> OnServerRegistered { get; private set; } = new UnityEvent<string>();

    /// <summary>
    ///Called when the server fails to register, invokes an event that passes in the returned error message as well as the response code
    /// </summary>
    public UnityEvent<string, string> OnServerRegisteredFail { get; private set; } = new UnityEvent<string, string>();

    /// <summary>
    /// Called when the client gets a network address successfully, invokes an event that passes in the returned server address as a parameter
    /// </summary>
    public UnityEvent<string> OnClientGetNetworkAddress { get; private set; } = new UnityEvent<string>();

    /// <summary>
    ///Called when the server fails to register, invokes an event that passes in the returned error message as well as the response code
    /// </summary>
    public UnityEvent<string, string> OnClientGetNetworkAddressFail { get; private set; } = new UnityEvent<string, string>();

    protected override void OnAwake()
    {
        base.OnAwake();
        _persistent = true;
        MasterServerUrl = MasterServerUrl.Length == 0 ? "localhost:1337" : MasterServerUrl;
    }

    /// <summary>
    /// Call this to start registering your server
    /// </summary>
    public void RegisterServer(int gameMode)
    {
        string ip = isServerIPLocal ? "localhost" : IPGetter.GetIP();
        JObject req = new JObject();
        req.Add("NetworkAddress", ip);
        req.Add("GameMode", gameMode);
        ServerIP = ip;
        var raw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(req));

        StartCoroutine(_RegisterServer());

        IEnumerator _RegisterServer()
        {
            using (var registerServerRequest = UnityWebRequest.Put(string.Format("{0}/RegisterServer", MasterServerUrl), raw))
            {
                registerServerRequest.method = "POST";
                registerServerRequest.SetRequestHeader("Content-Type", "application/json");
                yield return registerServerRequest.SendWebRequest();

                try
                {
                    if (registerServerRequest.responseCode == 200)
                    {
                        JObject res = JObject.Parse(registerServerRequest.downloadHandler.text);
                        string code = res["Code"].Value<string>();
                        OnServerRegistered?.Invoke(code);
                    }
                    else if (registerServerRequest.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log("Failed to connect to the master server");
                        throw new Exception("ConnectionFailed", new Exception(registerServerRequest.result.ToString()));
                    }
                    else
                    {
                        Debug.Log("Request failed: " + registerServerRequest.downloadHandler.text);
                        throw new Exception("RequestFailed", new Exception(registerServerRequest.downloadHandler.text));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    OnServerRegisteredFail?.Invoke(e.Message, e.InnerException.Message);
                    yield break;
                }

            }
        }
    }

    /// <summary>
    /// Call this to pass in a code to retrieve a network address/error
    /// </summary>
    /// <param name="code"></param>
    public void CodeToServer(string code, int gameMode)
    {
        JObject req = new JObject();
        req.Add("Code", code);
        req.Add("GameMode", gameMode);
        var raw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(req));
        StartCoroutine(_CodeToServer());

        IEnumerator _CodeToServer()
        {

            using (var codeToServerRequest = UnityWebRequest.Put(string.Format("{0}/CodeToServer", MasterServerUrl), raw))
            {
                codeToServerRequest.method = "POST";
                codeToServerRequest.SetRequestHeader("Content-Type", "application/json");

                Debug.Log("Getting network address with code...");
                yield return codeToServerRequest.SendWebRequest();
                try
                {
                    if (codeToServerRequest.responseCode == 200)
                    {
                        JObject res = JObject.Parse(codeToServerRequest.downloadHandler.text);
                        string code = res["NetworkAddress"].Value<string>();
                        OnClientGetNetworkAddress?.Invoke(code);
                    }
                    else if (codeToServerRequest.result == UnityWebRequest.Result.ConnectionError)
                    {
                        Debug.Log("Failed to connect to the master server");
                        throw new Exception("ConnectionFailed", new Exception(codeToServerRequest.result.ToString()));
                    }
                    else
                    {
                        Debug.Log("Request failed: " + codeToServerRequest.downloadHandler.text);
                        throw new Exception("RequestFailed", new Exception(codeToServerRequest.downloadHandler.text));
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    OnClientGetNetworkAddressFail?.Invoke(e.Message, e.InnerException.Message);
                    yield break;
                }

            }
        }
    }
}
