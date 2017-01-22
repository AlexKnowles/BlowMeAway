using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int Score = 0;
    public bool Paused = false;
    public Canvas CanvasReference;
    
    public void GameOver()
    {
        Paused = true;
        CanvasReference.GetComponent<HUD>().GameOver();
    }
}
