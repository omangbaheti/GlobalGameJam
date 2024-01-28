using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticlesController: MonoBehaviour
{
    public Color paintColor;
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private TerritoryCalculator _territoryCalculator;
    
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;
    private int playerIndex => playerInput.playerIndex;
    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        
        //var pr = part.GetComponent<ParticleSystemRenderer>();
        //Color c = new Color(pr.material.color.r, pr.material.color.g, pr.material.color.b, .8f);
        //paintColor = c;
    }

    void OnParticleCollision(GameObject other) 
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable p = other.GetComponent<Paintable>();
        if(p != null)
        {
            for  (int i = 0; i< numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.paint(p, pos, radius, hardness, strength, paintColor);
                TerritoryCalculator.instance.UpdateTerritory(pos, radius, playerIndex+1);
            }
            TerritoryCalculator.instance.CalculateCapturedTerritory();

        }
    }
}