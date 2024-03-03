using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject timeGO;
    TextMeshProUGUI timetext;
    float time = 300;

    GameObject liveGO;
    TextMeshProUGUI livetext;
    int live = 3;

    GameObject scoreGO;
    TextMeshProUGUI scoretext;
    int score = 0;

    GameObject coinGO;
    TextMeshProUGUI cointext;
    int coin = 0;

    GameObject audioGO;
    AudioManager audio;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        timeGO = GameObject.FindWithTag("Time");
        timetext = timeGO.GetComponent<TextMeshProUGUI>();
        liveGO = GameObject.FindWithTag("Live");
        livetext = liveGO.GetComponent<TextMeshProUGUI>();
        scoreGO = GameObject.FindWithTag("Score");
        scoretext = scoreGO.GetComponent<TextMeshProUGUI>();
        coinGO = GameObject.FindWithTag("Coin");
        cointext = coinGO.GetComponent<TextMeshProUGUI>();
        audioGO = GameObject.FindWithTag("Audio");
        audio = audioGO.GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timeGO = GameObject.FindWithTag("Time");
        timetext = timeGO.GetComponent<TextMeshProUGUI>();
        liveGO = GameObject.FindWithTag("Live");
        livetext = liveGO.GetComponent<TextMeshProUGUI>();
        scoreGO = GameObject.FindWithTag("Score");
        scoretext = scoreGO.GetComponent<TextMeshProUGUI>();
        coinGO = GameObject.FindWithTag("Coin");
        cointext = coinGO.GetComponent<TextMeshProUGUI>();
        audioGO = GameObject.FindWithTag("Audio");
        audio = audioGO.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        time -= Time.deltaTime;
        timetext.SetText((int)time + "");
        livetext.SetText(live + "");
        scoretext.SetText(score + "");
        cointext.SetText(coin + "");
    }

    public void getScore(int gainz)
    {
        Debug.Log(gainz);
        score += gainz;
    }

    public void coined()
    {
        coin += 1;
        getScore(100);
    }

    public void die()
    {
        live -= 1;
        audio.PlayMarioDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void liveup()
    {
        live += 1;
    }
}
