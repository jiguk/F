using UnityEngine;
using System.Collections;

public class Pipe : MonoBehaviour {

    public float Speed = 3f;

    void Start()
    {
        transform.position = new Vector3(8f, Random.Range(0f, 8f), 0);
    }
    
	void Update () {

        transform.Translate(-Manager.me.Speed*Time.deltaTime, 0, 0);

        if (transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
	
	}
}
