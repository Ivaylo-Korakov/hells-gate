using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class MetalonStats : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    public Animator animator;
    public Slider healthBar;

    private void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if(HP <= 0)
        {
            //play death
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            //play get hit
            animator.SetTrigger("Damage");
        }
    }
}
