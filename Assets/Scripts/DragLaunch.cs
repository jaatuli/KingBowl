using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    [Range(0f,1f)]
    public float sideWaysMultiplier = 1f;
    [Range(0f, 1f)]
    public float speedMultiplier = 1f;

    private Ball ball;
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;

    // Use this for initialization
    void Start() {
        ball = GetComponent<Ball>();

    }

    /// <summary>
    /// Moves the ball starting position sideways
    /// </summary>
    /// <param name="amount">Amount to move in x-axis</param>
    public void MoveStart(float amount) {
        if (!ball.inPlay) {
            ball.transform.Translate(amount, 0f, 0f);
        }
    }

    /// <summary>
    /// Capture time and position of drag start
    /// </summary>
    public void DragStart() {
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    /// <summary>
    /// Capture time and position on drag end. Launch the ball
    /// </summary>
    public void DragEnd() {
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;
        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration * sideWaysMultiplier;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration * speedMultiplier;

        ball.Launch(launchSpeedZ, launchSpeedX);

    }

}
