using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour {

    private GameObject _keyE;
    private GameObject _key;
    private bool _canEnter = false;

    void Start() {
        _keyE = transform.Find("Keyboard_E").gameObject;
        _key = transform.Find("Key").gameObject;
    }

    void Update() {
        if (_canEnter) {
            if (Input.GetKeyDown(KeyCode.E)) {
                SceneManager.LoadScene("HouseParty");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            BagController _bag = other.GetComponent<BagController>();
            _canEnter = _bag.GetItems().Contains("Key");
            _keyE.SetActive(_canEnter);
            _key.SetActive(!_canEnter);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _canEnter = false;
            _keyE.SetActive(false);
            _key.SetActive(false);
        }
    }
}
