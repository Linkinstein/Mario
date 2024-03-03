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
        cointext = scoreGO.GetComponent<TextMeshProUGUI>();
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
        cointext = scoreGO.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        timetext.SetText((int)time + "");
        livetext.SetText(live + "");
        scoretext.SetText(score + "");
        cointext.SetText(coin + "");
    }

    public void getScore(int gainz)
    {
        score += gainz;
    }

    public void coined()
    {
        coin += 1;
    }

    public void die()
    {
        live -= 1;

    }

    public void liveup()
    {
        live += 1;
    }
}
