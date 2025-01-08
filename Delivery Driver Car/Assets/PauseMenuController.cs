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
    [SerializeField] private TextMeshProUGUI coinText; // Hiển thị số coin

    public int currentCoins = 0; // Số coin hiện tại
    public int winConditionCoins = 1000; // Điều kiện thắng

    void Start()
    {
        timer = countdownTime; // Đặt thời gian bắt đầu là 5 phút
        UpdateCountdownText(); // Hiển thị giá trị ban đầu (5:00)
        UpdateCoinText(); // Hiển thị số coin ban đầu
    }

    void Update()
    {
        // Đếm ngược thời gian
        if (!isPaused)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0)
            {
                timer = 0; // Đảm bảo không xuống dưới 0
                CheckGameEnd(); // Kiểm tra điều kiện kết thúc game
            }
            UpdateCountdownText();
        }

        // Bật/tắt menu tạm dừng
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

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinText();
    }

    private void CheckGameEnd()
    {
        if (currentCoins >= winConditionCoins)
        {
            SceneManager.LoadScene("Win"); // Chuyển sang scene Win nếu đạt đủ coin
        }
        else
        {
            SceneManager.LoadScene("Lost"); // Chuyển sang scene Lost nếu không đủ coin
        }
    }

    private void UpdateCoinText()
    {
        coinText.text = "" +currentCoins;
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

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
