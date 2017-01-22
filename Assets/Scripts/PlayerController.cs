using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ForceAndTime
{
    public Vector3 ForceToExert;
    public float TimeTillForceApply;

    public ForceAndTime(Vector3 _Force, float _Time)
    {
        ForceToExert = _Force;
        TimeTillForceApply = _Time; 
    }
}

public class ExplosionObject
{
    public Vector3 ScreenPositionOfExplosion;
    public float ExplosionStartTime;
    public ForceAndTime CalculatedValues;
    public bool Remove = false;

    public ExplosionObject(Vector3 _ExplosionLocation, float _ExplosionStartTime)
    {
        ScreenPositionOfExplosion = _ExplosionLocation;
        ExplosionStartTime = _ExplosionStartTime;
    }
}

public class PlayerController : MonoBehaviour
{
    public float speed;

    private List<ExplosionObject> Explosions = new List<ExplosionObject>();
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
        if (GameManagerReference.Paused)
            return;


        foreach (ExplosionObject explosion in Explosions)
        {
            explosion.CalculatedValues = CalculateForceAndTimeTillHit(explosion.ScreenPositionOfExplosion, explosion.ExplosionStartTime);

            if (explosion.CalculatedValues.TimeTillForceApply > Time.time)
                continue;

            RigidbodyReference.AddForce(explosion.CalculatedValues.ForceToExert);
            explosion.Remove = true;
            
        }

        Explosions.RemoveAll(fte => fte.Remove);
     
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
        {
            GetComponent<AudioSource>().Play();
            GameManagerReference.GameOver();
        }
    }

    private ForceAndTime CalculateForceAndTimeTillHit(Vector3 _ScreenPostionOfExplosion, float _TimeOfExplosion)
    {
        Vector3 compareBetweenPlayerAndExplosion = (Camera.main.ScreenToWorldPoint(_ScreenPostionOfExplosion) - transform.position);
        float distanceBetweenPlayerAndExplosion = compareBetweenPlayerAndExplosion.magnitude;
        Vector3 direction = (compareBetweenPlayerAndExplosion / distanceBetweenPlayerAndExplosion);
        
        return new ForceAndTime(direction * (speed / (distanceBetweenPlayerAndExplosion / 10)) * -1, _TimeOfExplosion + (distanceBetweenPlayerAndExplosion / 10));
    }

    public void PushShipFromExplosion(Vector3 _ExplosionScreenPosition)
    {
        Explosions.Add(new ExplosionObject(_ExplosionScreenPosition, Time.time));       
    }
}