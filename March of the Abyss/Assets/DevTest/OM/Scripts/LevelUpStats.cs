using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelUpStats : MonoBehaviour
{
    public int level = 1;
    public float experience { get; private set; }
    public Text lvlText;
    public Image expBarImage;

    public static int ExpNeedToLvlUp(int currentLevel)

    {
        if (currentLevel == 0)
            return 0;

        return (currentLevel * currentLevel * currentLevel) * 5;

    }


    public void SetExperience(float exp)
    {
        experience += exp;

        float expNeeded = ExpNeedToLvlUp(level);
        float previousExperience = ExpNeedToLvlUp(level - 1);


        //Level up with exp
        if (experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExperience = ExpNeedToLvlUp(level - 1);
        }

        //Fill exp bar image with exp
        expBarImage.fillAmount = (experience - previousExperience) / (expNeeded - previousExperience);

        //Reset the fillbar
        if(expBarImage.fillAmount == 1)
        {
            expBarImage.fillAmount = 0;
        }
    }

    //What happens when the player levels up
    public void LevelUp()
    {
        level++;
        lvlText.text = level.ToString("");
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
