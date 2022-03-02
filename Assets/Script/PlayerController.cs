using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Starting states")]
    public float _speed;
    Vector2 _input;
    Rigidbody2D _playerRigidBody;

    [Header("Shooting")]
    public GameObject _bullet;
    public GameObject[] _bulletSpawnPosistions;
    private float _cools;
    public float _timeBetweenShots;
    public GameObject _flash;

    [Header("Health")]
    public int _maxHealth = 10;
    public int _health;
    public GameObject _healthImage;
    public GameObject _healthParent;
    public float _timeBetweenHurts = 0.3f;
    float _iframe;
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _cools = _timeBetweenShots;
        _health = _maxHealth;
        _iframe = _timeBetweenHurts;
        for(int i = 0; i < _health - 1; i++)
            AddHeart();
    }

    void AddHeart()
    {
        GameObject _heart = Instantiate(_healthImage);
        _heart.transform.SetParent(_healthParent.transform);
    }

    void RemoveHeart(int n)
    {
        if (_healthParent.transform.childCount > 0)
        {
            if (_healthParent.transform.childCount < n)
                n = _healthParent.transform.childCount;
            
            for (int i = 0; i < n; i++)
                Destroy(_healthParent.transform.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _playerRigidBody.AddForce(_input * _speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && _cools <= 0)
            Shoot();

        if (_cools > 0)
            _cools -= Time.deltaTime;
        if (_iframe > 0) 
            _iframe -= Time.deltaTime;
    }

    void Shoot()
    {
        for (int i = 0; i < _bulletSpawnPosistions.Length; i++)
            Instantiate(_bullet, _bulletSpawnPosistions[i].transform.position, Quaternion.identity);
        
        Instantiate(_flash, transform.position, Quaternion.identity);
        _cools = _timeBetweenShots;
    }
    public void TakeDamage(int damage)
    {
        if (_iframe <= 0)
        {
            RemoveHeart(damage);
            _health = _health - damage;
            if (_health <= 0)
                GameOVer();
            _iframe = _timeBetweenHurts;
        }
    }

    void GameOVer()
    {
        FindObjectOfType<GameController>()._gameOver = true;
        FindObjectOfType<GameController>()._gameOverUI.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}

