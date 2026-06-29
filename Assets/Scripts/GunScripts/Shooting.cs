using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour, IUsableItem
{
    public Transform FirePoint;
    public GameObject BulletPrefab;

    public float bulletSpeed = 20f;

    // Это задержка между выстрелами.
    // 0.15 = быстро, примерно как автомат
    public float fireCooldown = 0.15f;

    public int damage = 10;

    private float nextFireTime = 0f;
    private Camera mainCamera;
    private WeaponReload weaponReload;

    private void Awake()
    {
        mainCamera = Camera.main;
        weaponReload = GetComponent<WeaponReload>();
    }

    public void Use()
    {
        if (Time.time < nextFireTime)
            return;

        Shoot();
        nextFireTime = Time.time + fireCooldown;
    }

    public void Reload()
    {
        if (weaponReload != null)
        {
            weaponReload.StartReload();
        }
    }

    private void Shoot()
    {
        if (FirePoint == null || BulletPrefab == null)
        {
            Debug.LogError("FirePoint или BulletPrefab не назначены на оружии!");
            return;
        }

        if (weaponReload != null)
        {
            if (!weaponReload.CanShoot())
            {
                weaponReload.StartReload();
                return;
            }

            weaponReload.SpendAmmo();
        }

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector2 direction = ((Vector2)mousePos - (Vector2)FirePoint.position).normalized;

        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
            bulletScript.SetSpeed(bulletSpeed);
            bulletScript.SetDamage(damage);
        }
    }
}