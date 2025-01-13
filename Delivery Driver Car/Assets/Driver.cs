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
    private SpriteRenderer spriteRenderer;

    private float normalSpeed; // Lưu trữ tốc độ bình thường
    private Coroutine speedResetCoroutine; // Để quản lý coroutine phục hồi tốc độ

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer không tìm thấy trên object xe!");
        }
        normalSpeed = moveSpeed; // Lưu tốc độ ban đầu
    }

    // Update is called once per frame
    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Lowering the Speed");
        moveSpeed = slowSpeed;

        // Hủy coroutine cũ nếu đang chạy
        if (speedResetCoroutine != null)
        {
            StopCoroutine(speedResetCoroutine);
        }

        // Bắt đầu coroutine mới
        speedResetCoroutine = StartCoroutine(ResetSpeedAfterDelay());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Speed Up")
        {
            Debug.Log("BOOST! Speed up incoming!");
            moveSpeed = boostSpeed;

            // Hủy coroutine phục hồi tốc độ nếu đang chạy
            if (speedResetCoroutine != null)
            {
                StopCoroutine(speedResetCoroutine);
            }
        }
    }

    IEnumerator ResetSpeedAfterDelay()
    {
        yield return new WaitForSeconds(5f); // Đợi 5 giây
        moveSpeed = normalSpeed; // Phục hồi về tốc độ bình thường
        Debug.Log("Speed restored to normal");
    }
}