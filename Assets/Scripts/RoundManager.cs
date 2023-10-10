using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    //Timer variablesd
    float timer;
    float startingTime = 180;
    bool timerOn;
    public TextMeshProUGUI tmpTimer;
    bool underMinute = false;

    //Leaderboard variables
    public GameObject leaderboard;
    public List<PlayerManager> playeList;
    float timeTillActive;

    private void Start()
    {
        RoundStart();
    }
    public void RoundStart()
    {
        timerOn = true;
        timer = startingTime;
        startingTime += 60f;
        tmpTimer.color = Color.black;
    }
    public void RoundEnd()
    {
        tmpTimer.transform.parent.parent.gameObject.SetActive(false);
        ShowLeaderBoard();
    }
    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        if (!underMinute && currentTime < 61)
        {
            underMinute = true;
            tmpTimer.color = Color.red;
        }
        tmpTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    private void Update()
    {
        if (timerOn)
        {
            if(timer >= 0)
            {
                timer -= Time.deltaTime;
                UpdateTimer(timer);
            }
            else
            {
                RoundEnd();
                timer = 0;
                timerOn = false;
            }
        }
    }

    public void ShowLeaderBoard()
    {
        leaderboard.gameObject.SetActive(true);

        if(timeTillActive > 5)
        {
            if (Input.GetButton("South Button"))
            {
                leaderboard.gameObject.SetActive(false);
                RoundStart();
            }
        }
        else
        {
            timeTillActive += Time.deltaTime;
        }
    }
}
