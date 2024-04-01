using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    private bool _isPause = false;
    private GameObject _ui;

    void Start() {
        _ui = transform.Find("UI").gameObject;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void Pause() {
        Time.timeScale = 0;
        _isPause = true;
        _ui.SetActive(_isPause);
    }

    public void Resume() {
        Time.timeScale = 1;
        _isPause = false;
        _ui.SetActive(_isPause);
    }

    void TogglePause() {
        _isPause = !_isPause;

        if (_isPause) {
            Pause();
        } else {
            Resume();
        }
    }
}
