using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class FinishMenuButtonAction : MonoBehaviour {

    [SerializeField]
    private Enums.FinishMenuButtonType buttonType;
    private Button button;

    void Awake()
    {
        button = (button == null) ? GetComponent<Button>() : button;
        switch (buttonType)
        {
            case Enums.FinishMenuButtonType.BACK:
                button.onClick.AddListener(() => { GameManager.Instance.SwitchToMainMenu(); });
                break;
            case Enums.FinishMenuButtonType.REPLAY:
                button.onClick.AddListener(() => 
                {
                    GameController.Instance.RestartGame();
                    gameObject.transform.parent.gameObject.SetActive(false);
                });
                break;
            default:
                break;
        }

    }
}
