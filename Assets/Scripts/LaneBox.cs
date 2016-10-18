using UnityEngine;
using System.Collections;

public class LaneBox : MonoBehaviour {

    private PinSetter pinSetter;
    
    void Start() {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.GetComponent<Ball>()) {
            pinSetter.BallExitedLane();
        }
    }
}
