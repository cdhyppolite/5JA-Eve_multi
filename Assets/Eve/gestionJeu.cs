using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


#pragma warning disable 649

public class gestionJeu : MonoBehaviourPunCallbacks
{
    static public gestionJeu Instance;
    private GameObject instance;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] posInstances;
    Transform playerTransform;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene(0);
            return;
        }

        if (playerPrefab == null)
        {
            Debug.LogWarning("Pas de référence pour le player prefab");
        }
        else
        {
            int rand = Random.Range(0, posInstances.Length);
            PhotonNetwork.Instantiate(this.playerPrefab.name, posInstances[rand].transform.position, Quaternion.identity,0);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loadArena();
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loadArena();
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    void loadArena()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void Quitter()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
}