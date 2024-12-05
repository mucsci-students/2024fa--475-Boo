using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIMainMenu : MonoBehaviour
{
  [SerializeField] Button _startSinglePlayer;
  [SerializeField] Button _Quit;


  public void Start()
  {
    _startSinglePlayer.onClick.AddListener(startSinglePlayer);
    _Quit.onClick.AddListener(QuitGame);
  }

  private void QuitGame()
  {
    Application.Quit();
  }
  private void startSinglePlayer()
  {
    ScenesManager.Instance.LoadScene(ScenesManager.Scene.KartTrack);
  }
}
