using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    public bool inPlay = true;

    private Rigidbody body;
    private AudioSource audioSource;
    private Vector3 startPosition;
    public UnityEngine.UI.Text standingDisplay;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        body.useGravity = false;
        startPosition = transform.position;

    }

    public void Launch (Vector3 velocity) {
        if (!inPlay) {
            body.useGravity = true;
            body.velocity = velocity;
            audioSource.Play();
            inPlay = true;
            standingDisplay.color = Color.red;
        }
    }

    public void Reset () {
        body.useGravity = false;
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        transform.position = startPosition;       
        inPlay = false;
        standingDisplay.color = Color.green;
    }
}
