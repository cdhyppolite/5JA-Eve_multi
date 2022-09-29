using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(InputField))]
public class inputFieldNom : MonoBehaviour
{
    const string playerNomPrefKey = "playerNom";

    void Start()
    {
        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();

        //Vérifier si on a écrit un truc dans nom
        if (_inputField !=null)
        {
            if (PlayerPrefs.HasKey(playerNomPrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNomPrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }
    public void SetPlayerName(string value)
    {
        //Sauvegarder le pseudo
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Nom du Joueur non valide");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNomPrefKey, value);
    }
}
