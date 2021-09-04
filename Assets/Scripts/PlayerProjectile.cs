using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    public int Damage;
    public int Speed;

    private void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
            Destroy(gameObject);

        if (collision.CompareTag("EndScreen"))
        {
            Destroy(gameObject);
        }

    }
}
