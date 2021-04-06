using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    protected Animator animator;
    protected GameObject currentTarget;
    [SerializeField] AudioClip meleeHitSFX;
    float meleeHitSFXVolume;

    protected void Start()
    {
        meleeHitSFXVolume = PlayerPrefsController.GetSFXVolume();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        if (!currentTarget || currentTarget.GetComponent<Health>().health <= 0)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        GameObject otherObject = otherCollider.gameObject;
        CheckOpponent(otherObject);
    }

    protected virtual void CheckOpponent(GameObject otherObject)
    {
        if (CompareTag("Defender") && otherObject.GetComponent<Attacker>())
        {
            Attack(otherObject);
        }

        if (CompareTag("Attacker") && otherObject.GetComponent<Defender>())
        {
            Attack(otherObject);
        }
    }

    protected void Attack(GameObject target)
    {
        {
            animator.SetBool("isAttacking", true);
            currentTarget = target;
        }
    }
    public void StrikeCurrentTarget()
    {

        Health health = currentTarget.GetComponent<Health>();
        if (!currentTarget || health.health <= 0) { return; }
        if (health)
        {
            health.ProcessHit(GetComponent<DamageDealer>());
            if (!meleeHitSFX) { return; }
            AudioSource.PlayClipAtPoint(meleeHitSFX, Camera.main.transform.position, meleeHitSFXVolume);
        }
    }
}
