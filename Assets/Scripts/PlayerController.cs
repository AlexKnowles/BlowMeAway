using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ForceAndTime
{
    public Vector3 Force;
    public float Time;
    public bool Remove = false;

    public ForceAndTime(Vector3 _Force, float _Time)
    {
        Force = _Force;
        Time = _Time; 
    }
}

public class PlayerController : MonoBehaviour
{
    public float speed;

    private List<ForceAndTime> ForcesToExcert = new List<ForceAndTime>();
    private Vector3 PlayerSize;
    private Rigidbody RigidbodyReference;
    private GameManager GameManagerReference;
    private int TopY = 0;

    private void Start()
    {
        GameManagerReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        PlayerSize = GetComponent<Collider>().bounds.size;
        RigidbodyReference = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        foreach (ForceAndTime force in ForcesToExcert)
        {
            if (Time.time >= force.Time)
            {
                RigidbodyReference.AddForce(force.Force);
                force.Remove = true;
            }
        }

        ForcesToExcert.RemoveAll(fte => fte.Remove);
     
        Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);               

        if (playerPosScreen.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-PlayerSize.x, playerPosScreen.y, playerPosScreen.z));
        else if (playerPosScreen.x < -PlayerSize.x)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerPosScreen.y, playerPosScreen.z));



        if (TopY < transform.position.y)
        {
            TopY = Mathf.CeilToInt(transform.position.y);
            GameManagerReference.Score = TopY;
        }
        else if (playerPosScreen.y <= 0)
            GameOver();
    }
    

    public void PushShipFromExplosion(Vector3 _ExplosionPosition)
    {
        var heading = _ExplosionPosition - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        ForcesToExcert.Add(new ForceAndTime(direction * (speed / (distance / 10)) * -1, Time.time + (distance / 10)));
    }

    private void GameOver()
    {
        //todo
        Debug.Log("Game Over");
    }
}