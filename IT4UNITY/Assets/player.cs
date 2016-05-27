using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
  public BoxCollider bc;
  public GameObject trackGo;
  public Track track = null;
  public Rigidbody rb;
  public Vector3 lanepos;
  public float maxVel = 20.0f;
	// Use this for initialization
	void Start () {
    bc = GetComponent<BoxCollider>();
    track = null;
    trackGo = null;
    rb = GetComponent<Rigidbody>();
  }
  void OnCollisionExit(Collision collisionInfo)
  {
    foreach (ContactPoint contact in collisionInfo.contacts)
    {
      Debug.Log("Exit: " + contact.otherCollider.gameObject.name);
    }
    Debug.Log("Exit: " + collisionInfo.other.name);
  }

    void OnCollisionStay(Collision collisionInfo)
  {
   // Debug.Log("stay");
    foreach (ContactPoint contact in collisionInfo.contacts)
    {
      Debug.DrawRay(contact.point, contact.normal, Color.white);
    }
  }
  void OnCollisionEnter(Collision collision)
  {
   // Debug.Log("Enter");
    Debug.Log(collision.contacts.Length);
    foreach (ContactPoint contact in collision.contacts)
    {
     if(contact.otherCollider.gameObject.GetComponents<Track>().Length > 0) {
        trackGo = contact.otherCollider.gameObject;
        track = trackGo.GetComponent<Track>();
        break;
     }
    }
  }
  
  Vector3 CalculateLaneTarget() {
    Vector2 of = new Vector2(track.transform.forward.x, track.transform.forward.z);
    Vector2 oc = new Vector2(transform.position.x- track.transform.position.x,transform.position.z - track.transform.position.z);
    float ang = Vector2.Angle(of,oc);
    float dist = Mathf.Cos(ang*Mathf.Deg2Rad) * oc.magnitude;
    return (track.LaneOrigin + (track.transform.forward * dist));
  }

  void OnDrawGizmosSelected()
  {
  
    if (track != null)
    {
      Gizmos.color = Color.magenta;
      Gizmos.DrawWireSphere(lanepos, 0.2f);
      Gizmos.color = Color.white;
      Vector3 nudge = (lanepos - transform.position);
   //   Debug.Log(transform.position + " _ " + trackGo.transform.right + " _ " + nudge);
      nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y), Mathf.Abs(trackGo.transform.right.z)));
    //  Debug.Log(nudge);
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position+ nudge, 0.3f);
      Gizmos.DrawLine(transform.position , transform.position+(trackGo.transform.right*2.0f));
    }
    
  }
  
  void FixedUpdate() {
    if (track != null)
    {
      //move
      lanepos = CalculateLaneTarget();
      Vector3 nudge = (lanepos - transform.position);
      nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y), Mathf.Abs(trackGo.transform.right.z)));
       transform.position += nudge*0.2f;
      rb.AddRelativeForce(trackGo.transform.forward * track.speedboost * 10.0f,ForceMode.Force);
      //keep in lane
    }
    //have we fallen off the track?
    if(transform.position.y < -10.0f) {
      transform.position = new Vector3(0, 1.0f, 0);
      rb.position.Set(0, 1.0f, 0);
      rb.rotation = Quaternion.identity;
      rb.velocity = Vector3.zero;
      track = null;
      trackGo = null;
    }
    if(rb.velocity.magnitude> maxVel) {
      rb.velocity = rb.velocity.normalized * maxVel;
    }
  }

  // Update is called once per frame
  void Update () {
   
	}
}
