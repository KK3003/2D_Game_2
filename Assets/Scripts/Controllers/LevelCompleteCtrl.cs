using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // provides access to unity UI
using DG.Tweening;   // for using dotween to make star go to level complete star
/// <summary>
/// handles the level compelete AI
/// </summary>
public class LevelCompleteCtrl : MonoBehaviour
{
    public Button btnNext;       // this button loads the next level
    public Sprite goldenStar;    // awarded when score is above certain value
    public Image Star1;          // the UI image for a white star
    public Image Star2;          // the UI image for a white star
    public Image Star3;          // the UI image for a white star
    public Text txtScore;        // for showing the score
    public int levelNumber;  // to get which level is this
    [HideInInspector]
    public int score;     // the current score [HideInInspector] after development/testing
    public int ScoreForThreeStars;  // score required for earning 3 stars
    public int ScoreForTwoStars;  // score required for earning 2 stars
    public int ScoreForOneStar;  // score required for earning 1 stars
    public int ScoreFoeNextLevel;  // score required for unlocking the next level
    public float animStartDelay;  // brief delay in seconds before golden stars are awarded
    public float animDelay;     // animation delay between each golden star 0.7f

    bool showTwoStars, showThreeStars;   // for checking how many stars to show


    // Start is called before the first frame update
    void Start()
    {
        
        // enable when testing and deploying
        score = GameCtrl.instance.GetScore();  // get the current score

        // update the score text
        txtScore.text = "" + score;

        // determine the number of stars to be awarded
        if(score >= ScoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if(score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if(score <= ScoreForOneStar && score !=0)
        {
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        // called if more than one star is awarded
        if(showTwoStars || showThreeStars)
        {
            StartCoroutine("HandleSecondStarAnim", Star2);
        }
        else
        {
            Invoke("CheckLevelStatus", 1.2f);
        }

    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if(showThreeStars)
        {
            StartCoroutine("HandleThirdStarAnim", Star3);
        }
        else
        {
            Invoke("CheckLevelStatus", 1.2f);
        }
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);

        // cause a delay before showing the next star
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;

        Invoke("CheckLevelStatus", 1.2f);



    }

    void CheckLevelStatus()
    {
        // unlock the next level if a certain score is reached
        if (score >= ScoreFoeNextLevel)
        {
            btnNext.interactable = true;

            // particle effect
            SFXCtrl.instance.ShowSanitizerSparkle(btnNext.gameObject.transform.position);

            // play audio
            AudioCtrl.instance.KeyFound(btnNext.gameObject.transform.position);

            // unlock the next level
            GameCtrl.instance.UnlockLevel(levelNumber);
        }
        else
        {
            btnNext.interactable = false;
        }
    }


    void DoAnim(Image starImg)
    {
        // increase the star size
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);

        // show the golden star
        starImg.sprite = goldenStar;

        // reduce the star size to normal using DOTween animation
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);

        // play an audio effect
        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);

        // show a sparkle effect
        SFXCtrl.instance.ShowSanitizerSparkle(starImg.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
