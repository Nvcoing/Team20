using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;

    [SerializeField] private float countdownTime = 300f; // 5 phút = 300 giây
    private float timer;

    [SerializeField] private TextMeshProUGUI countdownText;

    void Start()
    {
        timer = countdownTime; // Đặt thời gian bắt đầu là 5 phút
        UpdateCountdownText(); // Hiển thị giá trị ban đầu (5:00)
    }

    void Update()
    {
        // Đếm ngược thời gian (dùng unscaledDeltaTime để không phụ thuộc vào Time.timeScale)
        if (!isPaused)
        {
            timer -= Time.unscaledDeltaTime; // Sử dụng unscaledDeltaTime để đảm bảo đếm ngược
            if (timer <= 0)
            {
                timer = 0; // Đảm bảo không xuống dưới 0
                LoadNextScene(); // Hết giờ, chuyển sang scene khác
            }
            UpdateCountdownText(); // Cập nhật TMP Text hiển thị
        }

        // Kiểm tra nhấn phím Escape để bật/tắt menu tạm dừng
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextScene()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian bình thường khi chuyển scene
        SceneManager.LoadScene("Lost"); // Thay "Lost" bằng tên scene bạn muốn chuyển đến
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timer / 60); // Tính số phút
        int seconds = Mathf.FloorToInt(timer % 60); // Tính số giây
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Hiển thị dạng MM:SS
    }
}
