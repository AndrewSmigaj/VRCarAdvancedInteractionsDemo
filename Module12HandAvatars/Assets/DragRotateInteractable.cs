using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotateInteractable : DragInteractable
{
    // Start is called before the first frame update

    protected override IEnumerator CalculateDrag()
    {
        while (interactor != null)
        {
            Debug.Log("Calculating Drag");

            //get line pointing from center to right side of steering wheel
            Vector3 startVector = endPosition.localPosition - startPosition.localPosition;

            //get interactor position
            Vector3 interactorLocal = startPosition.parent.InverseTransformPoint(interactor.gameObject.transform.position);

            //get angle from 0 to 180
            float angle = Vector2.Angle(new Vector2(startVector.x, startVector.z), new Vector2(interactorLocal.x, interactorLocal.z));

            //Vector3 point = 
            dragAmount = Mathf.Clamp01(angle / 180f);
            Debug.Log("Rotate percent: " + dragAmount);



            onDragUpdate?.Invoke(dragAmount);
            //fire event
            yield return null;
        }
    }
}
