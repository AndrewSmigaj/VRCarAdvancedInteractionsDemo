using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnRotateUpdate(float dragPercent)
    {
        Debug.Log("Entered Slider Change: " + dragPercent);
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 90 - dragPercent * 180);
    }
}
