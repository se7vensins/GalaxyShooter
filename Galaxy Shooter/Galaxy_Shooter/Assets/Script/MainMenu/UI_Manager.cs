using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Text _gameOver;

    [SerializeField]
    private Text _restartGame;

    [SerializeField]
    private Sprite[] _livesSprites;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager = null");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;

    }
    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSeq();
        }
    }
    private void GameOverSeq()
    {
        _gameManager.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartGame.gameObject.SetActive(true);
        StartCoroutine(_gameOverFlicker());
    }

    IEnumerator _gameOverFlicker()
    {
        while (true)
        {
            _gameOver.text = "Game Over man, GAME OVER!";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = " ";
            yield return new WaitForSeconds(0.5f);

        }
    }
}
