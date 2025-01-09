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

    public static PauseMenuController player;
    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PauseMenuController>();
        }
    }

    // Xu ly gan thong tin cho xe
    public void setCar(Sprite image, string quality, int price)
    {
        carImage.sprite = image;
        qualityCar.text = quality;
        this.price = price;
        carPrice.text = this.price.ToString();
    }

    // Xu  ly mua xe
    public void OnBtnBuyClicked()
    {
        if (player.SpeedCar(price))
        {
            btnBuy.interactable = false;
            carPrice.text = "Purchased!";
        }
    }
}
