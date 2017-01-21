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
    public Vector3 MouseClickLocation;

    public Vector3 translationOfMouseClickLocation;
    private Renderer rendererReference;
    private Color solidColour;
    private Color fadedColour;
    private ParticleSystem particleEmiiter;
    private Vector3 objectSize;

    private void Start()
    {
        objectSize = GetComponent<Renderer>().bounds.size;

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
                translationOfMouseClickLocation = Camera.main.ScreenToWorldPoint(MouseClickLocation);                

                var heading = translationOfMouseClickLocation - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;

                GetComponent<Rigidbody>().AddForce(direction * BombSpeed);


                if (Vector3.Distance(transform.position, translationOfMouseClickLocation) < 1.0f)
                    State = BombStates.Moved;
                break;

            case BombStates.Moved:
                PlayerControllerReference.PushShipFromExplosion(transform.position);
                particleEmiiter.Play();
//              particleEmiiter.transform.position = transform.position;

                StartTime_Fade = Time.time;
                State = BombStates.Exploded;
                break;
            case BombStates.Exploded:
                FadeOut();
                break;
        }                
    }

    void LateUpdate()
    {
        Vector3 screenBounds = new Vector3(Screen.width, 0, Screen.height);
        Vector2 screen;

        screen.x = screenBounds.x;
        screen.y = Screen.height;

        Vector3 playerPosScreen = Camera.main.WorldToScreenPoint(transform.position);


        if (playerPosScreen.x > Screen.width)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-objectSize.x, playerPosScreen.y, playerPosScreen.z));
        else if (playerPosScreen.x < -objectSize.x)
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, playerPosScreen.y, playerPosScreen.z));
    }

    public void SetupBombLocations(Vector3 _MouseClickLocation)
    {
        State = BombStates.Ready;
        StartTime_Move = Time.time;
        MouseClickLocation = _MouseClickLocation;
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
