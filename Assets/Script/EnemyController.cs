using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D _enemyRigidbody;
    PlayerController _player;
    public float _xSpeed, _ySpeed;
    public GameObject _bullet;
    public float _timeBetweenAttackLow = 0.5f;
    public float _timeBetweenAttackHigh = 2f;
    float _attackCools;

    public int _maxEnemyHealth = 2;
    private int _enemyHealth;

    GameController _cont;
    public int _amount;
    Vector2 _bounds;

    public GameObject _explosion;
    // Start is called before the first frame update
    void Start()
    {
        _bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerController>();
        _attackCools = Random.Range(_timeBetweenAttackLow, _timeBetweenAttackHigh);
        _enemyHealth = _maxEnemyHealth;
        _cont = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 0f;
        if (_player != null)
            if (_player.transform.position.x > transform.position.x)
                x = _xSpeed;
            else if (_player.transform.position.x < transform.position.x)
                x = -_xSpeed;
        
        _enemyRigidbody.AddForce(new Vector2(x, -_ySpeed ) * Time.deltaTime);

        if (_attackCools > 0)
            _attackCools -= Time.deltaTime;
        else
            Attack();

        if (transform.position.y < -_bounds.y)
        {
            _cont.AddScore(-_amount);
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        Instantiate(_bullet, transform.position, transform.rotation);
        _attackCools = Random.Range(_timeBetweenAttackLow, _timeBetweenAttackHigh);
    }

    public void TakeDamage(int dmg)
    {
        _enemyHealth -= dmg;
        if (_enemyHealth <= 0)
            Die();
    }

    private void Die()
    {
        _cont.AddScore(_amount);
        Instantiate(_explosion, transform.position, Quaternion.Euler(0,0,0));
        Destroy(gameObject);
    }
}
