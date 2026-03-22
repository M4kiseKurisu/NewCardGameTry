using UnityEngine;

public enum GameState
{
    GameStart,
    PlayerTurn,
    EnemyTurn,
    GameOver
}

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    public RuntimeChessBoard chessBoard { get; private set; }
    public GameState currentState { get; private set; }

    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        chessBoard = new RuntimeChessBoard();
        ChangeState(GameState.GameStart);
    }

    public void ChangeState(GameState newState) {
        currentState = newState;
        switch (currentState) {
            // todo: 进入各个状态的逻辑
            case GameState.GameStart:
                break;
            case GameState.PlayerTurn:
                break;
            case GameState.EnemyTurn:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void InstantiateChess(GameObject chessPrefab, Vector3 position) {
        Instantiate(chessPrefab, position, Quaternion.identity);
    }
}
