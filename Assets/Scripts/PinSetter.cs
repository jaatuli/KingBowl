using UnityEngine;
using System.Collections;

public class PinSetter : MonoBehaviour {

    public UnityEngine.UI.Text standingDisplay;
    public float waitTime = 3f;
    public float distanceToRaise = 40f;
    public GameObject pinsPrefab;
    public Vector3 pinsPosition;

    private bool ballEnteredTrigger = false;
    private Ball ball;
    private bool havePinsStopped = false;    
    private float timer = 0f;
    private int fallenPins, standingPinsAtStart;
    private ActionMaster actionMaster;
    private Animator anim;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        actionMaster = new ActionMaster();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (ballEnteredTrigger) {
            

            timer += Time.deltaTime;

            standingDisplay.text = CountStandingPins().ToString();
            HavePinsStopped();

            if (havePinsStopped && timer > waitTime) {
                fallenPins = standingPinsAtStart - CountStandingPins();
                ActionMaster.Action action = actionMaster.Bowl(fallenPins);
                Debug.Log("Pins fallen: " + fallenPins + " " + action);

                switch(action) {
                    case ActionMaster.Action.Tidy:
                        anim.SetTrigger("triggerTidy");
                        break;
                    case ActionMaster.Action.Reset:
                        anim.SetTrigger("triggerReset");
                        break;
                    case ActionMaster.Action.EndGame:
                        anim.SetTrigger("triggerReset");
                        break;
                    case ActionMaster.Action.EndTurn:
                        anim.SetTrigger("triggerReset");
                        break;
                }                

                ballEnteredTrigger = false;
                timer = 0f;
                ball.Reset();
            } 
        }
	
	}

    public void RaisePins() {
        DisableBall();

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsPinStanding()) {

                pin.RaisePin(distanceToRaise);
            }
        }
    }

    public void LowerPins() {

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsPinStanding()) {

                pin.LowerPin(distanceToRaise);
            }
        }

        standingDisplay.text = CountStandingPins().ToString();
        EnableBall();

    }

    public void DisableBall () {
        ball.inPlay = true;
        standingDisplay.color = Color.red;
    }

    public void EnableBall() {
        ball.inPlay = false;
        standingDisplay.color = Color.green;
    }

    public void RenewPins() {

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            Destroy(pin.gameObject);
        }

        Vector3 pinsRaisedPosition = pinsPosition;
        pinsRaisedPosition.y = pinsPosition.y + distanceToRaise;

        Instantiate(pinsPrefab, pinsRaisedPosition, Quaternion.identity, transform);
    }

    int CountStandingPins() {
        int standing = 0;

        foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsPinStanding()) {
                standing++;
            }
        }

        return standing;
    }

    /// <summary>
    /// Loop through all pins to check if any are moving
    /// </summary>
    void HavePinsStopped() {

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if(pin.GetComponent<Rigidbody>().IsSleeping() == false) {
                //print(pin.name + " is not sleeping");
                havePinsStopped = false;
            }
        }
        //print("All pins sleeping");
        havePinsStopped = true;
    }

    public void BallExitedLane() {
        standingPinsAtStart = CountStandingPins();
        ballEnteredTrigger = true;
    }

    void OnTriggerEnter(Collider collider) {

        if (collider.gameObject.GetComponent<Ball>()) {
            BallExitedLane();
        }
    }

    void OnTriggerExit (Collider collider) {
        if (collider.gameObject.GetComponentInParent<Pin>()) {            
            Destroy(collider.gameObject.GetComponentInParent<Pin>().gameObject);
        }
    }
}
