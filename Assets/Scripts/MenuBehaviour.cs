using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
public class MenuBehaviour : MonoBehaviour
{
    public GameObject Options;
    public GameObject MainMenu;
    public GameObject NewGameMenu;
    public Button ContinueButton;
    public AudioMixer MenuMixer;
    public Slider Volume;
    public Text VolumeText;
    public Toggle ScreenSettings;
    public Text FOVtext;
    public Slider FOVslider;
    public Text Sensitivitytext;
    public Slider Sensslider;
    public GameObject OnEnterSettings,OnEnterNewGame, OnExit;
    public Text Version;
    private void Start()
    {
        Time.timeScale = Activators.timeLine;
        Activators.volume = PlayerPrefs.GetFloat("volume");
        Activators.forscreen = PlayerPrefs.GetInt("FullScreen");
        Activators.level = PlayerPrefs.GetInt("level");
        Activators.defaultFOV = PlayerPrefs.GetFloat("FOV");
        Activators.mouseSensitivity = PlayerPrefs.GetFloat("sensitivity");
        Activators.isEasy = PlayerPrefs.GetInt("HardMode");
        if (Activators.forscreen == 0)
        {
            Activators.isFullScreen = true;
        }
        else
        {
            Activators.isFullScreen = false;
        }
        if (Activators.isFullScreen)
        {
            ScreenSettings.isOn = true;
        }
        else
        {
            ScreenSettings.isOn = false;
        }
        Screen.fullScreen = Activators.isFullScreen;
        MainMenu.SetActive(true);
        Options.SetActive(false);
        NewGameMenu.SetActive(false);
        if (Activators.level <= 1)
        {
            ContinueButton.interactable = false;
        }
        VolumeText.text = ((int)(Activators.volume * 100)).ToString();
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 5)).ToString();
        FOVtext.text = Activators.defaultFOV.ToString();
        FOVslider.value = Activators.defaultFOV;
        Volume.value = Activators.volume;
        Sensslider.value = Activators.mouseSensitivity;
        Version.text = Application.version;
    }
    private void Update()
    {

        if (Activators.level <= 1)
        {
            ContinueButton.interactable = false;
        }
    }
    public void SetSensitivity(float sensitivity)
    {
        Activators.mouseSensitivity = sensitivity;
        Sensitivitytext.text = ((int)(Activators.mouseSensitivity / 5)).ToString();
    }
    public void SetFOV(float FOV)
    {
        Activators.defaultFOV = FOV;
        FOVtext.text = Activators.defaultFOV.ToString();
    }
    public void SetVolume(float volume)
    {
        Activators.volume = volume;
        MenuMixer.SetFloat("Main", Mathf.Log(Activators.volume) * 20);
        VolumeText.text = ((int)(Activators.volume * 100)).ToString();
    }
    public void FullScreenToggle(bool isfullscreen)
    {
        Activators.isFullScreen = isfullscreen;
        Screen.fullScreen = Activators.isFullScreen;
    }
    public IEnumerator DelayForNewGame()
    {
        yield return new WaitForSeconds(0.35f);
        NewGameMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(OnEnterNewGame);
    }
    public void NewGame()
    {
        MainMenu.SetActive(false);
        StartCoroutine(DelayForNewGame());
    }
    public void Continue()
    {
        SceneManager.LoadScene(Activators.level);
    }
    public IEnumerator DelayForOptions()
    {
        yield return new WaitForSeconds(0.35f);
        Options.SetActive(true);
        EventSystem.current.SetSelectedGameObject(OnEnterSettings);
    }
    public void InputOptions()
    {
        MainMenu.SetActive(false);
        StartCoroutine(DelayForOptions());
    }
    public IEnumerator DelayForMenu()
    {
        yield return new WaitForSeconds(0.35f);
        MainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(OnExit);
    }
    public void BackToMenu()
    {
        Options.SetActive(false);
        NewGameMenu.SetActive(false);
        StartCoroutine(DelayForMenu());
        PlayerPrefs.SetFloat("volume", Activators.volume);
        PlayerPrefs.SetFloat("FOV", Activators.defaultFOV);
        PlayerPrefs.SetFloat("sensitivity", Activators.mouseSensitivity);
        if (Activators.isFullScreen)
        {
            Activators.forscreen = 0;
        }
        else
        {
            Activators.forscreen = 1;
        }
        PlayerPrefs.SetInt("FullScreen", Activators.forscreen);
        PlayerPrefs.Save();
    }
    public void InputExit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
    public void EasyNewGame()
    {
        Activators.level = 1;
        PlayerPrefs.SetInt("level", Activators.level);
        SceneManager.LoadScene(Activators.level);
        Activators.isEasy = 0;
        PlayerPrefs.SetInt("HardMode", Activators.isEasy);
        PlayerPrefs.Save();
    }
    public void HardNewGame()
    {
        Activators.level = 1;
        PlayerPrefs.SetInt("level", Activators.level);
        SceneManager.LoadScene(Activators.level);
        Activators.isEasy = 1;
        PlayerPrefs.SetInt("HardMode", Activators.isEasy);
        PlayerPrefs.Save();
    }
}
