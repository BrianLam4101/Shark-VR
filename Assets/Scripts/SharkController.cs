using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour {

    private Camera mainCamera;
    private Transform controller;
    private Quaternion previousRotation;
    private float rotationsPerSec;
    private Queue<FrameRotation> runningCount = new Queue<FrameRotation>();

    void Start() {
        mainCamera = Camera.main;
        previousRotation = controller.rotation;
    }

    private void Update() {
        OVRInput.Controller activeController = OVRInput.GetActiveController();
        Vector3 angVel = OVRInput.GetLocalControllerAngularVelocity(activeController);
        data.AppendFormat("AngVel: ({0:F2}, {1:F2}, {2:F2})\n", angVel.x, angVel.y, angVel.z);
        //rotationsPerSeccontroller.rotation
        runningCount.Enqueue(new FrameRotation(Quaternion.Angle(controller.rotation, previousRotation)));
        previousRotation = controller.rotation;
    }

    void FixedUpdate() {

    }

    private class FrameRotation {
        public float Time;
        public float Angle;

        public FrameRotation (float angle) {
            Time = UnityEngine.Time.time;
            Angle = angle;
        }
    }
}
