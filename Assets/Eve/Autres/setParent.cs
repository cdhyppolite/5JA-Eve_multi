using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setParent : MonoBehaviour
{
    public Transform newParent;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(newParent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}