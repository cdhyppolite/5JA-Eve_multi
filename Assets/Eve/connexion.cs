using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

#pragma warning disable 649

public class connexion : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private Text feedBacktext;
    [SerializeField] private byte maxPlayerPerRoom = 5;
    bool isConnecting;
    string gameVersion = "1";

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Connect()
    {
        feedBacktext.text =  "";
        isConnecting = true;
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        } else
        {
            LogFeedback("connexion en cours...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }

    }
    void LogFeedback(string msg)
    {
        if (feedBacktext == null)
            return;

        feedBacktext.gameObject.SetActive(true);
        feedBacktext.text += System.Environment.NewLine + msg;
    }
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayerPerRoom });
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        LogFeedback("Vous avez été déconnecté  "+cause);
        isConnecting = false;
        controlPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
        {
            PhotonNetwork.LoadLevel("SceneJeu");
        }
    }
}