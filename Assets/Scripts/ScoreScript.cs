using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;
    public GameObject youWin;
    public AudioSource victory;
    
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        scoreValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Collect all crates: " + scoreValue;

        if (scoreValue == 3)
        {
            victory.Play();
            Time.timeScale = 0f;
            youWin.SetActive(true);

        }
        else
            youWin.SetActive(false);
    }
}
