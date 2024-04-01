using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private GameObject _keyboardMenu;

    [SerializeField]
    private GameObject _buttons;

    void Start() {
    }

    void Update() {

    }

    public void HandleStart() {
        SceneManager.LoadScene(1);
    }

    public void HandleExit() {
        Application.Quit();
    }

    public void HandleKeyboardMenu() {
        _keyboardMenu.SetActive(true);
        _buttons.SetActive(false);
    }

    public void HandleBackMenu() {
        _keyboardMenu.SetActive(false);
        _buttons.SetActive(true);
    }
}
