using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Toggle luckFactorToggle;

    private void Awake()
    {
        luckFactorToggle.isOn = PlayerPrefs.GetInt("LuckFactor", 0) == 1;
    }

    public void OnLocalClicked()
    {
        SceneManager.LoadScene("LocalGame");
    }

    public void OnOnlineClicked()
    {
        SceneManager.LoadScene("OnlineGame");
    }

    public void OnLuckFactorToggle(bool isOn)
    {
        PlayerPrefs.SetInt("LuckFactor", isOn ? 1 : 0);
    }
}
