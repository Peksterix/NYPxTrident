using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI MatchIDText;
    [SerializeField]
    private GameObject KOTHGamemodeHeader;
    [SerializeField]
    private GameObject WGTGamemodeHeader;
    [SerializeField]
    private GameObject THGamemodeHeader;

    private void Start()
    {
        MatchIDText.text = NetworkRoomManagerExt.Instance.MatchID;

       switch(NetworkRoomManagerExt.Instance.gameType)
        {
            case NetworkRoomManagerExt.GameType.KOTH:
                KOTHGamemodeHeader.SetActive(true);
                break;
            case NetworkRoomManagerExt.GameType.WGT:
                WGTGamemodeHeader.SetActive(true);
                break;
            case NetworkRoomManagerExt.GameType.TH:
                THGamemodeHeader.SetActive(true);
                break;
        }
    }
}
