using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Fonctionnement et utilité générale du script
 * Change de skybox et change la couleur de la lumière (utilsé dans le monde campement)
 * Par : Carl-David Hyppolite
*/

public class changerSkyBox : MonoBehaviour
{
    public Material ciel;
    public Light lumiereCiel;


    void Start()
    {
        RenderSettings.skybox = ciel;
        if (lumiereCiel != null) lumiereCiel.color = Color.white;
        Destroy(gameObject);
    }
}