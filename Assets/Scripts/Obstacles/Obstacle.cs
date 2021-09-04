using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    /// <summary> The amount of miles to units to determine the actual speed of the obstacles </summary>
    public static float MPH_PER_UNIT = 0.2f;

    public int damage;
    public int health = 1;
    public GameObject HitEffect;

    /// <summary> Speed of the obstacle when spawned. This is the percieved constant speed. </summary>
    [HideInInspector] public float speedOnSpawn = -1;
    private float speed;
    private float playerSpeedOnSpawn;
    private float playerSpeed;

    private Animator animimator;
    private bool doInteractions = true;
    // Speed of actual movement. This changes with the players speed to create the illusion of a constant speed.
    private float moveSpeed;

    public float gameOverAccelSpeed;

    private void Awake()
    {
        GameEvents.instance.onCarStop += Accelerate;
        animimator = GetComponent<Animator>();
    }
    

    private void Start()
    {
        if (speedOnSpawn == -1)
            Debug.LogError("Obstacle speed incorrectly set on spawn");
        playerSpeedOnSpawn = Score.instance.speed;
        moveSpeed = Mathf.Abs(MPH_PER_UNIT * speedOnSpawn);
    }
    

    // Moves FAST. (Used when the player crashes)
    bool accelerating = false;
    private void Accelerate()
    {
        accelerating = true;
        moveSpeed = gameOverAccelSpeed;
    }

    private void OnDestroy()
    {
        GameEvents.instance.onCarStop -= Accelerate;
    }

    public void StopInteractions()
    {
        doInteractions = false;
    }

    /// <summary> Use when this obstacle should play its destroy anim </summary>
    private void DestroyObstacle()
    {
        doInteractions = false;
        animimator.SetTrigger("Destroy");
    }

    /// <summary> Called via destroy animation </summary>
    public void DeleteObstacle()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!Score.instance.gameOver)
        {
            playerSpeed = Score.instance.speed;
            if(!accelerating)
                moveSpeed = MPH_PER_UNIT * (speedOnSpawn - (Score.instance.speed - playerSpeedOnSpawn));
        }
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if(health <= 0)
        {
            DestroyObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndScreen"))
        {
            DeleteObstacle();
        }

        if (doInteractions)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Player>().Damage(damage);
                Instantiate(HitEffect, new Vector3(transform.position.x - 0.5f, transform.position.y, 0), Quaternion.Euler(183.186f, 118.941f, 181.13f));
                DestroyObstacle();
            }

            if (collision.CompareTag("PlayerProjectile"))
            {
                health = health - collision.GetComponent<PlayerProjectile>().Damage;
            }
        }
    }
}
