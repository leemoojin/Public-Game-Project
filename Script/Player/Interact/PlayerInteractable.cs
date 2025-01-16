using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractable : MonoBehaviour
{
    [Header("CurInteractInfo")]
    [field: SerializeField] private GameObject curInteractGameObject;
    private IInteractable _curInteractable;
    private bool _isHoldInterating;

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
    private float _holdDuration = 0f;
    private Image _reticleImage;

    [Header("UI")]
    public GameObject interactUI;
    public Text objectText;
    public Text interactKey;
    public Text interactType;

    private void Awake()
    {
        _reticleImage = reticle.GetComponent<Image>();
    }

    private void Start()
    {
        _camera = Camera.main;       
    }

    private void FixedUpdate()
    {
        PerformRaycast();
        if (_isHoldInterating) CheckHoldInteract();
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
                _curInteractable = hit.collider.GetComponent<IInteractable>();

                if (_curInteractable == null || !_curInteractable.IsInteractable) return;
         
                //reticle.GetComponent<Image>().sprite = ringImg;
                _reticleImage.sprite = ringImg;
                reticle.transform.localScale = new Vector3(1.5f,1.5f,1f);
                // set ui text, image 
                SetUIPrompt();
                return;
            }

            if (!_curInteractable.IsInteractable)
            {
                //reticle.GetComponent<Image>().sprite = dotImg;
                _reticleImage.sprite = dotImg;
                reticle.transform.localScale = Vector3.one;
                SetUIPrompt();
            }
        }
        else
        {
            curInteractGameObject = null;
            _curInteractable = null;
            //reticle.GetComponent<Image>().sprite = dotImg;
            _reticleImage.sprite = dotImg;
            reticle.transform.localScale = Vector3.one;
            SetUIPrompt();
        }        
    }

    public void SetUIPrompt()
    {
        if (_curInteractable != null)
        {
            if (_curInteractable.IsInteractable)
            {
                objectText.text = _curInteractable.ObjectName;
                interactKey.text = _curInteractable.InteractKey;
                interactType.text = _curInteractable.InteractType;
                interactUI.SetActive(true);
                return;
            }            
        }
        interactUI.SetActive(false);
    }

    public void OnInteracted()
    {
        if (_curInteractable == null || !_curInteractable.IsInteractable) return;

        //Debug.Log("PlayerInteractable - 상호작용 시작");

        if (_curInteractable.InteractHoldTime > 0f)
        {
            _isHoldInterating = true;
        }
        else
        {
            _curInteractable.Interact();
            SetUIPrompt();
        }
    }

    public void OffInteracted()
    {
        if (_curInteractable == null) return;

        if (_curInteractable.InteractHoldTime > 0f)
        {
            _isHoldInterating = false;
            _holdDuration = 0f;
            holdInteract.fillAmount = 0f;
        }
    }

    private void CheckHoldInteract()
    {
        if (_curInteractable == null || _curInteractable.InteractHoldTime <= 0f || !_curInteractable.IsInteractable)
        {
            _isHoldInterating = false;
            _holdDuration = 0f;
            holdInteract.fillAmount = 0f;
            return;
        }

        _holdDuration += Time.deltaTime;
        holdInteract.fillAmount = Mathf.Clamp01(_holdDuration / _curInteractable.InteractHoldTime);

        if (_holdDuration >= _curInteractable.InteractHoldTime)
        {
            _curInteractable.Interact();
            SetUIPrompt();
            _holdDuration = 0f;
            holdInteract.fillAmount = 0f;
        }
    }
}
