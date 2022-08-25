using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField]
    private GameObject nicknamePopup;

    [SerializeField]
    private GameObject waitForOpponentPopup;

    [SerializeField]
    private TMP_InputField nicknameInputField;

    public void OnLocalClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnOnlineClicked()
    {
        nicknamePopup.SetActive(true);
    }

    public void OnConfirmNicknameClicked()
    {
        if (!string.IsNullOrEmpty(nicknameInputField.text))
        {
            nicknamePopup.SetActive(false);
        }
    }
}
