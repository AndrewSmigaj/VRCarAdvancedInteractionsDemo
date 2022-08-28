using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Hand : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right
    }
    public InputAction inputAction;

    public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    private bool currentlyTracked = false;
    private bool isHidden;
    public HandType handType = HandType.Left;

    // Start is called before the first frame update
    void Start()
    {
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
        foreach(MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = true;
        }
        isHidden = false;
    }

    void Hide()
    {
        meshRenderers.Clear();

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
            meshRenderers.Add(renderer);
        }
        isHidden = true;
    }

}
