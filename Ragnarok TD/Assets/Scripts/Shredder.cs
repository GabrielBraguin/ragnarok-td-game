using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    GameController game;
    [SerializeField] AudioClip loseHealthSFX;
    float loseHealthSFXvolume;

    private void Start()
    {        
        game = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        loseHealthSFXvolume = PlayerPrefsController.GetSFXVolume();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
        else if (collision.transform.GetComponent<Attacker>())
        {
            if (game.playerHealth <= 0) { return; }
            game.playerHealth -= 1;
            Destroy(game.playerHeart[game.playerHealth]);
            if (!loseHealthSFX) { return; }
            AudioSource.PlayClipAtPoint(loseHealthSFX, Camera.main.transform.position, loseHealthSFXvolume);
            FindObjectOfType<ShakeBehaviour>().TriggerShake();

        }
        Destroy(collision.transform.gameObject);
    }
}
