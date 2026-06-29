using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeapon : MonoBehaviour, IUsableItem
{
    public GameObject attackProjectionPrefab;
    public Transform ghostPoint;

    public float attackSpeed = 2f;
    public int damage = 1;

    private float nextHitTime = 0f;

    private int comboIndex = 0;
    private float comboTimer = 0f;
    public float comboResetTime = 0.8f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Time.time > comboTimer)
        {
            comboIndex = 0;
        }
    }

    public void Use()
    {
        if (Time.time < nextHitTime)
            return;

        Attack();
        nextHitTime = Time.time + 1f / attackSpeed;
    }

    public void Reload()
    {
        // У мили оружия перезарядки нет
    }

    private void Attack()
    {
        if (attackProjectionPrefab == null)
        {
            Debug.LogError("Attack Projection Prefab не назначен!");
            return;
        }

        if (ghostPoint == null)
        {
            Debug.LogError("Ghost Point не назначен!");
            return;
        }

        comboIndex++;

        if (comboIndex > 3)
            comboIndex = 1;

        comboTimer = Time.time + comboResetTime;

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        Vector2 direction = ((Vector2)mousePos - (Vector2)ghostPoint.position).normalized;

        GameObject projection = Instantiate(
            attackProjectionPrefab,
            ghostPoint.position,
            Quaternion.identity
        );

        AttackProjection attackProjection = projection.GetComponent<AttackProjection>();

        if (attackProjection != null)
        {
            attackProjection.Setup(comboIndex, damage, direction);
        }
        else
        {
            Debug.LogError("На attackProjectionPrefab нет скрипта AttackProjection!");
        }
    }
}