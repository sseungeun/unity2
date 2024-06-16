using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagers : MonoBehaviour
{
    static int score=0;

    public static void setScore(int value){
        score+=value;
    }

    public static int getScore(){
        return score;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
