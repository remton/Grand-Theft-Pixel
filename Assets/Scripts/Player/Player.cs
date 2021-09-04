using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Score score;
    private Vector2 targetPos;
    private Animator animator;

    //Used for moving between lanes
    private float changeYUp = 2.3f;
    private float changeYDown = 2.3f;
    private float maxHieght = 3.65f;
    private float minHieght = -0.75f;

    public float speed;
    public int health;
    public GameObject moveEffect;

    private bool canMove = true;

    public GameObject PlayerProjectile;


    public float FailWaitTime;
    private void Start()
    {
        score = gameObject.GetComponent<Score>();
        animator = gameObject.GetComponent<Animator>();
        health = PlayerData.instance.activeCar.health;
        targetPos = transform.position;
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            return;
        score.Damage(dmg);
    }

    // Update is called once per frame
    void Update ()
    {
        if (gameOver)
            return;

        if(transform.position.y == targetPos.y)
        {
            canMove = true;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);

        if (Input.GetButtonDown("Up") && transform.position.y < maxHieght && canMove == true)
        {
            Instantiate(moveEffect, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.Euler(0, -70, 0));
           
            targetPos = new Vector2(transform.position.x, transform.position.y + changeYUp);
            canMove = false;
        }
        if (Input.GetButtonDown("Down") && transform.position.y > minHieght && canMove == true)
        {
            Instantiate(moveEffect, new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z), Quaternion.Euler(0, -70 ,0));
    
            targetPos = new Vector2(transform.position.x, transform.position.y - changeYDown);
            canMove = false;
        }

        if(health <= 0)
            GameOver();
    }

    private bool gameOver = false;
    private void GameOver()
    {
        gameOver = true;
        gameObject.GetComponent<Score>().GameOver();
        animator.SetTrigger("Crash");
        animator.SetBool("isCrashed", true);
        StartCoroutine("Fail");
    }
    
    IEnumerator Fail()
    {
        yield return new WaitForSeconds(FailWaitTime);
        yield return new WaitUntil(() => Input.anyKey == true);
        SceneManager.LoadScene("MainMenu");
    }

    //Called via crash anim
    public void OnCarStop()
    {
        GameEvents.instance.OnCarStop();
    }


}
