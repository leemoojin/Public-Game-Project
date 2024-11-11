using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractable : MonoBehaviour
{
    [Header("CurInteractInfo")]
    [field: SerializeField] private GameObject curInteractGameObject;
    private IInteractable curInteractable;
    private bool isHoldInterating;

    [Header("Raycast")]
    public float maxCheckDistance = 10f;
    public LayerMask layerMask;
    private Camera _camera;

    [Header("Reticle")]
    [field: SerializeField] private GameObject reticle;
    [field: SerializeField] private Sprite dotImg;
    [field: SerializeField] private Sprite ringImg;
    [field: SerializeField] private Sprite holdImg;
    [field: SerializeField] private Image holdInteract;
    private float holdDuration = 0f;

    private void Start()
    {
        _camera = Camera.main;       
    }


    private void FixedUpdate()
    {
        PerformRaycast();

        if (isHoldInterating) CheckHoldInteract();
    }

    private void PerformRaycast()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {            
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                curInteractable = hit.collider.GetComponent<IInteractable>();

                if (curInteractable == null || !curInteractable.IsInteractable) return;
         
                reticle.GetComponent<Image>().sprite = ringImg;
                reticle.transform.localScale = new Vector3(1.5f,1.5f,1f);
                return;
                // set ui text, image 
            }

            if (!curInteractable.IsInteractable)
            {
                reticle.GetComponent<Image>().sprite = dotImg;
                reticle.transform.localScale = Vector3.one;
            }
        }
        else
        {
            curInteractGameObject = null;
            curInteractable = null;
            reticle.GetComponent<Image>().sprite = dotImg;
            reticle.transform.localScale = Vector3.one;
        }        
    }

    public void OnInteracted()
    {
        if (curInteractable == null || !curInteractable.IsInteractable) return;

        //Debug.Log("PlayerInteractable - 상호작용 시작");

        if (curInteractable.InteractHoldTime > 0f)
        {
            isHoldInterating = true;
        }
        else
        {
            curInteractable.Interact();
        }
    }

    public void OffInteracted()
    {
        if (curInteractable == null) return;

        if (curInteractable.InteractHoldTime > 0f)
        {
            isHoldInterating = false;
            holdDuration = 0f;
            holdInteract.fillAmount = 0f;
        }
    }

    private void CheckHoldInteract()
    {
        if (curInteractable == null || curInteractable.InteractHoldTime <= 0f || !curInteractable.IsInteractable)
        {
            isHoldInterating = false;
            holdDuration = 0f;
            holdInteract.fillAmount = 0f;
            return;
        }

        holdDuration += Time.deltaTime;
        holdInteract.fillAmount = Mathf.Clamp01(holdDuration / curInteractable.InteractHoldTime);

        if (holdDuration >= curInteractable.InteractHoldTime)
        {
            curInteractable.Interact();
            holdDuration = 0f;
            holdInteract.fillAmount = 0f;
        }
    }


}
