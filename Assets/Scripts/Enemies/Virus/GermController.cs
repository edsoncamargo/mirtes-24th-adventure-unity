using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermController : MonoBehaviour {
    private float _horizontalSpeed = 12f;
    private float _totalDistance = 10f;
    private float _distanceMoved = 0f;

    void Update() {
        if (_distanceMoved < _totalDistance) {
            Vector3 movement = new Vector3(_horizontalSpeed * Time.deltaTime, 0f, 0f);
            transform.Translate(movement);
            _distanceMoved += Mathf.Abs(-movement.x);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().Demage(1);
            Destroy(gameObject);
        }
    }
}
