using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI timerText;     
    [SerializeField] float remainingTime;      
    public GameObject FailedPanel;              //Gameobject panel used to intiantiate after time is over
    public Transform parent;                    //Parent gamebject where Failed Panel will instaite

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update the time
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //Instatiate the failed panel after time is over
            Instantiate(FailedPanel, parent);
        }    

        //Format of time in minute and seconds
        int minute = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minute, seconds);
    }
}
