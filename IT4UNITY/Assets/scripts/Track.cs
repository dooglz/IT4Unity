using UnityEngine;

public
class Track : MonoBehaviour
{
    public
      float speedboost = 1.0f;

    public
      float landespread = 1.0f;

    public
      Vector3 LaneOrigin;

  public int lanes = 1;
    // Use this for initialization
    protected virtual void Start()
    {
        float xx = ((4.0f / lanes) * 0.5f) - 2.0f;
        LaneOrigin = transform.position + (transform.right * xx);
    }

    protected virtual void OnValidate()
    {
    float xx = ((4.0f / lanes) * 0.5f) - 2.0f;
    LaneOrigin = transform.position + (transform.right * xx);
  }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LaneOrigin, 0.2f);
        Gizmos.color = Color.blue;
    for (int a = 0; a < lanes; a++) {


        Gizmos.DrawWireSphere(CalculateLaneTarget(transform.position,a), 0.2f);
    }
    Gizmos.color = Color.white;
    }

    public virtual Vector3 GetAccelerateDirection(Vector3 playerPos, int lane)
    {
      if (lane > lanes) lane = lanes;
    return transform.forward;
    }

    public virtual Vector3 CalculateNudge(Vector3 playerPos, int lane)
    {
     if (lane > lanes) lane = lanes;
      Vector3 lt = CalculateLaneTarget(playerPos,lane);
        //nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y), Mathf.Abs(trackGo.transform.right.z)));
        Vector3 nudge = (lt - playerPos) * 0.1f;
        //never nudge Y
        nudge.y = 0.0f;
        return nudge;
    }

    public virtual Vector3 CalculateLaneTarget(Vector3 playerPos, int lane)
    {
    if (lane > lanes) lane = lanes;
        Vector2 of = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 oc = new Vector2(playerPos.x - transform.position.x,
                                 playerPos.z - transform.position.z);
        float ang = Vector2.Angle(of, oc);
        float dist = Mathf.Cos(ang * Mathf.Deg2Rad) * oc.magnitude;
        float min = transform.localScale.z * -4.8f;
        float max = transform.localScale.z * 5.1f;
        dist = Mathf.Min(Mathf.Max(dist, min),max);
        return (LaneOrigin + (transform.forward * dist) + (transform.right * lane));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        LaneOrigin = transform.position + transform.right * landespread;
    }
}