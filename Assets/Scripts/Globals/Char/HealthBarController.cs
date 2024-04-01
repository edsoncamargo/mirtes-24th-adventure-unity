using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour {

    [SerializeField]
    private Slider _slider;

    private int _currentLife = int.MaxValue;

    public int TotalLifes {
        set {
            this._slider.maxValue = value;
        }
    }

    public int CurrentLife {
        set {
            this._slider.value = value;
            _currentLife = value;
        }
    }

    public void Setup(int value) {
        TotalLifes = value;
        CurrentLife = value;
    }

    void Update() {
        if (_currentLife <= 0) {
            Destroy(transform.parent.gameObject);
        }
    }
}
