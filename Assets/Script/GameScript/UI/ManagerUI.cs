using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    #region Variables
    [Header("Panel")]
    [SerializeField] private Image _panelMain;
    [SerializeField] private Image _panelIndication;
    [SerializeField] private Image _panelVictory;
    [Header("Info")]
    [SerializeField] private Image _shipVictory;
    #endregion

    private void OnEnable()
    {
        ControllerShip.OnVictory += InclusionPanelVictory;
    }

    private void OnDisable()
    {
        ControllerShip.OnVictory -= InclusionPanelVictory;
    }
    private void Awake() => Time.timeScale = 0;
 
    public void OnStartGame()
    {
        InclusionPanelIndication();
        ShutDownPanelMain();
        Time.timeScale = 1;
    }
    public void OnRestart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void InclusionPanelIndication() => _panelIndication.gameObject.SetActive(true);
    private void ShutDownPanelMain() => _panelMain.gameObject.SetActive(false);
    private void InclusionPanelVictory(Sprite ship)
    {
        _panelVictory.gameObject.SetActive(true);
        _shipVictory.sprite = ship;
    }
}
