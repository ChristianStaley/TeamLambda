using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    private Slider BossHP;
    public static int currenthp = 100;

    private void Awake()
    {
        BossHP = GetComponent<Slider>();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        BossHP.value = currenthp;
    }

    public void ChangeHP(int dEn)
    {
        currenthp += dEn;
    }
}
