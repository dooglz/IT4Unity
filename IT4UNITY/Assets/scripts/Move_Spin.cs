using UnityEngine;
using System.Collections;

public class Move_Spin : MonoBehaviour
{
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    private void FixedUpdate()
    {
        
        transform.Rotate(Vector3.up, speed);
    }
}
