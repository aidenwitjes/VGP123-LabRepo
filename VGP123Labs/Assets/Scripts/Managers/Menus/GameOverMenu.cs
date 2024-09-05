using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : BaseMenu
{
    public Button continueButton;
    public Button mainMenuButton;
    public Button quitButton;
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.GameOver;
        continueButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Level"));
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Title"));
        quitButton.onClick.AddListener(QuitGame);
    }
}