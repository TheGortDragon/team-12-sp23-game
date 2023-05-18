using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    // Start is called before the first frame update
    private Camera mainCam;
    private Vector3 currVelocity; //needed for smooth damp
    void Start() {
        mainCam = Camera.main;
        currVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 newCameraPos = new(transform.position.x, transform.position.y, mainCam.transform.position.z);
        //float distance = Vector3.Distance(newCameraPos, mainCam.transform.position) / 10;
        //mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newCameraPos, Time.deltaTime * distance);
        mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, newCameraPos, ref currVelocity, .5f);
    }
}
