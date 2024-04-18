using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerMusic : MonoBehaviour
    

{
    public float totalTime = 217f;
    private float currentTime;
    
  public void TimerStart()
    {
        currentTime = totalTime;
        InvokeRepeating("UpdateTimer", 1f, 1f); 
    }

    void UpdateTimer()
    {
        currentTime--;

        if (currentTime <= 0)
        {
            currentTime = 0;
            Debug.Log("Timer finished!");
            CancelInvoke("UpdateTimer");
            SceneManager.LoadScene("Map_LevelDesign");
        }
        else
        {
          
        }

    }   
}
