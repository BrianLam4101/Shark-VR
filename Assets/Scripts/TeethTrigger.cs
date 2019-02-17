using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ColliderAction : UnityEvent<Collider> { }

public class TeethTrigger : MonoBehaviour {

    public ColliderAction OnBite;

    private void OnTriggerEnter(Collider other) {
        OnBite.Invoke(other);
    }
}
