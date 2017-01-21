using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float BombSpeed = 1;
    public float BombFadeDuration = 0.5f;
    public PlayerController PlayerControllerReference;

    public BombStates State = BombStates.None;
    public float StartTime_Move;
    public float StartTime_Fade;
    public Vector3 EndLocation;

    private Renderer rendererReference;
    private Color solidColour;
    private Color fadedColour;
    private ParticleSystem particleEmiiter;

    private void Start()
    {
        rendererReference = GetComponent<Renderer>();
        particleEmiiter = GetComponent<ParticleSystem>();

        solidColour = rendererReference.material.color;
        fadedColour = new Color(solidColour.r, solidColour.g, solidColour.b, 0f);

    }

    // Update is called once per frame
    void Update ()
    {
        switch(State)
        {
            case BombStates.Ready:
                transform.position = Vector3.MoveTowards(transform.position, EndLocation, Time.deltaTime * BombSpeed);
                
                if (transform.position.Equals(EndLocation))
                    State = BombStates.Moved;
                break;

            case BombStates.Moved:
                PlayerControllerReference.PushShipFromExplosion(transform.position);
                particleEmiiter.Play();
                StartTime_Fade = Time.time;
                State = BombStates.Exploded;
                break;
            case BombStates.Exploded:
                FadeOut();
                break;
        }                
    }

    public void SetupBombLocations(Vector3 _EndPosition)
    {
        State = BombStates.Ready;
        StartTime_Move = Time.time;
        EndLocation = _EndPosition;
    }

    private void FadeOut()
    {
        rendererReference.material.color = Color.Lerp(solidColour, fadedColour, (Time.time - StartTime_Fade) / BombFadeDuration);

        if (rendererReference.material.color.Equals(fadedColour)) 
        {
            RenderParticleEffect(EndLocation);
        }
            
    }

    private void RenderParticleEffect(Vector3 _EndPosition) 
    {
        
        particleEmiiter.transform.position = _EndPosition;
        particleEmiiter.Play();
        Debug.Log(particleEmiiter.time);
        if (particleEmiiter.time > 20) 
        {
            PlayerControllerReference.RemoveBomb(gameObject);
        }
        

    }
}
