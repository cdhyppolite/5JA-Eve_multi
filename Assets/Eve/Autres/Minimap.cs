using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    Transform cible;

    void Start()
    {
        cible = Camera.main.transform;
    }

    void LateUpdate()
    {
        //Suivre la cible
        Vector3 newPosition = cible.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //Orienter la caméra selon la cible
        transform.rotation = Quaternion.Euler(90f, cible.eulerAngles.y, 0);
    }
}