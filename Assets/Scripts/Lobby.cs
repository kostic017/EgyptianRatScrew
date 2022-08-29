using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void OnLocalClicked()
    {
        SceneManager.LoadScene("LocalGame");
    }

    public void OnOnlineClicked()
    {
        SceneManager.LoadScene("OnlineGame");
    }
}
