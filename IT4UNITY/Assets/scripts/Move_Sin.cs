using UnityEngine;
using System.Collections;

public class Move_Sin : MonoBehaviour
{
    public enum Directions { UpDown, Sideways };
    public float speed = 1.0f;
    public float mag = 4.0f;
    public float time_offset = 0.0f;
    private Vector3 startpos;
    private Vector3 MoveDir = Vector3.up;
    public Directions direction = Move_Sin.Directions.UpDown;
    // Use this for initialization
    void Start()
    {
        startpos = transform.position;
        if (direction == Directions.UpDown)
        {
            MoveDir = Vector3.up;
        }
        else
        {
            MoveDir = Vector3.left;
        }
    }

    protected virtual void OnValidate()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
        void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (direction == Directions.UpDown)
        {
            MoveDir = Vector3.up;
        }
        else
        {
            MoveDir = Vector3.left;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startpos + (mag * MoveDir), 1.0f);
        Gizmos.DrawWireSphere(startpos - (mag * MoveDir), 1.0f);
        Gizmos.DrawLine(startpos + (mag * MoveDir), startpos - (mag * MoveDir));
        Gizmos.color = Color.white;

    }

    private void FixedUpdate()
    {
        float timeNow = speed * (Time.realtimeSinceStartup + time_offset);
        transform.position = startpos +  (mag * MoveDir * Mathf.Sin(timeNow));
    }

}
