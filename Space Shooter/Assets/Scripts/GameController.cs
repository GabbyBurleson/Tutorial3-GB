using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text restartText;
    public Text gameOverText;
    public Text ScoreText;

    private bool restart;
    private bool gameOver;
    private int score;

    public AudioSource musicSource;
    //public AudioClip musicClipOne;

    void Start()
    {
        restart = false;
        restartText.text = "";
        gameOver = false;
        gameOverText.text = "";
        score = 0;
        UpdateScore();

        StartCoroutine (SpawnWaves());
        //musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }


        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {     
            for (int i = 0; i < hazardCount; i++)
            { 
                Vector3 spawnPosition = new Vector3(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Score: " + score;
    }

    public void GameOver ()
    {

        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
