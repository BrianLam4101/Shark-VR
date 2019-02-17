using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float radius = 50;
    [SerializeField]
    private float cooldown = 2;
    private float cooldownTimer = 0;
    [SerializeField]
    private float cooldownVariance = 1;
	
	// Update is called once per frame
	void Update () {
        if (cooldownTimer <= 0) {
            Instantiate<GameObject>(prefab, transform.position + Random.insideUnitSphere * radius, Quaternion.identity);
            cooldownTimer = cooldown + cooldown * Random.Range(0, cooldownVariance);
        }
        cooldownTimer -= Time.deltaTime;
    }
}
