using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialouge : MonoBehaviour
{
    public static Dialouge instance;

    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float TypingSpeed;
    public GameObject ContinueButton;
    public GameObject DialougeCanvas;
    public GameObject MobileUI;
    public Button pauseBtn;

    int ShowDialouge;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        

        //StartCoroutine(Type());
        if (PlayerPrefs.GetInt("ShowDialouge")==0)
        {
            StartCoroutine(Type());

        }
        DialougeCanvas.SetActive(true);
    }

    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            ContinueButton.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        MobileUI.SetActive(false);
        pauseBtn.interactable = false;
        if(GameCtrl.instance.timerOn)
        {
            GameCtrl.instance.timerOn = false;
        }
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(TypingSpeed);

            // comment this
             PlayerPrefs.SetInt("ShowDialouge", 1);
        }
    }

    public void NextSentence()
    {
        //ContinueButton.SetActive(false);

        if(index <sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            ContinueButton.SetActive(false);
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            ContinueButton.SetActive(false);
            DialougeCanvas.SetActive(false);
            MobileUI.SetActive(true);
            pauseBtn.interactable = true;
            GameCtrl.instance.timerOn = true;
        }
    }

    public void deleteDialouge()
    {
        if (PlayerPrefs.GetInt("ShowDialouge") == 1)
        {
            PlayerPrefs.SetInt("ShowDialouge", 0);
            StartCoroutine(Type());
        }
        else
        {

        }
    }
}
