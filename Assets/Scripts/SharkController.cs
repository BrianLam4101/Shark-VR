using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharkController : MonoBehaviour {

    private Camera mainCamera;
    private new Rigidbody rigidbody;
    [SerializeField]
    private float avgTime = 0.5f;
    private float totalVel = 0;
    private float AvgVel {
        get { return runningCount.Count > 0 ? totalVel / runningCount.Count : 0; }
    }
    private Queue<FrameRotation> runningCount = new Queue<FrameRotation>();
    [SerializeField]
    private float anglesPerUnit = 90;
    private float speed;
    private Vector3 direction;

    [SerializeField]
    private float DashCoolDown = 0.25f;
    private float dashCoolDownTimer = 0;
    private bool dashing = false;

    private bool isHurt = false;

    [SerializeField]
    private Animator BiteAnimator;

    public Slider debugVelocity;

    void Start() {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        UpdateAverageVelocity();
        debugVelocity.value = AvgVel;

        if (!isHurt) {
            if (!dashing)
                speed = AvgVel / anglesPerUnit;
            direction = mainCamera.transform.forward;
        }

        if (dashCoolDownTimer <= 0 && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
            StartCoroutine(Dash());
            dashCoolDownTimer = DashCoolDown;
        }
        dashCoolDownTimer -= Time.deltaTime;
    }

    void FixedUpdate() {
        rigidbody.velocity = direction * speed;
    }

    private void UpdateAverageVelocity() {
        while (runningCount.Count > 0 && runningCount.Peek().Time <= Time.time - avgTime) {
            totalVel -= runningCount.Dequeue().Velocity;
        }
        if (!isHurt) {
            OVRInput.Controller activeController = OVRInput.GetActiveController();
            float angVelMagnitude = OVRInput.GetLocalControllerAngularVelocity(activeController).magnitude * Mathf.Rad2Deg;
            totalVel += angVelMagnitude;
            runningCount.Enqueue(new FrameRotation(angVelMagnitude));
        }
    }

    private IEnumerator Dash() {
        dashing = true;
        BiteAnimator.Play("Bite");
        float initSpeed = speed;
        speed *= 1.5f;
        speed += 15;
        yield return new WaitForSeconds(0.15f);
        float velocity = 0;
        float timer = 0.05f;
        while (timer > 0) {
            speed = Mathf.SmoothDamp(speed, initSpeed, ref velocity, 0.05f);
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        speed = initSpeed;
        dashing = false;
    }

    public void Hurt() {
        direction = -mainCamera.transform.forward;
        speed *= 0.75f;
        speed += 10;
        StartCoroutine(HurtDelay());
    }

    private IEnumerator HurtDelay() {
        isHurt = true;
        float velocity = 0;
        float timer = 0.5f;
        while (timer > 0) {
            speed = Mathf.SmoothDamp(speed, 0, ref velocity, 0.5f);
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        isHurt = false;
    }

    public void OnBite(Collider other) {

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
