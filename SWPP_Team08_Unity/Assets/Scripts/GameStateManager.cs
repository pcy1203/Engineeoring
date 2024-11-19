using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameState;

public class GameStateManager : MonoBehaviour
{
    private PlayerController playerController;
    private UIManager uiManager;
    private SceneController sceneController;
    private static GameStateStrategy gameStateStrategy;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Duck").GetComponent<PlayerController>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        sceneController = GameObject.Find("UIManager").GetComponent<SceneController>();
        gameStateStrategy = new GameStateStrategy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterGamePlayState()
    {
        gameStateStrategy.SetState(new GamePlayState());
        gameStateStrategy.ChangePlayerSettings(playerController);
        gameStateStrategy.ShowUI(uiManager);
        gameStateStrategy.ChangeScene(sceneController);
    }

    public void EnterGamePauseState()
    {
        gameStateStrategy.SetState(new GamePauseState());
        gameStateStrategy.ChangePlayerSettings(playerController);
        gameStateStrategy.ShowUI(uiManager);
        gameStateStrategy.ChangeScene(sceneController);
    }

    public void EnterGameOverState()
    {
        gameStateStrategy.SetState(new GameOverState());
        gameStateStrategy.ChangePlayerSettings(playerController);
        gameStateStrategy.ShowUI(uiManager);
        gameStateStrategy.ChangeScene(sceneController);
    }

    public void EnterStageClearState()
    {
        gameStateStrategy.SetState(new StageClearState());
        gameStateStrategy.ChangePlayerSettings(playerController);
        gameStateStrategy.ShowUI(uiManager);
        gameStateStrategy.ChangeScene(sceneController);
    }

    public string GetState()
    {
        return gameStateStrategy.GetState();
    }
}