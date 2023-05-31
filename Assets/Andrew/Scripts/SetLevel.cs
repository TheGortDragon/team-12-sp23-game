using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevel : MonoBehaviour {
    // Start is called before the first frame update
    public int level;
    void Start() {
        GetComponent<TimeManagerScript>().setLevel(level);
    }
}
