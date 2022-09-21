using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class affichage : MonoBehaviour
{
    [SerializeField]
    private Text nomJoueur;

    [SerializeField]
    private Slider sliderJoueur;

    gestionJoueur target;
    float characterControllerHeight;
    Transform targetTransform;//Eve
    Renderer targetRenderer;//Eve
    CanvasGroup _canvasGroup;
    Vector3 targetPosition;//Eve

    private void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (sliderJoueur != null)
        {
            sliderJoueur.value = target.Health;
        }
    }
    private void LateUpdate()
    {
        if (this.targetRenderer != null)
        {
            //this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            if (this.targetRenderer.isVisible)
            {
                this._canvasGroup.alpha = 1;
            }
            else
            {
                this._canvasGroup.alpha = 0;

            }
            if (targetTransform != null)
            {
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
