using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f;
    public float speed = 2f;

    private float defaultSpeed;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    public void ApplySlow(float slowAmt)
    {
        speed = defaultSpeed * slowAmt;
        StartCoroutine(RemoveSlowEffect());
    }

    IEnumerator RemoveSlowEffect()
    {
        yield return new WaitForSeconds(2f);
        speed = defaultSpeed;
    }
}
