using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    public void Awake()
    {
        gameObject.SetActive(false);
    }


    public void activate()
  {
    gameObject.SetActive(true);
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
