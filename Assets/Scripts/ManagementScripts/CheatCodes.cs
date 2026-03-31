using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CheatCodes : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
        
        _inputActions.CheatCode.ExitGame.performed += ExitGame;
        _inputActions.CheatCode.RestartGame.performed += RestartGame;
    }
    private void OnDisable()
    {
        _inputActions.CheatCode.ExitGame.performed -= ExitGame;
        
        _inputActions.Disable();
    }
    private void ExitGame(InputAction.CallbackContext context)
    {
        GameManager.GameOver();
    }

    private void RestartGame(InputAction.CallbackContext context)
    {
        GameManager.Instance.RestartCurrentScene();
    }
}
