using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text Score;
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
}
