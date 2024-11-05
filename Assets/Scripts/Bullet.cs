using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 50f;
    public float slowAmt = 0.5f;
    public float speed = 10f;

    private Vector3 targetPosition;

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                enemy.ApplySlow(slowAmt);
            }

            Destroy(gameObject);
        }
    }
}
