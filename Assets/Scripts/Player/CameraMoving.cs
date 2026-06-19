using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed=5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition=new Vector3(player.position.x,
        player.position.y,
        transform.position.z);

        transform.position=Vector3.Lerp(transform.position,
        targetPosition,
        smoothSpeed*Time.deltaTime);
        
    }
}
