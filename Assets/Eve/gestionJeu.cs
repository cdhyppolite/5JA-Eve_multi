using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


#pragma warning disable 649

public class gestionJeu : MonoBehaviour
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

    }

    public void Quitter()
    {
        Application.Quit();
    }
}
