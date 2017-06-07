using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTimer : MonoBehaviour
{

    private int minutes = 0;
    private int seconds = 0;

    public Text score;
    private GUIText scoreText;

    // Use this for initialization
    void Awake()
    {
        score = GameObject.Find("Score").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator StartTimer()
    {
        Debug.Log(score.text);
        string separator = ":";
        while (true)
        {
            Debug.Log(minutes + ":" + seconds++);
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            separator = ":";
            if (seconds < 10)
                separator += "0";
            
            score.text = minutes + separator + seconds;
            yield return new WaitForSeconds(1);

        }
    }
}
