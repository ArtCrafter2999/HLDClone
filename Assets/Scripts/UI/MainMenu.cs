using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject darkScreen;
        [SerializeField] private GameObject image;
        public void ShowDarkScreen()
        {
            darkScreen.SetActive(true);
        }
        public void ShowImage()
        {
            image.SetActive(true);
        }
        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }
        public void ExitGame()
        {
            print("Application.Quit();");
            Application.Quit();
        }
    }
}