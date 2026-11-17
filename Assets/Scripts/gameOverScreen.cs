using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class gameOverScreen : MonoBehaviour
{
    //variables
    public CameraController CameraController;
    public PlayerController PlayerController;   
    public SwingSword sword;

    public void activate()
    {
        //make game over screen seen
       gameObject.SetActive(true);

        //just making player not do anything
       CameraController.GetComponent<CameraController>().enabled = false;
       PlayerController.GetComponent<PlayerController>().enabled = false;
       sword.GetComponent<SwingSword>().enabled = false;

        //making cursor usable
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }


  public void restart()
  {
        //load scene again
    SceneManager.LoadScene("Main Game");
  }


  public void quit()
  {
        //close exe
    Application.Quit();

  }
}
