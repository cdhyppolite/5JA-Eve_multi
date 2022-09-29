using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class affichage : MonoBehaviour
{
    [SerializeField] private Text nomJoueur;
    [SerializeField] private Slider sliderJoueur;

    gestionJoueur target;
    float characterControllerHeight;
    Transform targetTransform; //Ève
    Renderer targetRenderer; //Ève
    CanvasGroup _canvasGroup;
    Vector3 targetPosition; //Ève

    void Awake()
    {
        //placer le slider dans le canvas
        _canvasGroup = this.GetComponent<CanvasGroup>();
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    void Update()
    {
        if (target == null)
        {
            //Détruire le slider si sa cible disparaître
            Destroy(this.gameObject);
            return;
        }

        if (sliderJoueur != null)
        {
            //Afficher la vie dans le slider
            sliderJoueur.value = target.Health;
        }
    }
    void LateUpdate()
    {
        if (this.targetRenderer != null)
        {
            //Cacher si hors caméra
            this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;

            if (targetTransform != null)
            {
                //Modifier la position du slider se la caméra
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition + new Vector3(0, 0.3f, 0));
            }
        }
    }
    public void SetTarget(gestionJoueur _target)
    {
        if (_target == null)
        {
            return;
        }
        //Trouver le joueur cible et afficher son nom au dessus de lui

        this.target = _target;
        targetTransform = this.target.GetComponent<Transform>();
        targetRenderer = this.target.GetComponentInChildren<Renderer>();
        CharacterController _characterController = this.target.GetComponent<CharacterController>();
        if (_characterController != null)
        {
            characterControllerHeight = _characterController.height;
        }

        if (nomJoueur != null)
        {
            nomJoueur.text = this.target.photonView.Owner.NickName;
        }
    }
}