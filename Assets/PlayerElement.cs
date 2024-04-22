using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerElement : MonoBehaviour
{

    ElementBoardManager elementBoardManager;

    public int fireNumber = 2;
    public int waterNumber = 2;
    public int lighteningNumber = 2;
    public int greenNumber = 2;
    public int rockNumber = 2;

    public SwordAttack swordAttack;

    public float buffMultiplier;
    public float buffAdder;



    private void Start()
    {
        elementBoardManager = GameObject.FindGameObjectWithTag("ElementBar").GetComponent<ElementBoardManager>();
        elementBoardManager.refreshBoard();
    }

    public void AddWaterElement(int amount)
    {
        waterNumber += amount;
        calculateElementBuff();
        elementBoardManager.refreshBoard();
    }

    public void AddFireElement(int amount)
    {
        fireNumber += amount;
        calculateElementBuff();
        elementBoardManager.refreshBoard();
    }

    public void AddLighteningElement(int amount)
    {
        lighteningNumber += amount;
        calculateElementBuff();
        elementBoardManager.refreshBoard();
    }

    public void AddGreenElement(int amount)
    {
        greenNumber += amount;
        calculateElementBuff();
        elementBoardManager.refreshBoard();
    }

    public void AddRockElement(int amount)
    {
        rockNumber += amount;
        calculateElementBuff();
        elementBoardManager.refreshBoard();
    }


    public void calculateElementBuff()
    {
        //find the distance between user element ratio and weapon's
        float[] userRatio = new float[] { fireNumber, waterNumber, lighteningNumber, greenNumber, rockNumber };
        float[] weaponRatio = swordAttack.swordElement;

        float divisor = userRatio.Sum() / weaponRatio.Sum();
        float[] userRatioNormalized = userRatio.Select(num => num / divisor).ToArray();

        float[] resultArray = userRatioNormalized.Zip(weaponRatio, (a, b) => a - b).ToArray();
        float[] squaredResultArray = resultArray.Select(num => num * num).ToArray();

        float squaredDistance = squaredResultArray.Sum();
        float distance = (float)Mathf.Sqrt(squaredDistance);

        //calculate the maximum distance if the user just average out the elements.
        float averageUserNormalized = userRatio.Sum() / divisor / 5;
        float[] averageUserRatioNormalized = new float[] { averageUserNormalized, averageUserNormalized, averageUserNormalized, averageUserNormalized, averageUserNormalized };

        float[] averageResultArray = averageUserRatioNormalized.Zip(weaponRatio, (a, b) => a - b).ToArray();
        float[] squaredAverageResultArray = averageResultArray.Select(num => num * num).ToArray();

        float averageSquaredDistance = squaredAverageResultArray.Sum();
        float maxDistance = (float)Mathf.Sqrt(averageSquaredDistance);

        //calculte mutiplier and assign the variables for this script and swordattack script.
        if(distance == 0 & maxDistance == 0)
        {
            buffMultiplier = 2;
            swordAttack.elementBuff = 2;
        }
        else if(distance > maxDistance)
        {
            buffMultiplier = 1;
            swordAttack.elementBuff = 1;
        }
        else
        {
            swordAttack.elementBuff = Mathf.Exp(Mathf.Log(2) * (1 - distance / maxDistance));
            buffMultiplier = swordAttack.elementBuff;
        }

        //calculate the addition base on the element number sum
        buffAdder = (fireNumber + waterNumber + lighteningNumber + greenNumber + rockNumber) * 0.1f;
        swordAttack.elementAdder = buffAdder;

        Debug.Log(distance + " " + maxDistance +" "+ swordAttack.elementBuff + ". the buff adder is "+ buffAdder);
    }
}
