using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    [Header("Components")]
    public GameObject dialogueObject;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index = 0;

    public float radius = 1;



    void Start() {

    }

    public void Speech(Sprite otherProfile, string[] otherSpeechText, string otherActorNameText) {
        speechText.text = "";
        index = 0;
        dialogueObject.SetActive(true);
        profile.sprite = otherProfile;
        sentences = otherSpeechText;
        actorNameText.text = otherActorNameText;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence() {
        foreach (char letter in sentences[index].ToCharArray()) {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void Hide() {
        speechText.text = "";
        index = 0;
        dialogueObject.SetActive(value: false);
    }

    public void NextSentence() {
        if (speechText.text == sentences[index]) {
            Debug.Log(index);
            if (index < sentences.Length - 1) {
                index++;
                speechText.text = "";
               
                StartCoroutine(TypeSentence());
            } else {
                Hide();
            }
        } else {
            Hide();
        }

        
    }
}

