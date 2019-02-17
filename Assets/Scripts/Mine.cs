using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    [SerializeField]
    private GameObject explosion;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<SharkController>().Hurt();
        }
        GameObject.Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Outside")) {
            Destroy(gameObject);
        }
    }
}
