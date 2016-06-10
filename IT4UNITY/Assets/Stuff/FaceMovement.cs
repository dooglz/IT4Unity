using UnityEngine;

public class FaceMovement : MonoBehaviour
{
    private Vector3 lastpos;

    // Use this for initialization
    private void Start()
    {
        lastpos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 dist = transform.position - lastpos;
        lastpos = transform.position;
        if (dist.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Slerp(
                  transform.rotation,
                  Quaternion.LookRotation(dist),
                  Time.deltaTime * 5.0f
                  );
        }
        //rotate to face velocity
        // Vector3 dir = rb.velocity.normalized;
        // transform.rotation = Quaternion.LookRotation(dir);
        /*
        if (dir != Vector3.zero)
        {
          transform.rotation = Quaternion.Slerp(
              transform.rotation,
              Quaternion.LookRotation(dir),
              Time.deltaTime * 1.0f
          );
        }
        */
    }
}