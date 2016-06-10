using UnityEngine;

public class player : MonoBehaviour
{
    public BoxCollider bc;
    public GameObject trackGo;
    public Track track = null;
    public Track oldtrack = null;
    public Rigidbody rb;
    public Vector3 lanepos;
    public int lane;
    public float maxVel = 20.0f;
    public GameObject playerExplosion;
    // Use this for initialization
    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        track = null;
        trackGo = null;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.Log("Exit: " + contact.otherCollider.gameObject.name);
        }
        Debug.Log("Exit: " + collisionInfo.other.name);
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        // Debug.Log("stay");
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    public void Explode()
    {
        Instantiate(playerExplosion, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<player>().enabled = false;
        //rb.AddRelativeForce(Vector3.up*1000.0f);
        // Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Track newT = null;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.GetComponents<Track>().Length > 0)
            {
                newT = contact.otherCollider.gameObject.GetComponent<Track>();
                break;
            }
            if (contact.otherCollider.gameObject.GetComponents<Death>().Length > 0)
            {
                Explode();
                return;
            }
        }
        if (newT == null)
        {
            return;
        }
        if (track == null)
        {
            track = newT;
            Debug.Log("From no track to: " + track.gameObject.name);
        }
        else if (newT != track && newT != oldtrack)
        {
            oldtrack = track;
            track = newT;
            if (track == null)
            {
                Debug.Log("Leaving: " + oldtrack.gameObject.name + " Now on: Null");
                trackGo = null;
            }
            else
            {
                trackGo = track.gameObject;
                if (oldtrack != null)
                {
                    Debug.Log("Leaving: " + oldtrack.gameObject.name + " Now on: " + track.gameObject.name);
                }
                else
                {
                    Debug.Log("Leaving: null, Now on: " + track.gameObject.name);
                }
            }
        }

        Debug.Log(track);
    }

    private void OnDrawGizmosSelected()
    {
        if (track != null && trackGo != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(lanepos, 0.2f);
            Gizmos.color = Color.white;
            Vector3 nudge = (lanepos - transform.position);
            //   Debug.Log(transform.position + " _ " + trackGo.transform.right + " _ " + nudge);
            nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y),
                Mathf.Abs(trackGo.transform.right.z)));
            //  Debug.Log(nudge);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position + nudge, 0.3f);
            Gizmos.DrawLine(transform.position, transform.position + (trackGo.transform.right*2.0f));
        }
    }

    private void FixedUpdate()
    {
        if (track != null)
        {
            //move
            lanepos = track.CalculateLaneTarget(transform.position, lane);
            Vector3 nudge = track.CalculateNudge(transform.position, lane);
            transform.position += nudge;
            rb.AddRelativeForce(track.GetAccelerateDirection(transform.position, lane)*track.speedboost*10.0f,
                ForceMode.Force);
            //keep in lane
        }
        //have we fallen off the track?
        if (transform.position.y < -10.0f)
        {
            transform.position = new Vector3(0, 1.0f, 0);
            rb.position.Set(0, 1.0f, 0);
            rb.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            track = null;
            trackGo = null;
        }
        if (rb.velocity.magnitude > maxVel)
        {
            rb.velocity = rb.velocity.normalized*maxVel;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (lane > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            lane--;
        }
        else if (lane < 3 && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            lane++;
        }
    }
}