using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    [SerializeField] float bulletSpeed = 1f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    public GameObject[] Detached;
    public int damageAmount;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(bulletSpeed != 0f)
        {
            bulletRigidBody.velocity = transform.forward * bulletSpeed;
        }
    }

    private void Start()
    {
        
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bulletRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        bulletSpeed = 0f;

        ContactPoint contactPoint = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
        Vector3 position = contactPoint.point + contactPoint.normal * hitOffset;

        //Spawn hit effect on collision
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, position, rotation);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contactPoint.point + contactPoint.normal); }

            //Destroy hit effects depending on particle Duration time
            var hitPS = hitInstance.GetComponent<ParticleSystem>();
            if (hitPS != null)
            {
                Destroy(hitInstance, hitPS.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }

        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
                Destroy(detachedPrefab, 1);
            }
        }
        //Destroy projectile on collision
        Destroy(gameObject);

        //Check if it collides with an enemy and call the take dammage func if it does
        if (collision.gameObject.tag == "Enemy")
        {
            transform.parent = collision.transform;
            collision.gameObject.GetComponent<MetalonStats>().TakeDamage(damageAmount);
        }

    }
}
