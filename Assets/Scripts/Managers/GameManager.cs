using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class GameManager : Singleton<GameManager> {

    public GameSettings Settings = new GameSettings();
    [SerializeField]
    private GameObject AISettingsMenu;
    protected GameManager() { }


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame(Enums.GameType type)
    {
        switch (type)
        {
            case Enums.GameType.MULTIPLAYER:
                StartMultiplayerGame();
                break;
            case Enums.GameType.PLAYERvsAI:
                StartPlayerVSAIGame();
                break;
            default:
                break;
        }
    }

    private void StartMultiplayerGame()
    {
        this.Settings.useAI = false;
        SceneManager.LoadScene(1);
    }

    private void StartPlayerVSAIGame()
    {
        this.Settings.useAI = true;
        SceneManager.LoadScene(1);
    }

    public void SwitchToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowAISettings(bool bState)
    {
        AISettingsMenu.SetActive(bState);
    }

    

    public void QuitGame()
    {
        Application.Quit();
    }



}
