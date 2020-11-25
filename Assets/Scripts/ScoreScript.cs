using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    Text score;
    public GameObject youWin;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "CratesPoints:" + scoreValue;

        if (scoreValue == 30)
        {
            Time.timeScale = 0f;
            youWin.SetActive(true);

        }
        else
            youWin.SetActive(false);
    }
}
