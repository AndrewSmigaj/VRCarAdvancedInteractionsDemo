using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehavior : MonoBehaviour
{

    private bool isMoving;
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = transform.TransformPoint(Vector3.forward * moveSpeed);
        }
    }

    public void OnRotateChange(float dragPercent)
    {
        //.5 is no rotation so we want 
        //put it on a range of -30 to 30 degrees

        float newAngle = (dragPercent * 20 - 10)*Time.deltaTime;

        this.transform.RotateAround(transform.up, newAngle);
    }
}
