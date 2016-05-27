using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour {
  public float speedboost = 1.0f;
  public float landespread = 1.0f;
  public Vector3 LaneOrigin;
	// Use this for initialization
	void Start () {
    LaneOrigin = transform.position + transform.right * landespread;
  }

  void OnValidate()
  {
    LaneOrigin = transform.position + transform.right * landespread;
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(LaneOrigin, 0.2f);
    Gizmos.color = Color.white;
  }

  // Update is called once per frame
  void Update () {
    LaneOrigin = transform.position + transform.right * landespread;
  }
}
