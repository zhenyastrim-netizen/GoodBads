using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    public int expAmount = 5;

    public float pickupRange = 3f;
    public float collectDistance = 0.25f;
    public float moveSpeed = 5f;

    private Transform player;
    private PlayerExperience playerExperience;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerExperience = playerObject.GetComponent<PlayerExperience>();
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= pickupRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }

        if (distance <= collectDistance)
        {
            Collect();
        }
    }

    void Collect()
    {
        if (playerExperience != null)
            playerExperience.AddExperience(expAmount);

        Destroy(gameObject);
    }
}