using UnityEngine;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour
{
	public GameObject bulletPrefab;
    public Camera cam;

    private Animator anim;
    private float speed = 5f;
    private float turn_speed = 20f;

    
    public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

	[Command]
	void CmdFire()
	{
		// This [Command] code is run on the server!

		// create the bullet object locally
		var bullet = (GameObject)Instantiate(
			bulletPrefab, transform.position - transform.forward,
			Quaternion.identity);

		bullet.GetComponent<Rigidbody>().velocity = transform.forward * 100;

		// spawn the bullet on the clients
		NetworkServer.Spawn(bullet);

		// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
		Destroy(bullet, 2.0f);
	}

    void Start()
    {
        // if this is not my player, remove the camera
        if (!isLocalPlayer)
        { 
            Destroy(cam);
        }
        anim = GetComponent<Animator>();
    }

    void Update()
	{
        if (!isLocalPlayer) return;
 
        //var x = Input.GetAxis("Horizontal")*0.1f;
        //var z = Input.GetAxis("Vertical")*0.1f;

        //transform.Translate(x, 0, z);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        // Command function is called from the client, but invoked on the server
        //}

        if (Input.anyKey)
        {
            if (Input.GetButton("Fire1"))
            {
                CmdFire();
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("assault_combat_run")) { 
                    anim.Play("assault_combat_shoot");
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, -turn_speed * Time.deltaTime);
                anim.Play("assault_combat_idle");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, turn_speed * Time.deltaTime);
                anim.Play("assault_combat_idle");
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.Play("assault_combat_run");
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                anim.Play("assault_combat_run");
                transform.Translate(Vector3.back * Time.deltaTime * speed);
            }
        }
        else
        {
            anim.Play("assault_combat_idle");         
        }                    
        }
}