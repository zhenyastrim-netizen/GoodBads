using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public float attackDistance = 1f;
    public float hitboxDuration = 0.1f;
    public float AttackSpeed=1f;
    public float NextHit=0f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time>=NextHit)
        {
            Attack();
            NextHit=Time.time+1f/AttackSpeed;

        }
    }

    void Attack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - transform.position).normalized;

        Vector3 spawnPos = transform.position + (Vector3)direction * attackDistance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject hitbox = Instantiate(
            hitboxPrefab,
            spawnPos,
            Quaternion.Euler(0, 0, angle)
        );

        Destroy(hitbox, hitboxDuration);
    }
}