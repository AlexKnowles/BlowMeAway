using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text Score;
    public GameObject GameOverSection;
    private GameManager GameManagerReference;

    // Use this for initialization
    void Start ()
    {
        GameManagerReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Score.text = "Score: " + GameManagerReference.Score;
    }

    public void GameOver()
    {
        Score.enabled = false;
        GameOverSection.SetActive(true);
        GameObject.Find("GameOverScreen/FinalScore").GetComponent<Text>().text = string.Format("Final Score: {0}", GameManagerReference.Score);
    }
}
