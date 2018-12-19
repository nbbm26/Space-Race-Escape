using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour {

    public int ringsToCollect;
    public int carsToDestroyed;
    private int amountOnTimer;
    private int currentLevel = 1;
    private bool currentLevelComplete = false;
    private int highestUnlockedLevel;
    private int goldMetal;
    private int silverMetal;
    private int bronzeMetal;
    
	// Use this for initialization
	void Start () {
        StartLevel1();
	}
	
	// Update is called once per frame
	void Update () {

        //If the current level is complete.
        if (currentLevelComplete)
        {
            //Goes to the next level
            currentLevel = currentLevel + 1;

            //This switch level will set the variables for the next level
            switch (currentLevel)
            {
                case 1:
                    StartLevel1();
                    break;
                case 2:
                    StartLevel2();
                    break;
                default:
                    Debug.Log("Level could not be set, since there is no such level");
                    break;
            }

            //Reset currentLevelComplete to false
            currentLevelComplete = false;
        }

	}

    void StartLevel1()
    {
        ringsToCollect = 5;
        carsToDestroyed = 5;
        amountOnTimer = 2; //In minutes
        
        //If this is the highest unlocked level, set the highest unlocked level to reflect that
        if (!(highestUnlockedLevel > currentLevel))
        {
            highestUnlockedLevel = currentLevel;
        }

        //Initializes variable for the level 
        goldMetal = 1000;
        silverMetal = 700;
        bronzeMetal = 400;
    }

    void StartLevel2()
    {
        ringsToCollect = 6;
        carsToDestroyed = 5;
        amountOnTimer = 6; //In minutes

        //If this is the highest unlocked level, set the highest unlocked level to reflect that
        if (!(highestUnlockedLevel > currentLevel))
        {
            highestUnlockedLevel = currentLevel;
        }

        goldMetal = 3000;
        silverMetal = 2400;
        bronzeMetal = 1700;
    }

}
