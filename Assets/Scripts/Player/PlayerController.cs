using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D _rb;
    [SerializeField]private float _speed = 10f;
    private float _moveX;

    private Animator _animator;
    private AudioSource _audioSourceJump;
    private AudioSource _audioSourceSplash;
    private AudioSource _audioSourceHit;

    [SerializeField] private float _jumpForce = 20f;
    [SerializeField] private int _jumps = 2;
    [SerializeField] private int _maxJumps = 2;
    private bool _canJump = false;

    [SerializeField]
    private int _initialLifes = 5;
    private int _lifes = 5;
    [SerializeField]
    private TextMeshProUGUI _textLifes;

    [SerializeField]
    private GameObject _ammo;
    private float _fireRate = 2f;
    private float _nextFireTime;

    [SerializeField]
    private GameObject _weapon;

    [SerializeField]
    private GameObject _uiYouDie;

    void Start() {
        _lifes = _initialLifes;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSourceJump = GetComponents<AudioSource>()[0];
        _audioSourceSplash = GetComponents<AudioSource>()[1];
        _audioSourceHit = GetComponents<AudioSource>()[2];
        UpdateTextLifes();
    }

    void Update() {
        HandleMove();
        if (Input.GetButtonDown("Jump")) {
            _canJump = true;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= _nextFireTime && _ammo) {
            Instantiate(_ammo, transform.position, transform.rotation);
            _nextFireTime = Time.time + 1f / _fireRate;
        }

        if (WasAlive() != true) 
            Kill();
    }

    void FixedUpdate() {
        if (_canJump) {
            _canJump = false;

            if (_jumps > 0) {
                _jumps--;
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _audioSourceJump.Play();
            }
        }
    }

    private void HandleMove() {
        _moveX = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveX * _speed, _rb.velocity.y);

        if (_moveX > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _animator.SetBool("isRun", true);
        } else if (_moveX < 0) {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            _animator.SetBool("isRun", true);
        }

        if (_moveX == 0) {
            _animator.SetBool("isRun", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _jumps = _maxJumps;
        }

        if (other.gameObject.CompareTag("Void")) {
            _audioSourceSplash.Play();
            Kill();
        }
    }

    IEnumerator Restart() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    public void Kill() {
        _lifes = 0;
        UpdateTextLifes();
        _weapon.SetActive(false);
        _animator.CrossFade("Die", 0.1f, 0, 0f);
        _uiYouDie.SetActive(true);
        enabled = false;
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            _canJump = false;
        }
    }

    public void Demage(int demageValue) {
        if (WasAlive()) {
            _lifes = _lifes - demageValue;
            UpdateTextLifes();
            _audioSourceHit.Play();
            _animator.CrossFade("Hit", 0.1f, 0, 0f);
            return;
        }

        _audioSourceHit.Play();
        Kill();
    }

    private void UpdateTextLifes() {
        if (_textLifes != null) {
            _textLifes.text = _lifes < 10 ? "0" + _lifes : _lifes.ToString();
        }
    }

    public bool WasAlive() {
        return _lifes > 0;
    }
}
