using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : BaseMenu
{
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;

    private GameManager gameManager;

    public override void InitState(MenuController ctx)
    {
        base.InitState(ctx);
        state = MenuController.MenuStates.Pause;
        gameManager = GameManager.Instance;
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnResumeButtonClicked()
    {
        gameManager.UnpauseGame();
    }
    private void OnMainMenuButtonClicked()
    {
        gameManager.UnpauseGame();
        GameManager.Instance.LoadScene("Title");
        Debug.Log("Title Screen");
    }
}