using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    public static Bird me;

    public float Power = 10f;
    public bool Death = false;

    public AudioClip JumpSound;
    public AudioClip GoalSound;
    public AudioClip DeathSound;



    public Vector3 LookDirection;
    public GameObject Image;

    public Rigidbody rigidbody;
    public AudioSource audio;

    void Awake()
    {
        me = this;
    }

	void Update () {

        if (Manager.me.GameStarting == true)
        {
            Rotation();
        }

        if (Input.GetMouseButtonDown(0) && Death == false)
        {
            if (Manager.me.GameStarting == false)
            {
                Manager.me.GameStart();
            }

            rigidbody.velocity = new Vector3(0, 0, 0);
            rigidbody.AddForce(0f, Power, 0f, ForceMode.Impulse);
            audio.clip = JumpSound;
            audio.Play();
        }

        
	}

    void OnTriggerEnter(Collider Target)
    {
        if (Target.gameObject.name == "Pipe" && Death == false)
        {
            Death = true;
            audio.clip = DeathSound;
            audio.Play();

            Manager.me.GameOver();
        }

        if (Target.gameObject.name == "Goal" && Death == false)
        {     
            Manager.me.GetScore();
        }
    }

    void Rotation()
    {
        if (Death == false)
        {
            LookDirection.z = GetComponent<Rigidbody>().velocity.y * 5f+20f;
        }

        Quaternion R = Quaternion.Euler(LookDirection);
        Image.transform.rotation = Quaternion.RotateTowards(Image.transform.rotation, R, 3f);
    }

}
