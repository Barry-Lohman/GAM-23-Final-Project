﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarabhoming : MonoBehaviour
{
    private GameObject targetPlayer;
    [SerializeField] public float force;
    [SerializeField] public float rotationForce;
    [SerializeField] public float secondsBeforeHoming;
    [SerializeField] public float launchForce;
    public bool shouldFollow;
    public Rigidbody rb;
    public GameObject Explosion;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitBeforeHoming());
    }

    private void Update()
    {
        if (shouldFollow)
        {
            targetPlayer = CharacterShip.Player.gameObject;

            //Debug.Log(targetPlayer);

            if (targetPlayer)
            {
                Vector3 direction = targetPlayer.transform.position - transform.position;
                direction.Normalize();
                Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
                rb.angularVelocity = rotationAmount * rotationForce;
                rb.velocity = transform.forward * force;
            }
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Instantiate(Explosion, transform.position, transform.rotation);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    } 
  

    private IEnumerator WaitBeforeHoming()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        yield return new WaitForSeconds(secondsBeforeHoming);
        shouldFollow = true;
    }

}