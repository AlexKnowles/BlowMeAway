﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private List<GameObject> Planets;
    private GameManager GameManagerReference;
    float intervalToNextPlanet;

    // Use this for initialization
    void Start()
    {
        Planets = new List<GameObject>();
        GameManagerReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CreatePlanet(3);

        intervalToNextPlanet = Time.time + (Random.value * 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > intervalToNextPlanet)
            CreatePlanet(Mathf.CeilToInt(Random.Range(1f, 3f)));

        Vector3 bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));

        List<GameObject> itemsToRemove = new List<GameObject>();

        foreach (GameObject planet in Planets)
        {
            if (planet.transform.position.y + planet.GetComponent<Collider>().bounds.size.y < bottomOfScreen.y )                
                itemsToRemove.Add(planet);
        }

        foreach (GameObject planet in itemsToRemove)
        {
            Planets.Remove(planet);
            Destroy(planet);
        }
    }

    private void CreatePlanet(int _Type)
    {
        string assetToLoad = string.Format("Prefabs/Planets/Planet_{0}", _Type.ToString().PadLeft(3, '0'));
        GameObject newPlanet = (Resources.Load(assetToLoad, typeof(GameObject)) as GameObject);

        float xSeed = Random.Range(0, Screen.width);
        Vector3 newItemPosition = Camera.main.ScreenToWorldPoint(new Vector3(xSeed, Screen.height, 20));
        
        Planets.Add(Instantiate(newPlanet, newItemPosition, Quaternion.identity, gameObject.transform));
        
        intervalToNextPlanet = Time.time + (Random.value * (10 - Mathf.FloorToInt(GameManagerReference.Score / 1000)));
    }
}
