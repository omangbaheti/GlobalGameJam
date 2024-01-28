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
    //public float coolDown;
    private bool hasFired = false;


    void Start()
    { 
        inputSystem = GetComponentInParent<InputSystem>();   
    }

    void Update()
    {
        if( inputSystem.shoot && !hasFired){
            LaunchProjectile();

        }
        shootingPoint = m_MainCamera.transform;    
        //StartCoroutine(LaunchProjectile());
    }

    public void LaunchProjectile()
    {
        hasFired = true;
        Vector3 shootingDirection = m_MainCamera.transform.forward;
        
        GameObject projectile = Instantiate(poop, shootingPoint.position, Quaternion.identity);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        
        if(projectileRb != null){
            projectileRb.velocity = shootingDirection * projectileSpeed;
        }

        Invoke("ResetFireFlag", 0.5f);

        //yield return new WaitForSeconds(coolDown);
    }

    void ResetFireFlag()
    {
        hasFired = false;
    }
}
