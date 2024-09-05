using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;
    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Pause;
        resumeButton.onClick.AddListener(JumpBack);
        mainMenuButton.onClick.AddListener(() => GameManager.Instance.LoadScene("Title"));
        quitButton.onClick.AddListener(QuitGame);
    }
}