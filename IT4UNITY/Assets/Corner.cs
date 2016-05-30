using UnityEngine;
using System.Collections;

public class Corner : Track {

    // Use this for initialization
    override protected void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    public override Vector3 CalculateLaneTarget(Vector3 playerPos)
    {
        return (transform.position + (transform.right*(transform.localScale.x * 5.0f)*1.5f));
    }

    public override Vector3 CalculateNudge(Vector3 playerPos)
    {
        Vector3 lt = CalculateLaneTarget(playerPos);
        //nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y), Mathf.Abs(trackGo.transform.right.z)));
        Vector3 nudge = (lt - playerPos) * 0.05f;
        nudge.y = 0.0f;
        return nudge;
    }

    public override Vector3 GetAccelerateDirection(Vector3 playerPos)
    {
        //  return transform.right;
        return Vector3.zero;
    }

}
