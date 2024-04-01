using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseController : MonoBehaviour {

    private Transform _parent;
    private EnemyController _enemy;

    private void Start() {
        _parent = transform.parent;
        _enemy = _parent.Find("CharObject").GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _enemy.Chase(other.gameObject.GetComponent<Transform>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _enemy.Guard();
        }
    }
}
