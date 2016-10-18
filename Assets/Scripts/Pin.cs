using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {

    public float standingLimit = 10f;
    public float pinSleepThreshold = 0.005f;

    private Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.sleepThreshold = pinSleepThreshold;

    }
	
	// Update is called once per frame
	void Update () {
        //print(name + " " + IsPinStanding());
	}

    public bool IsPinStanding() {

        float angle = Vector3.Angle(transform.up, Vector3.up);

        if (angle > standingLimit) {
            return false;
        } else {
            return true;
        }      

    }

    public void RaisePin(float distance) {
        body.isKinematic = true;
        transform.rotation = Quaternion.identity;
        transform.Translate(new Vector3(0f, distance, 0f));
    }

    public void LowerPin(float distance) {        
        transform.Translate(new Vector3(0f, -distance, 0f));
        body.isKinematic = false;
    }
}
