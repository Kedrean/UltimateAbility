using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject bulletPrefab;
    public float fireCooldown = 1f;
    public int maxShots = 4;
    public float bulletSpeed = 20f;
    public float zoomOutAmt = 10f;

    private int currentShots;
    private bool isUltActive = false;
    private float fireTimer = 0f;
    private float originalFOV;

    // Start is called before the first frame update
    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        originalFOV = playerCamera.fieldOfView;
        currentShots = maxShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isUltActive)
        {
            ActivateUltimate();
        }

        if (isUltActive && Input.GetButtonDown("Fire1") && fireTimer <= 0f && currentShots > 0)
        {
            FireBullet();
        }

        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }

        if (currentShots <= 0)
        {
            EndUltimate();
        }
    }

    void ActivateUltimate()
    {
        isUltActive = true;
        fireTimer = fireCooldown;
        currentShots = maxShots;
        playerCamera.fieldOfView += zoomOutAmt;
    }

    void FireBullet()
    {
        currentShots--;
        fireTimer = fireCooldown;

        Vector3 bulletStartPosition = new Vector3(-15f, transform.position.y, transform.position.z);
        Quaternion bulletRotation = Quaternion.Euler(0, -180f, 0);

        GameObject bullet = Instantiate(bulletPrefab, bulletStartPosition, bulletRotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetTargetPosition(new Vector3(15f, bulletStartPosition.y, bulletStartPosition.z));
        }
    }

    void EndUltimate()
    {
        isUltActive = false;
        playerCamera.fieldOfView = originalFOV;
    }
}
