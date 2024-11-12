using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private ThirdPersonController controller;
    private bool active = false;
    bool isPaused = false;
    bool cursorVisibility;
    CursorLockMode cursorState;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Unpause();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(pauseKey)){
            if(!isPaused){
                Pause();
            }else
            {
                Unpause();
            }
        }
    }

    public void Pause(){
        isPaused = true;
        // cursorVisibility = Cursor.visible;
        // cursorState = Cursor.lockState;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        controller.enabled = false;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Unpause(){
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = cursorVisibility;
        // Cursor.lockState = cursorState;
        controller.enabled = true;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ChangeLocale(int localeID){
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID){
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }

    public void Quit(){
        Application.Quit();
    }
}
