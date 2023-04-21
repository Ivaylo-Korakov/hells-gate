using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonStats : MonoBehaviour
{
    [SerializeField] private int HP = 75;
    public Animator animator;
    public Slider healthBar;

    private void Update()
    {
        healthBar.value = HP;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            //play death
            animator.SetTrigger("Die");
            //Debug.Log("ded ded");
            //this.enabled = false;
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            //play get hit
            animator.SetTrigger("Damage");
        }
    }
}
