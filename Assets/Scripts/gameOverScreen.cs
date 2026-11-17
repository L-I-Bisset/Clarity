using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class gameOverScreen : MonoBehaviour
{
 
    public CameraController CameraController;
    public PlayerController PlayerController;   
    public SwingSword sword;

    public void activate()
    {
       gameObject.SetActive(true);
       CameraController.GetComponent<CameraController>().enabled = false;
       PlayerController.GetComponent<PlayerController>().enabled = false;
       sword.GetComponent<SwingSword>().enabled = false;

       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }


  public void restart()
  {
    SceneManager.LoadScene("Main Game");
  }


  public void quit()
  {
    Application.Quit();

  }
}
