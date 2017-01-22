using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public void ParticleEffectOnClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        GameObject particleSystem = (Resources.Load("Prefabs/Particle System", typeof(GameObject)) as GameObject);

        GameObject particleSystemMedium = (Resources.Load("Prefabs/Particle System 1", typeof(GameObject)) as GameObject);
        GameObject particleSystemLarge = (Resources.Load("Prefabs/Particle System 2", typeof(GameObject)) as GameObject);

		mousePosition.x = mousePosition.x - 3;
        
        Instantiate(particleSystem, mousePosition, Quaternion.identity, gameObject.transform);
        Instantiate(particleSystemMedium, mousePosition, Quaternion.identity, gameObject.transform);
        Instantiate(particleSystemLarge, mousePosition, Quaternion.identity, gameObject.transform);
    }
}
