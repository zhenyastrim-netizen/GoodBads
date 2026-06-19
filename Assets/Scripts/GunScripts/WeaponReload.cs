using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class WeaponReload : MonoBehaviour
{
    public InputAction ReloadAction;

    public int maxAmmo = 12;
    public int currentAmmo;

    public float reloadTime = 1.5f;

    public bool IsReloading { get; private set; }

    void Start()
    {
        currentAmmo = maxAmmo;
        ReloadAction.Enable();
    }

    void Update()
    {
        if (ReloadAction.WasPressedThisFrame())
        {
            StartReload();
        }
    }

    public bool CanShoot()
    {
        return currentAmmo > 0 && !IsReloading;
    }

    public void SpendAmmo()
    {
        currentAmmo--;

        if (currentAmmo <= 0)
        {
            StartReload();
        }
    }

    public void StartReload()
    {
        if (IsReloading)
            return;

        if (currentAmmo == maxAmmo)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        IsReloading = true;

        // сюда потом можно добавить анимацию
        // animator.SetBool("IsReloading", true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        IsReloading = false;

        // animator.SetBool("IsReloading", false);
    }
}
