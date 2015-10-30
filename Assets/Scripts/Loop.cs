using UnityEngine;
using System.Collections;

public class Loop : MonoBehaviour {

    public float Speed = 3f;

	void Update () {

        if (Bird.me.Death == false)
        {
            this.gameObject.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * Speed, 0);
        }
	
	}
}
