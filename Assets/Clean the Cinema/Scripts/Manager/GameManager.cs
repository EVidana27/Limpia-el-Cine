using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [SerializeField] private int pickedItemsProgress;
    [SerializeField] private RandomInstantiation instances;
    [SerializeField] private int progressAmount;


    [Header("Panels")]
    [SerializeField] private GameObject victoryPanel;

    [Header("Game State")]
    [SerializeField] public GameState currentState;

    private void Awake()
    {
        instance = this;
        currentState = GameState.Playing;
    }

    public void AddToProgress()
    {
        pickedItemsProgress++;
    }

    public void UpdateProgress()
    {
        progressAmount += pickedItemsProgress;
        pickedItemsProgress = 0;
    }

    private void Update()
    {
        Debug.Log(currentState);
        if (progressAmount >= instances.TotalItemAmount)
        {
            currentState = GameState.Victory;
            victoryPanel.SetActive(true);
        }
    }


    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
    }

}


public enum GameState
{
    Playing,
    Defeat,
    Victory,
    Waiting
}