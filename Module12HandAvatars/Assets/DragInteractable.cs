using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabEvent : UnityEvent<float> { }

public class DragInteractable : XRBaseInteractable
{
    // Start is called before the first frame update
    public Transform startPosition;
    public Transform endPosition;

    public MeshRenderer renderer;

    public float dragAmount;

    public UnityEvent onGrabStart = new UnityEvent();
    public UnityEvent onGrabEnd = new UnityEvent();

    public GrabEvent grabChanged = new GrabEvent();

    private XRBaseInteractor interactor;
    private Coroutine grabCalculationRoutine;

    void StartDrag()
    {
        if(grabCalculationRoutine != null)
        {
            StopCoroutine(grabCalculationRoutine);
        }
        grabCalculationRoutine = StartCoroutine(CalculateDrag());
        onGrabStart?.Invoke();
    }

    void EndDrag()
    {
        if (grabCalculationRoutine != null)
        {
            StopCoroutine(grabCalculationRoutine);
            onGrabEnd?.Invoke();

        }
    }

    public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
    {
        Vector3 AB = b - a;
        Vector3 AV = value - a;
        return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
    }
    IEnumerator CalculateDrag()
    {

        while(interactor != null)
        {
            //get line
            Vector3 slideLine = startPosition.localPosition - endPosition.localPosition;

            //get interactor position
            Vector3 interactorLocal = startPosition.parent.InverseTransformPoint(interactor.gameObject.transform.position);

            //project
            Vector3 projectedOnLine = Vector3.Project(interactorLocal, slideLine);

            //Vector3 point = 
            dragAmount = InverseLerp(startPosition.localPosition, endPosition.localPosition, projectedOnLine);
            //fire event
            yield return null;
        }

    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Slider interactable select enter");
        renderer.material.SetColor("_Color", Color.red);
        StartDrag();


        this.interactor = args.interactor;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Slider interactable select exit");
        renderer.material.SetColor("_Color", Color.grey);
        EndDrag();
        this.interactor = null;
        base.OnSelectExited(args);
    }

/*    public void HandleSelectEnterTest(SelectEnterEventArgs args)
    {
        Debug.Log("HEYTHERE");
    }*/


}
