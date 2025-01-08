using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private bool isButtonClicked = false; 

 
    public void StartGame()
    {
        if (isButtonClicked) return; 
        isButtonClicked = true;     

        Debug.Log("Start Game Button Pressed");
        SceneManager.LoadScene("StoreCar");
    }

    
    public void QuitGame()
    {
        if (isButtonClicked) return;
        isButtonClicked = true;

        Debug.Log("Quit Game Button Pressed");
        Application.Quit();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            SceneManager.LoadScene("Menu");  
        }
    }
}
