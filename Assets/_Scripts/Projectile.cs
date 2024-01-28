using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    public GameObject m_MainCamera;
    Transform shootingPoint;
    public GameObject poop;
    public float projectileSpeed = 10f;
    private InputSystem inputSystem;

    private PlayerStatsManager playerStatsManager;
    //public float coolDown;
    private bool hasFired = false;


    void Start()
    { 
        inputSystem = GetComponentInParent<InputSystem>();
        playerStatsManager = transform.parent.GetComponentInChildren<PlayerStatsManager>();
    }

    void Update()
    {
        if( inputSystem.shoot && !hasFired && playerStatsManager.Food >0)
        {
            LaunchProjectile();

        }

        shootingPoint = transform;
        //StartCoroutine(LaunchProjectile());
    }

    public void LaunchProjectile()
    {
        hasFired = true;
        AudioManager.instance.PlaySound("squish1", 15f);
        
        GameObject projectile = Instantiate(poop, shootingPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        
        if(projectileRb != null)
        {
            projectileRb.velocity = m_MainCamera.transform.forward * projectileSpeed;
        }

        playerStatsManager.RemoveFood();
        Invoke("ResetFireFlag", 0.5f);
        //yield return new WaitForSeconds(coolDown);
    }

    void ResetFireFlag()
    {
        hasFired = false;
    }
}
