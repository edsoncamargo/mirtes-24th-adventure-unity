using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : MonoBehaviour {

    private GameObject _player;
    private Transform _playerSyringe;

    private float horizontalSpeed = 12f;
    private float totalDistance = 10f;
    private float distanceMoved = 0f;

    void Start() {
        _player = GameObject.FindWithTag("Player");
        _playerSyringe = _player.transform.Find("Syringe");

        if (_playerSyringe) {
            Vector3 attach = _playerSyringe.transform.position;
            transform.position = attach;
        }
    }

    void Update() {
        if (distanceMoved < totalDistance) {
            Vector3 movement = new Vector3(horizontalSpeed * Time.deltaTime, 0f, 0f);
            transform.Translate(movement);
            distanceMoved += Mathf.Abs(movement.x);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemies")) {
            other.GetComponent<EnemyController>().Demage(1);
            Destroy(gameObject);
            return;
        }
    }
}
