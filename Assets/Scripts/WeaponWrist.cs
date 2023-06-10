using System.Collections;
using System.Collections.Generic;
using UltimateXR.Manipulation;
using UnityEngine;

public class WeaponWrist : MonoBehaviour
{
    public UxrGrabber grabber;
    // Start is called before the first frame update
    void Start()
    {

        UxrGrabManager.Instance.GrabObject(grabber, GetComponent<UxrGrabbableObject>(), 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
