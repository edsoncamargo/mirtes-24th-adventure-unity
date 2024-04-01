using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouDieController : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI _textTimer;
    private int _timer = 5;

    void Start() {
        _textTimer.text = "0" + _timer;
        StartCoroutine(SetTimeout());
    }

    IEnumerator Timer() {
        while (_timer > 0) {
            _timer--;
            _textTimer.text = "0" + _timer;
            yield return new WaitForSeconds(1f);
        }

        if (_timer == 0)
            SceneManager.LoadScene("Adventure");
    }

    IEnumerator SetTimeout() {
        yield return new WaitForSeconds(1f);
        StartCoroutine(Timer());
    }
}
