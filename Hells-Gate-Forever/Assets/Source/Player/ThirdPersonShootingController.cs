using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;

public class ThirdPersonShootingController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private GameObject pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    public GameObject[] SpellPrefabs;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private int _animIDShooting;
    private bool _isShooting;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        _animIDShooting = Animator.StringToHash("Shooting");
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        SpellCycle();
        Aiming(mouseWorldPosition);
        Shooting(mouseWorldPosition);


    }

    private void Aiming(Vector3 mouseWorldPosition)
    {
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            //Adding aiming animation by increasing the layer weight to 1
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);

            //Getting the aiming animation back to 0
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }
    }

    private void Shooting(Vector3 mouseWorldPosition)
    {
        if (starterAssetsInputs.shoot)
        {
            _isShooting = true;

            if (_isShooting)
            {
                //animator.Play("CastSpell");
                animator.SetTrigger("startCast");
                _isShooting = false;
            }

            // PROBLEM - it only plays the animation for a few seconds, i have to find out a way to make it wait for the animation to finish

            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;

         //  animator.SetTrigger("endCast");
        }
    }

    public void SpellCycle()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            pfBulletProjectile = SpellPrefabs[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pfBulletProjectile = SpellPrefabs[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pfBulletProjectile = SpellPrefabs[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            pfBulletProjectile = SpellPrefabs[3];
        }   
    }
}
