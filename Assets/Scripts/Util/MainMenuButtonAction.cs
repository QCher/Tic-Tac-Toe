using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class MainMenuButtonAction : MonoBehaviour {

    [SerializeField]
    private Enums.ButtonType buttonType;
    private Button button;
    [SerializeField]
    private GameObject AISettings;
    void Awake()
    {
        button = (button == null) ? GetComponent<Button>() : button;
        switch (buttonType)
        {
            case Enums.ButtonType.MULTIPLAYER:
                button.onClick.AddListener(() => { GameManager.Instance.StartGame(Enums.GameType.MULTIPLAYER); });
                break;
            case Enums.ButtonType.PLAYERvsAI:
                button.onClick.AddListener(() => { GameManager.Instance.StartGame(Enums.GameType.PLAYERvsAI); });
                break;
            case Enums.ButtonType.SHOW_AI_SETTINGS:
                button.onClick.AddListener(() => 
                {
                    if (AISettings!=null)
                    {
                        AISettings.SetActive(true);
                    }
                });
                break;
            case Enums.ButtonType.CLOSE_AI_SETTINGS:
                button.onClick.AddListener(() =>
                {
                    if (AISettings != null)
                    {
                        AISettings.SetActive(false);
                    }
                });
                break;
            case Enums.ButtonType.QUIT:
                button.onClick.AddListener(() => { GameManager.Instance.QuitGame(); });
                break;
            default:
                break;
        }
    }
	
}
