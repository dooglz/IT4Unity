using UnityEngine;
using System.Collections;

public class Move_Sin : MonoBehaviour
{
    public enum Directions { UpDown, Sideways };
    public float speed = 1.0f;
    public float mag = 4.0f;
    public float time_offset = 0.0f;
    private Vector3 startpos;
    public Directions direction = Move_Sin.Directions.UpDown;
    // Use this for initialization
    void Start()
    {
        startpos = transform.position;
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startpos + (mag * Vector3.up), 1.0f);
        Gizmos.DrawWireSphere(startpos - (mag * Vector3.up), 1.0f);
        Gizmos.DrawLine(startpos + (mag * Vector3.up), startpos - (mag * Vector3.up));
        Gizmos.color = Color.white;

    }

    private void FixedUpdate()
    {
        float timeNow = speed * (Time.realtimeSinceStartup + time_offset);
        transform.position = startpos +  (mag * Vector3.up * Mathf.Sin(timeNow));
    }

}
