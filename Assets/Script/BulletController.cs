using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D _bulletRigidbody;
    public float _speed;
    public int _damage = 1;
    // Start is called before the first frame update
    void Awake()
    {
        _bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        _bulletRigidbody.AddForce(Vector2.up * _speed);
        Invoke("Disable", 5f);
    }

    private void Disable()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(_damage);
            Invoke("Disable", 0.001f);
        }
    }
}
