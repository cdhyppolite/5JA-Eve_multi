using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class gestionAnim : MonoBehaviourPun
{
    Animator eveAnim;
    CharacterController eveController;
    public static bool isShooting;
    bool isJumping = false;
    Vector3 Velocity;
    float velocityY; // verticale
    float currentSpeed;
    float gravity = -10;
    float jumpHeight = 1f;

    void Start()
    {
        eveAnim = GetComponent<Animator>();
        eveController = GetComponent<CharacterController>();
        Velocity = Vector3.zero;
    }

    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            //Ne rien faire si on est pas joueur local
            return;
        }

        if (!eveAnim)
        {
            return;
        }

        //Saut
        if (Input.GetKeyDown(KeyCode.Space) && eveController.isGrounded)
        {
            isJumping = true;
            float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
            velocityY = jumpVelocity;
        } else
        {
            isJumping = false;
        }

        // Déplacement Avant
        float v = Input.GetAxis("Vertical");
        if (v < 0) v = 0;

        //gestion Animations
        eveAnim.SetFloat("Speed", v);
        eveAnim.SetBool("isJumping", isJumping);
        eveAnim.SetBool("isShooting", isShooting);

        //physique
        velocityY += gravity * Time.deltaTime;
        currentSpeed = new Vector2(eveController.velocity.x, eveController.velocity.z).magnitude;
        Velocity = currentSpeed * transform.forward + velocityY * Vector3.up;
        eveController.Move(Velocity * Time.deltaTime);
    }
}