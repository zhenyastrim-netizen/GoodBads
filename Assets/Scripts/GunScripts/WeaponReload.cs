using UnityEngine;
using System.Collections;

public class WeaponReload : MonoBehaviour
{
    public int maxAmmo = 12;
    public int currentAmmo;

    public float reloadTime = 1.5f;

    public bool IsReloading { get; private set; }

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    public bool CanShoot()
    {
        return currentAmmo > 0 && !IsReloading;
    }

    public void SpendAmmo()
    {
        if (IsReloading)
            return;

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

        if (currentAmmo >= maxAmmo)
            return;

        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        IsReloading = true;

        Debug.Log("Перезарядка...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        IsReloading = false;

        Debug.Log("Перезаряжено");
    }
}