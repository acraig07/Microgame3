using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] _enemies;
    public float _timeBetweenSpawnLow = 0.5f;
    public float _timeBetweenSpawnHigh = 3f;
    float _spawnCools;

    Vector2 _bounds;
    Vector3 _spawnPosition;

    public Text _scoreText;
    int _score = 0;

    public GameObject _gameOverUI;
    public bool _gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        _bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        _spawnCools = Random.Range(_timeBetweenSpawnLow, _timeBetweenSpawnHigh);
        _scoreText.text = "Score: " + _score.ToString();
        _gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_spawnCools > 0)
            _spawnCools -= Time.deltaTime;
        else 
            SpawnEnemy();

        _scoreText.text = "Score: " + _score.ToString();

        if (_gameOver && Input.anyKeyDown)
            Restart();
    }

    void SpawnEnemy()
    {
        _spawnPosition = new Vector3(Random.Range(-_bounds.x + 1f, _bounds.x -1f), _bounds.y + Random.Range(0.25f, 3f), 0f);
        Instantiate(_enemies[Random.Range(0, _enemies.Length)], _spawnPosition, Quaternion.Euler(0,0,180));
        _spawnCools = Random.Range(_timeBetweenSpawnLow, _timeBetweenSpawnHigh);
    }

    public void AddScore(int amount)
    {
        _score += amount;
    }

    void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }

}
