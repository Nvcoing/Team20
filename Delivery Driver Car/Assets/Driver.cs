using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
	// Variables
	[SerializeField] float steerSpeed = 300f;
	[SerializeField] float moveSpeed = 20f;
	[SerializeField] float slowSpeed = 15f;
	[SerializeField] float boostSpeed = 30f;
    private SpriteRenderer spriteRenderer; // Thêm tham chiếu đến SpriteRenderer

    // Start is called before the first frame update
    void Start()
	{

        //transform.Rotate(0,0,45);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer không tìm thấy trên object xe!");
        }
    }

	// Update is called once per frame
	void Update()
	{
		//transform.Rotate(0,0,45);
		//transform.Rotate(0,0,steerSpeed);
		//transform.Translate(0,moveSpeed,0);
		float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
		float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		transform.Rotate(0, 0, -steerAmount);
		transform.Translate(0, moveAmount, 0);

		// Time.deltatime how long each frame took to execute

	}
	void OnCollisionEnter2D(Collision2D other)
	{
		Debug.Log("Lowering the Speed");
		moveSpeed = slowSpeed;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Speed Up")
		{
			Debug.Log("BOOST! Speed up incoming!");
			moveSpeed = boostSpeed;
		}
	}
}
