using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    private int _lifes;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;
    private string _nextSide = "right";
    private bool _isGuard = true;

    private Transform _player;
    private Animator _animator;
    private Collider2D _collider;
    private AudioSource _audioSourceDie;
    private AudioSource _audioSourceHit;

    [SerializeField]
    private HealthBarController _healthBar;

    private DropController _dropController;

    void Start() {
        _animator = transform.Find("Skin").GetComponent<Animator>();
        _collider = transform.GetComponent<Collider2D>();
        _collider.enabled = true;
        _audioSourceDie = GetComponents<AudioSource>()[0];
        _audioSourceHit = GetComponents<AudioSource>()[1];
        _dropController = GetComponent<DropController>();

        if (_healthBar != null) {
            _healthBar.Setup(_lifes);
        }
    }

    void Update() {
        if (WasAlive()) {
            HandleGuardPosition();
            HandleChase();
        } else {
            _collider.enabled = false;
        }

        if (_healthBar) {
            _healthBar.transform.parent.transform.eulerAngles = new Vector3(0, 0f, 0);
        }
    }

    private void HandleGuardPosition() {
        if (_pointA && _pointB && _isGuard && WasAlive()) {
            if (Vector3.Distance(_pointA.position, transform.position) == 0) {
                _nextSide = "right";
            } else if (Vector3.Distance(_pointB.position, transform.position) == 0) {
                _nextSide = "left";
            }

            if (_nextSide == "right") {
                transform.position = Vector2.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 180f, 0);
                _animator.SetBool("isRun", true);
            } else if (_nextSide == "left") {
                transform.position = Vector2.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0f, 0);
                _animator.SetBool("isRun", true);
            }
        }
    }

    private void HandleChase() {
        if (_isGuard == false && WasAlive()) {
            string _currentSide = transform.position.x > _player.position.x ? "right" : "left";

            if (_currentSide == "right") {
                Vector2 playerPosition = new Vector2(_player.position.x + 0.5f, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 180f, 0);
            } else if (_currentSide == "left") {
                Vector2 playerPosition = new Vector2(_player.position.x - 0.5f, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0f, 0);
            }

            _animator.SetBool("isRun", true);
        }
    }

    public void Guard() {
        _isGuard = true;
    }

    public void Chase(Transform other) {
        _isGuard = false;
        _player = other;
    }

    public void Demage(int demageValue) {
        if (WasAlive()) {
            _lifes = _lifes - demageValue;
            _audioSourceHit.Play();
            _animator.CrossFade("Hit", 0.1f, 0, 0f);
            _healthBar.CurrentLife = _lifes;

            if (_lifes <= 0) {
                _audioSourceDie.Play();
                _animator.Play(stateName: "Die", 0);

                if (_dropController)
                    _dropController.Drop(transform);
            }
        }
    }

    public void Attack() {
        if (WasAlive()) {
            _animator.Play(stateName: "Attack", 0);
        }
    }

    public bool WasAlive() {
        return _lifes > 0;
    }

    public Animator GetAnimator() {
        return _animator;
    }
}
