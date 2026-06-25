using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : MonoBehaviour
{
    public GameObject attackProjectionPrefab;

    public Transform ghostPoint;

    public float attackSpeed = 2f;
    public int damage = 1;

    private float nextHitTime = 0f;

    private int comboIndex = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 0.8f;

    void Update()
    {
        if (Time.time > comboTimer)
            comboIndex = 0;

        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= nextHitTime)
        {
            Attack();
            nextHitTime = Time.time + 1f / attackSpeed;
        }
    }

    void Attack()
    {
        comboIndex++;

        if (comboIndex > 3)
            comboIndex = 1;

        comboTimer = Time.time + comboResetTime;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = (mousePos - ghostPoint.position).normalized;

        GameObject projection = Instantiate(
            attackProjectionPrefab,
            ghostPoint.position,
            Quaternion.identity
        );

        AttackProjection attackProjection = projection.GetComponent<AttackProjection>();
        attackProjection.Setup(comboIndex, damage, direction);
    }
}