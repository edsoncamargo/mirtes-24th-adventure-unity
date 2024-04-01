using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackController : MonoBehaviour {

    public GameObject projectilePrefab;
    private bool playerInsideTrigger = false;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private EnemyController _enemyController;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInsideTrigger = false;
        }
    }

    private void Start() {
        StartCoroutine(SpawnObjectCoroutine());
    }

    IEnumerator SpawnObjectCoroutine() {
        while (true && _player.GetComponent<PlayerController>().WasAlive()) {
            if (playerInsideTrigger && _enemyController.WasAlive()) {
                if (_player.position.x < transform.position.x) {
                    Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, -180f, 0f));
                } else {
                    Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0f, 360f, 0f));
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}