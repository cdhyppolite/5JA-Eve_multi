using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class gestionJoueur : MonoBehaviourPunCallbacks, IPunObservable
{
    public float Health = 1f;
    public static GameObject LocalPlayerInstance;
    [SerializeField] private GameObject playerUiprefab;
    [SerializeField] private GameObject beam;
    bool isShooting = false;

    void Awake()
    {
        //Assigner le prefab local au joueur local
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //Assigner la caméra au joueur local
        gestionCam gestionCamera = gameObject.GetComponent<gestionCam>();
        if (gestionCamera!=null)
        {
            if (photonView.IsMine)
            {
                gestionCamera.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogWarning("Pas de Script gestionCam sur le player prefab");
        }

        //Faire apparaître le slider du joueur local
        if (this.playerUiprefab != null)
        {
            GameObject _uiGo = Instantiate(this.playerUiprefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        //Prendre en compte seulement les commandes si on est le joueur local
        if (photonView.IsMine)
        {
            //Tire
            if (Input.GetButton("Fire1"))
            {
                if (EventSystem.current.IsPointerOverGameObject()) {
                    return;
                }
                if (!gestionAnim.isShooting)
                {
                    gestionAnim.isShooting = true;
                    //this.beam.SetActive(true);
                    ActiverBeam();
                }
            }
            else
            {
                gestionAnim.isShooting = false;
                this.beam.SetActive(false);
            }
        }
        if (this.Health <= 0)
        {
            //QuitterRoom();
            Invoke("QuitterRoom", 1f);
            GameObject.Find("Canvas").transform.Find("XMort").gameObject.SetActive(true); //Activer une animation de mort
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) //Envoi
        {
            stream.SendNext(this.isShooting);
            stream.SendNext(this.Health);
        }
        else //Reçoit
        {
            this.isShooting = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }
    private void QuitterRoom()
    {
        //retourner à l'accueil
        gestionJeu.Instance.Quitter();
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoad(scene.buildIndex);
    }

    void CalledOnLevelWasLoad(int level)
    {
        if (Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0, 5, 0);
        }

        //Faire apparaître le slider
        GameObject _uiGo = Instantiate(this.playerUiprefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

    //Méthodes des collisions ------------------------------------------
    void OnTriggerEnter(Collider infoCol)
    {
        if (!photonView.IsMine)
        {
            return;
            //Rien faire si joueur local
        }

        if (!infoCol.name.Contains("Beam"))
        {
            return;
            //Rien faire si ce n'est pas le laser
        }

        this.Health -= .1f;
    }

    void OnTriggerStay(Collider infoCol)
    {
        if (!photonView.IsMine)
        {
            return;
            //Rien faire si joueur local
        }

        if (!infoCol.name.Contains("Beam"))
        {
            return;
            //Rien faire si ce n'est pas le laser
        }

        this.Health -= .1f * Time.deltaTime;
    }
    public void ActiverBeam()
    {
        this.beam.SetActive(true);
    }
}