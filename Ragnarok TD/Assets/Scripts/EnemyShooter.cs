using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject gunPrefab;
    Animator animator;
    [SerializeField] AudioClip shotSFX;
    float shotSFXvolume;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Projectiles";

    private void Start()
    {
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }

    private void Update()
    {
        shotSFXvolume = PlayerPrefsController.GetSFXVolume();
    }

    public void Fire()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, gunPrefab.transform.position, gunPrefab.transform.rotation) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        if (!shotSFX) { return; }
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position, shotSFXvolume);
    }
}
