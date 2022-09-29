using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestionCam : MonoBehaviour
{
    private float distance = 2f;
    private float height = 2f;
    [SerializeField] private bool followOnStart = false;
    private float smoothSpeed = 2f;
    Transform camTransform;
    bool isFollowing;
    Vector3 camOffset = Vector3.zero;
    float sensitivity = 10;
    float yaw;
    float pitch;
    Vector3 CurrentRotation;
    Vector3 RotationSmoothVelocity;
    Vector2 PitchMinMax =new Vector2(-30,30);
    public float rotationSmoothTime = .12f;

    void Start()
    {
        if (followOnStart)
        {
            OnStartFollowing();
        }
    }

    void LateUpdate()
    {
        if (camTransform==null && isFollowing)
        {
            OnStartFollowing();
        }
        if (isFollowing)
        {
            Follow();
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, PitchMinMax.x, PitchMinMax.y);
            CurrentRotation = Vector3.SmoothDamp(CurrentRotation, (new Vector3(pitch,yaw,0)) , ref RotationSmoothVelocity, rotationSmoothTime);
            camTransform.eulerAngles = CurrentRotation;
            transform.eulerAngles = Vector3.up * CurrentRotation.y;
            Debug.Log(pitch);
        }
    }

    public void OnStartFollowing()
    {
        //Active le suivi au d�but
        camTransform = Camera.main.transform;
        isFollowing = true;
        Cut();
    }
    void Follow()
    {
        //la cam�ra suit le joueur
        camOffset.z = -distance;
        camOffset.y = height;
        camTransform.position = Vector3.Lerp(camTransform.position, transform.position + transform.TransformVector(camOffset),smoothSpeed * Time.deltaTime);
        camTransform.LookAt(transform.position);
    }
    void Cut()
    {
        //T�l�porter la cam�ra vers le joueur
        camOffset.z = -distance;
        camOffset.y = height;
        camTransform.position = transform.position + transform.TransformVector(camOffset);
        camTransform.LookAt(transform.position);
    }
}