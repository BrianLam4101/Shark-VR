using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIPositioning : MonoBehaviour {

    [SerializeField]
    private Transform VRCamera;
    private Vector3 ForwardDirection {
        get {
            return VRCamera.forward;
        }
    }
    private new Transform transform; // Cache transform for a small performace boost
    
    [SerializeField]
    private float moveDuration = 0.5f;
    private Vector3 turnVelocity;

    private void Awake() {
        transform = base.transform;
    }

    void LateUpdate () {
        transform.position = VRCamera.position;
        FaceForward(moveDuration);
    }

    private void FaceForward(float smoothTime) {
        transform.rotation = Quaternion.Slerp(transform.rotation, VRCamera.rotation, smoothTime);
    }
}
