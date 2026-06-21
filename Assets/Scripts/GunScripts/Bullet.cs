using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private int damage;

    public float lifeTime = 3f;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

    if (enemy != null)
    {
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
}
}