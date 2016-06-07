using UnityEngine;
using System.Collections;

public class Corner : Track {
    public Material cornermat;
    // Use this for initialization
    override protected void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override protected void Update () {
        base.Update();
	}

    protected override Material getMat()
    {
        return cornermat;
    }

  public override Vector3 CalculateLaneTarget(Vector3 playerPos, int lane)
  {
        if (lane > lanes - 1) lane = lanes - 1;
        Vector3 lt = (transform.position + (transform.right * (transform.localScale.x * 5.0f)));
    lt.y = playerPos.y;
    return lt;
   }

    public override Vector3 CalculateNudge(Vector3 playerPos, int lane)
    {
        if (lane > lanes - 1) lane = lanes - 1;
        Vector3 lt = CalculateLaneTarget(playerPos,lane);
        //nudge.Scale(new Vector3(Mathf.Abs(trackGo.transform.right.x), Mathf.Abs(trackGo.transform.right.y), Mathf.Abs(trackGo.transform.right.z)));
        Vector3 nudge = (lt - playerPos) * 0.01f;
        nudge.y = 0.0f;
        // return nudge;
        return Vector3.zero;
    }

    public override Vector3 GetAccelerateDirection(Vector3 playerPos, int lane)
    {
        if (lane > lanes - 1) lane = lanes - 1;
        return transform.right;
        //return Vector3.zero;
    }

}
