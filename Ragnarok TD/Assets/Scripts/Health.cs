using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float health = 200;
    [Range(0.5f, 5f)] [SerializeField] float deathAnimationTime = 1f;
    Animator anim;
    [SerializeField] AudioClip [] deathSFX;
    float deathSFXvolume;
    [SerializeField] AudioClip hurtSFX;
    float hurtSFXvolume;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
        if(CompareTag("Attacker")) { SetDifficultyAttackerHealthModifier(); }
    }

    private void Update()
    {
        deathSFXvolume = PlayerPrefsController.GetSFXVolume();
        hurtSFXvolume = PlayerPrefsController.GetSFXVolume();
    }

    private void SetDifficultyAttackerHealthModifier()
    {
        switch(PlayerPrefsController.GetDifficulty())
        {
            case 1:
                health *= 0.7f;
                break;
            case 3:
                health *= 1.5f;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    public void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();
        if (health <= 0)
        {            
            Die();
        }
        else if (damageDealer.CompareTag("Projectile") || damageDealer.CompareTag("Tank")) 
        {
            anim.Play("Hurt");
            if (!hurtSFX) { return; }
            AudioSource.PlayClipAtPoint(hurtSFX, Camera.main.transform.position, hurtSFXvolume);
        }
        else
        {
            return;
        }        
    }
    private void Die()
    {
        Collider2D colliderx = GetComponent<Collider2D>();
        Destroy(colliderx);
        anim.Play("Death");
        AudioSource.PlayClipAtPoint(deathSFX[UnityEngine.Random.Range(0, deathSFX.Length)], Camera.main.transform.position, deathSFXvolume);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(deathAnimationTime);        
        Destroy(gameObject);
    }

}
