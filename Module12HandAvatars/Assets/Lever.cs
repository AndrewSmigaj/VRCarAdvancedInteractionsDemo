using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    // Start is called before the first frame update
    //pubQuaternion startRotation;
    public void OnLeverUpdate(float percent)
    {
        //map percent from -90 to 90 (0 to 180 shifted over)
        float rotation = 180 * percent - 90;

        //replace after video
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, rotation);
    }
}
