using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
        
    public bool inPlay = true;
    public UnityEngine.UI.Text standingDisplay;

    private Rigidbody body;
    private AudioSource audioSource;
    private Vector3 startPosition;    

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        body.useGravity = false;
        startPosition = transform.position;

    }

    public void Launch (float velocity, float rotation) {
        if (!inPlay) {
            body.useGravity = true;
            body.velocity = new Vector3(0f, 0f, velocity);
            body.angularVelocity = new Vector3(0f,0f,-rotation); 
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
        transform.rotation = Quaternion.identity;     
        inPlay = false;
        standingDisplay.color = Color.green;
    }
}
