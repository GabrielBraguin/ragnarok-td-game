using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [Header("Enemy Game Statistics")]
    [Range(0f, 3f)] [SerializeField] float currentSpeed = 1f;
    [SerializeField] bool facingRight = true; //Depends on if your animation is by default facing right or left
    public GameController game;

    private void Awake()
    {
        game = FindObjectOfType<GameController>();
        game.AttackerSpawned();
    }

    private void Start()
    {
        if (facingRight)
            Flip();
    }

    void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        if (game.gameOver) { return; }
        game.AttackerKilled();
    }

    public void Flip()
    {
        facingRight = false;
        Transform newChild = transform.Find("Body Sprite");
        Vector3 theScale = newChild.localScale;
        theScale.x *= -1;
        newChild.localScale = theScale;
    }

    public void SetMovementSpeed (float speed)
    {
        currentSpeed = speed;
    }

    public void SetDifficultyMoveSpeedModifier ()
    {
        switch (PlayerPrefsController.GetDifficulty())
        {
            case 1:
                currentSpeed *= 0.8f;
                break;
            case 3:
                currentSpeed *= 1.2f;
                break;
            default:
                break;
        }
    }

    public void AddGold(int amount)
    {
        FindObjectOfType<CurrencyDisplay>().AddGold(amount);
        GameObject goldCoin = Instantiate(
            Resources.Load("GoldCoin", typeof(GameObject)), gameObject.transform.position, gameObject.transform.rotation) 
            as GameObject;
        Destroy(goldCoin, 1f);
    }

}
