using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkController : MonoBehaviour {

    private Camera mainCamera;
    [SerializeField]
    private float avgTime = 0.5f;
    private float totalVel = 0;
    private float AvgVel {
        get { return totalVel / runningCount.Count; }
    }
    private Queue<FrameRotation> runningCount = new Queue<FrameRotation>();

    public Slider debugVelocity;

    void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        UpdateAverageVelocity();
        debugVelocity.value = AvgVel;
    }

    void FixedUpdate() {

    }

    private void UpdateAverageVelocity() {
        while (runningCount.Count > 0 && runningCount.Peek().Time <= Time.time - avgTime) {
            totalVel -= runningCount.Dequeue().Velocity;
        }
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        float angVelMagnitude = OVRInput.GetLocalControllerAngularVelocity(activeController).magnitude * Mathf.Rad2Deg;
        totalVel += angVelMagnitude;
        runningCount.Enqueue(new FrameRotation(angVelMagnitude));
    }

    private class FrameRotation {
        public float Time;
        public float Velocity;

        public FrameRotation (float velocity) {
            Time = UnityEngine.Time.time;
            Velocity = velocity;
        }
    }
}
