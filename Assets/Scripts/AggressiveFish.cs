using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    [SerializeField]
    private bool isAggressive = true;
    private float changeDirectionCooldown = 0.0f;
    private Vector3 newRandomDirection;
    private bool keepChangingDirection = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (changeDirectionCooldown > 0)
        {
            changeDirectionCooldown -= Time.deltaTime;
        }
        else
        {
            newRandomDirection = Random.insideUnitSphere.normalized;
            changeDirectionCooldown = 5.0f;
        }

        if (newRandomDirection != null && keepChangingDirection)
        {
            transform.forward = Vector3.Slerp(transform.forward, newRandomDirection, 0.5f * Time.deltaTime);
        }

	}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keepChangingDirection = false;

            if (isAggressive)
            {

            }
            else
            {

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keepChangingDirection = true;
        }
    }

}
