using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 100;
    [SerializeField] GameObject HitVFX;
    [SerializeField] AudioClip HitSFX;
    float HitSFXvolume;

    public float Damage { get => damage; set => damage = value; }

    private void Start()
    {
        HitSFXvolume = PlayerPrefsController.GetSFXVolume();
    }

    public void Hit()
    {
        TriggerHit();
        if (CompareTag("Projectile"))
        {
            Destroy(transform.parent.gameObject);
        }
    }

    private void TriggerHit()
    {
        if (!HitVFX) { return; }
        GameObject hitVFXobject = Instantiate(HitVFX, transform.position, transform.rotation);
        Destroy(hitVFXobject, 1f);
        if (!HitSFX) { return; }
        AudioSource.PlayClipAtPoint(HitSFX, Camera.main.transform.position, HitSFXvolume);
    }



}
