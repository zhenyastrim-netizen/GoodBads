using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;

    public InputAction FireAction;

    public float bulletSpeed = 20f;
    public float fireRate = 0.15f;
    public int damage = 10;

    private float nextFireTime = 0f;
    private Camera mainCamera;
    public WeaponReload weaponReload;
    void Start()
    {
        FireAction.Enable();
        mainCamera = Camera.main;
       

    weaponReload = GetComponent<WeaponReload>();
    }

    void Update()
    {
        if (FireAction.IsPressed() && Time.time >= nextFireTime)
        {
            Shoot();
             nextFireTime = Time.time + 1f/fireRate;
        }
    }

    void Shoot()
    {
        if (!weaponReload.CanShoot())
        {
        weaponReload.StartReload();
        return;
        }
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0f;

        Vector2 direction = ((Vector2)mousePos - (Vector2)FirePoint.position).normalized;

        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
        weaponReload.SpendAmmo();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(direction);
        bulletScript.SetSpeed(bulletSpeed);
        bulletScript.SetDamage(damage);
       
    }
}