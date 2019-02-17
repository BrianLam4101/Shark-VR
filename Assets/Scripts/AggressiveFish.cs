using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveFish : MonoBehaviour {

    [SerializeField]
    private bool isAggressive = true;
    private float changeDirectionCooldown = 0.0f;
    private Vector3 targetDirection;
    private bool keepChangingDirection = true;

    public float chaseRadius = 10;
    private float speed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        speed = Mathf.Min(speed + 1 * Time.deltaTime, 3);
        transform.position += transform.forward * speed* Time.deltaTime;

        if (changeDirectionCooldown > 0)
        {
            changeDirectionCooldown -= Time.deltaTime;
        }
        else
        {
            targetDirection = Random.insideUnitSphere.normalized;
            changeDirectionCooldown = 5.0f;
        }

        if (Vector3.Distance(SharkController.instance.transform.position , transform.position) < chaseRadius) {
            targetDirection = (SharkController.instance.transform.position - transform.position).normalized;
            keepChangingDirection = false;
        }

        if (targetDirection != null && keepChangingDirection)
        {
            transform.forward = Vector3.Slerp(transform.forward, targetDirection, 0.5f * Time.deltaTime);
        }

	}

    private void LateUpdate() {
        transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<SharkController>().Hurt(transform.forward);
            speed = -1;
        }
    }

    private void OnTriggerExit(Collider other) {
        transform.position += SharkController.instance.mainCamera.transform.forward * 190;
    }
}
