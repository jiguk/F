using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using UnityEngine.Analytics;//애널리틱스 때문에 추가한 부분.
using UnityEngine.Advertisements;//애즈 때문에 추가한 부분.

public class Manager : MonoBehaviour {
    
    public GameObject Pipe;
    public int Score;
    public Text Score_Text;
    public GameObject WhiteScreen;
    public GameObject Ready_Image_A;
    public GameObject Ready_Image_B;
    public static Manager me;
    public float Speed = 5f;
    public bool GameStarting=false;
    public GameObject Final_GUI;
    public Text Final_Score;
    public Text Final_Best_Score;

    public AudioSource audio;

    void Awake()
    {
        me = this;
    }

    void Start()
    {
        iTween.FadeFrom(WhiteScreen, 0.8f, 0.5f);
        //애즈 때문에 추가한 부분.
        //Advertisement.Initialize("29861");

        ////애널리틱스 때문에 추가한 부분.
        Gender gender = Gender.Female;
        Analytics.SetUserGender(gender);

        int birthYear = 2014;
        Analytics.SetUserBirthYear(birthYear);
        
    }

    public void GameStart()
    {
        GameStarting = true;
        Bird.me.gameObject.GetComponent<Rigidbody>().useGravity = true;
        iTween.FadeTo(Ready_Image_B, 0f, 0.5f);
        iTween.FadeTo(Ready_Image_A, 0f, 0.5f);

        InvokeRepeating("MakePipe", 2f, 2f);
    }

    public void MakePipe()
    {
        Instantiate(Pipe);
    }

    public void GetScore()
    {
        Score++;
        Score_Text.text = Score.ToString();
        audio.clip = Bird.me.GoalSound;
        audio.Play();
    }

    public void GameOver()
    {
        Speed = 0f;
        CancelInvoke("MakePipe");
        iTween.ShakePosition(Camera.main.gameObject, new Vector3(0.5f, 0.5f, 0), 0.5f);
        iTween.FadeFrom(WhiteScreen, 0.8f, 0.5f);

        Bird.me.LookDirection = new Vector3(0, 0, -90f);

        //최종 점수 구문.
        Final_Score.text = Score.ToString();
        int LastBestCore = PlayerPrefs.GetInt("BestScore");
        if (Score > LastBestCore)
        {
            LastBestCore = Score;
            PlayerPrefs.SetInt("BestScore", LastBestCore);
        }
        Final_Best_Score.text = LastBestCore.ToString();

        Final_GUI.SetActive(true);
        iTween.MoveFrom(Final_GUI, new Vector3(320f, -500f, 0), 0.8f);

        //애널 때문에 추가한 구문.

        int totalPotions = 5;
        int totalCoins = 100;
        string weaponID = "Weapon_102";
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
            { "potions", totalPotions },
            { "coins", totalCoins },
            { "activeWeapon", weaponID }
          });

        
    if (Advertisement.IsReady())
    {
      Advertisement.Show();
    }
  

    }

    public void Replay()
    {
        //애널리틱스 때문에 추가한 부분.
        Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);

        Application.LoadLevel(0);
    }

}
