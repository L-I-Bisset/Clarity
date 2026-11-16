using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{

    public void activate()
    {
       gameObject.SetActive(true);
       Debug.Log("ran");
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
