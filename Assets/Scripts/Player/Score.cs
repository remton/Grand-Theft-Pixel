using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public float payRate; //dollars per mile

    public float moneyGained;
    public Text moneyTxt;

    public float milesDriven;
    public Text milesTxt;

    public int maxSpeed;
    private float acceleration; //in miles per hour per second

    public float speed;
    public GameObject speedometer;
    public Text speedTxt;

    public bool gameOver;

    public List<Sprite> speedometerStages;

    private void Start()
    {
        speed = 0;
        maxSpeed = PlayerData.instance.activeCar.speed;
        acceleration = PlayerData.instance.activeCar.acceleration;
    }

    public void Damage(int dmg)
    {
        speed -= Mathf.Min((speed/3)*dmg, speed);
        moneyGained -= Mathf.Min((moneyGained/5)*dmg , payRate*(1 / dmg));
    }

    private void Update()
    {
        if (!gameOver)
        {
            moneyTxt.text = ("$" + Mathf.RoundToInt(moneyGained).ToString());

            if (speed < maxSpeed)
            {
                speed = speed + acceleration * Time.deltaTime;
            }
            else if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
            UpdateSpeedometer();

            milesDriven += speed / 3600 * Time.deltaTime;
            milesTxt.text = (milesDriven.ToString("F2"));

            moneyGained += payRate * speed  / 3600 * Time.deltaTime;
        }
    }

    public void GameOver()
    {
        gameObject.GetComponent<PlayerWeapon>().SetActive(false);
        gameOver = true;
        float stopTime = gameObject.GetComponent<Animator>().GetClipLength(PlayerData.instance.activeCar.name + "_Crash");
        StartCoroutine(DecelarateSpeedometer(stopTime));
        PlayerData.instance.bank += Mathf.RoundToInt(moneyGained);
    }

    void UpdateSpeedometer()
    {
        speedTxt.text = (Mathf.RoundToInt(speed).ToString());
        int speedometerStageIndex = Mathf.RoundToInt((speed/maxSpeed)*(speedometerStages.Count-1));
        speedometer.GetComponent<Image>().sprite = speedometerStages[speedometerStageIndex];
    }

    IEnumerator DecelarateSpeedometer(float timeUntilStop)
    {
        yield return new WaitForEndOfFrame();
        float decelerationRate = speed / timeUntilStop;
        while (speed >= 0)
        {
            yield return new WaitForEndOfFrame();
            speed -= decelerationRate * Time.deltaTime;
            UpdateSpeedometer();
        }
    }
}
