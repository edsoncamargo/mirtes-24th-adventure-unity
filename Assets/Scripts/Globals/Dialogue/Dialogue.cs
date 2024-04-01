using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    public Sprite sprite;
    public string[] speechText;
    public string actorName;
    [SerializeField]
    private DialogueController _dialogueController;

    public LayerMask dialogueLayer;
    public float radius;

    public bool isOpened = false;
    public bool alreadyRead = false;

    void Start() {

    }


    void FixedUpdate() {
        Interact();
    }

    public void Interact() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, dialogueLayer);

        if (hit != null && isOpened == false && alreadyRead == false) {
            isOpened = true;
            alreadyRead = true;
            _dialogueController.Speech(sprite, speechText, actorName);
        } else {
            isOpened = false;
        }
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
