using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour {

    [SerializeField]
    GameObject _itemPrefab;

    public void Drop(Transform other) {
            Instantiate(_itemPrefab, other.position, Quaternion.Euler(0f, 0f, 0f));
    }
}
