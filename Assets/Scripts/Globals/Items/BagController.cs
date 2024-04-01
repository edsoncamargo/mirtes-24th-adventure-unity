using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour {

    [SerializeField]
    private List<string> _items;

    [SerializeField]
    private AudioSource _audioSource;

    private void Start() {
        _items = new List<string>();
    }

    public List<string> GetItems() {
        return _items;
    }

    public void AddItems(string item) {
        _audioSource.Play();
        Debug.Log(_audioSource);
        _items.Add(item);
    }
}
