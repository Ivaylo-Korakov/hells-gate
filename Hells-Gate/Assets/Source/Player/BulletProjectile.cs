using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    [SerializeField] float bulletSpeed = 1f;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        bulletRigidBody.velocity = transform.forward * bulletSpeed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
