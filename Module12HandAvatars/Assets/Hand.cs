using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right
    }
    public InputAction inputAction;

    public List<Renderer> meshRenderers = new List<Renderer>();

    private bool currentlyTracked = false;
    private bool isHidden;
    public HandType handType = HandType.Left;

    public bool isCollisionEnabled { get; private set; } = false;

    public Collider[] colliders;

    public XRBaseInteractor interactor;
    
    private void Awake()
    {
        
        if(interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }

    }

    private void OnEnable()
    {
        interactor.selectEntered.AddListener(OnGrab);
        interactor.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(OnGrab);
        interactor.selectExited.RemoveListener(OnRelease);
    }
    // Start is called before the first frame updatepublic 
    void Start()
    {


        colliders = GetComponentsInChildren<Collider>().Where(collider => !collider.isTrigger).ToArray();


        inputAction.Enable();
        
        
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = inputAction.ReadValue<float>();

        if(isTracked == 1.0f && !currentlyTracked)
        {
            Show();
            currentlyTracked = true;
        }
        else if(isTracked == 0.0f && currentlyTracked)
        {
            Hide();
            currentlyTracked = false;
        }
    }


    void Show()
    {
        foreach(Renderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        isHidden = false;
        EnableCollisions(true);
    }

    void Hide()
    {
        meshRenderers.Clear();

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
            meshRenderers.Add(renderer);
        }
        isHidden = true;
        EnableCollisions(false);
    }

    void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled) return;

        isCollisionEnabled = enabled;
        foreach(Collider collider in colliders)
        {
            collider.enabled = enabled;
        }
    }

    public void OnGrab(SelectEnterEventArgs args)
    {
        //check if whether to hide it or not
        HandControl ctrl = args.interactableObject.transform.gameObject.GetComponent<HandControl>();

        if(ctrl != null)
        {
            if (ctrl.hideHandOnGrab)
            {
                Hide();
            }
        }

    }

    public void OnRelease(SelectExitEventArgs args)
    {
        HandControl ctrl = args.interactableObject.transform.gameObject.GetComponent<HandControl>();
        if (ctrl != null)
        {
            if (ctrl.hideHandOnGrab)
            {
                Show();
            }
        }

    }
}
