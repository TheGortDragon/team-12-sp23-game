using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    // Start is called before the first frame update
    private Camera mainCam;
    void Start() {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        Vector3 newCameraPos = new(transform.position.x, transform.position.y, mainCam.transform.position.z);
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newCameraPos, Time.deltaTime);
    }
}
