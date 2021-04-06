using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject gunPrefab;
    AttackerSpawner myLaneSpawner;
    Animator animator;
    [SerializeField] AudioClip shotSFX;
    float shotSFXvolume;

    private void Start()
    {
        shotSFXvolume = PlayerPrefsController.GetSFXVolume();
        SetLaneSpawner();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(IsAttackerInLane())
        {
            animator.SetBool("isAttacking",true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetLaneSpawner()
    {
        AttackerSpawner[] spawners = FindObjectsOfType<AttackerSpawner>();

        foreach (AttackerSpawner spawner in spawners)
        {
            bool IsCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            if (IsCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Fire()
    {
        Instantiate(projectilePrefab, gunPrefab.transform.position, gunPrefab.transform.rotation);
        if (!shotSFX) { return;  }
        AudioSource.PlayClipAtPoint(shotSFX, Camera.main.transform.position, shotSFXvolume);
    }

    
}
