using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderScript : MonoBehaviour
{
    public Transform startPosition = null;
    public Transform endPosition = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TestSliderEvents()
    {
        Debug.Log("Event fired and recieved by slider");
    }

    public void OnSlideStart()
    {

    }

    public void OnSlideEnd()
    {

    }

    public void OnSlideUpdate(float dragPercent)
    {
        Debug.Log("Entered Slider Change: " + dragPercent);
        this.gameObject.transform.position = Vector3.Lerp(startPosition.position, endPosition.position, dragPercent);
    }

}
