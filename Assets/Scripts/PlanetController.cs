using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private List<GameObject> Planets;
    private GameManager GameManagerReference;
    float intervalToNextPlanet;

    private float PreviousScoreWhenPlanetWasAdded = 0;

    // Use this for initialization
    void Start()
    {
        Planets = new List<GameObject>();
        GameManagerReference = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        CreatePlanet(1);

        intervalToNextPlanet = Time.time + (Random.value * 10);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (PreviousScoreWhenPlanetWasAdded < GameManagerReference.Score + 10
                && Time.time > intervalToNextPlanet 
                && !GameManagerReference.Paused)
            CreatePlanet(Mathf.CeilToInt(1));

        Vector3 bottomOfScreen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));

        List<GameObject> itemsToRemove = new List<GameObject>();

        foreach (GameObject planet in Planets)
        {
            if (planet.transform.position.y + planet.GetComponent<Collider>().bounds.size.y < bottomOfScreen.y)
                itemsToRemove.Add(planet);
            else
                planet.transform.Rotate(Vector3.up, 20 * Time.deltaTime);
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
        
        Planets.Add(Instantiate(newPlanet, newItemPosition, Random.rotation, gameObject.transform));

        PreviousScoreWhenPlanetWasAdded = GameManagerReference.Score;

        intervalToNextPlanet = Time.time + Mathf.Tan(GameManagerReference.Score + 1);
    }
}
