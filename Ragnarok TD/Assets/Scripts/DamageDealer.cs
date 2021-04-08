using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damage = 100;
    [SerializeField] GameObject hitVFX;
    [SerializeField] AudioClip hitSFX;
    float hitSFXvolume;
    GameObject hitVFXParent;
    const string HITVFX_PARENT_NAME = "HITVFXs";

    public float Damage { get => damage; set => damage = value; }

    private void Start()
    {
        CreateHitVFXParent();
    }

    private void CreateHitVFXParent()
    {
        hitVFXParent = GameObject.Find(HITVFX_PARENT_NAME);
        if (!hitVFXParent)
        {
            hitVFXParent = new GameObject(HITVFX_PARENT_NAME);
        }
    }

    private void Update()
    {
        hitSFXvolume = PlayerPrefsController.GetSFXVolume();
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
        if (!hitVFX) { return; }
        else
        {
            GameObject hitVFXobject = Instantiate(hitVFX, transform.position, transform.rotation) as GameObject;
            hitVFXobject.transform.parent = hitVFXParent.transform;
            Destroy(hitVFXobject, 1f);
        }        
        if (!hitSFX) { return; }
        else
        {
            AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXvolume);
        }        
    }



}
