using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    [SerializeField]
    private GameObject explosion;
    public float timer = 120;

    // Use this for initialization
    void Start() {
        StartCoroutine(Destorytime());
    }

    private IEnumerator Destorytime() {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<SharkController>().Hurt();
            GameObject.Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        transform.position += SharkController.instance.mainCamera.transform.forward * 190;
    }
}
