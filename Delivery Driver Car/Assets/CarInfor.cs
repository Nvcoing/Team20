using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CarInfor : MonoBehaviour
{
    public Image carImage;
    public TMP_Text qualityCar;
    public TMP_Text carPrice;
    public Button btnBuy;
    private int price;
    private Sprite carSprite; // Thêm biến lưu sprite xe
    private Driver playerDriver; // Tham chiếu đến Driver component

    public static PauseMenuController player;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PauseMenuController>();
        }

        // Tìm Driver component
        playerDriver = FindObjectOfType<Driver>();
    }

    // Xu ly gan thong tin cho xe
    public void setCar(Sprite image, string quality, int price)
    {
        carImage.sprite = image;
        carSprite = image; // Lưu sprite để sử dụng sau
        qualityCar.text = quality;
        this.price = price;
        carPrice.text = this.price.ToString();
    }

    // Xu ly mua xe
    public void OnBtnBuyClicked()
    {
        if (player.SpeedCar(price))
        {
            btnBuy.interactable = false;
            carPrice.text = "Purchased!";

            // Thay đổi sprite của xe người chơi
            if (playerDriver != null)
            {
                SpriteRenderer playerSprite = playerDriver.GetComponent<SpriteRenderer>();
                if (playerSprite != null)
                {
                    playerSprite.sprite = carSprite;
                }
            }
        }
    }
}
