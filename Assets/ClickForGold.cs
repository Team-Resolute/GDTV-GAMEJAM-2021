using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ClickForGold : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldDisplay;
    [SerializeField] private Image marketBar;
    [SerializeField] private Image redLine;
    private float marketHostility = 0f;
    private float unresistedHostilityMultiplier = 10f;
    
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        ClickerBank.MarketValue -= (ClickerBank.MarketLosses * Time.deltaTime);
        ClickerBank.MarketValue = Mathf.Clamp01(ClickerBank.MarketValue);
    
        if (ClickerBank.Gold > 0)
        {
            if (ClickerBank.MarketValue == 0)
            {
                marketHostility = ClickerBank.MarketVolotility + (ClickerBank.MarketVolotilityRate * unresistedHostilityMultiplier * Time.deltaTime);
            }
            else
            {
                marketHostility = ClickerBank.MarketVolotility + (ClickerBank.MarketVolotilityRate * Time.deltaTime);
            }
        }
        else
        {
            marketHostility = 0f;
        }
        
        ClickerBank.MarketVolotility = Mathf.Clamp(marketHostility, 0f, 1 - ClickerBank.MarketValue);
        if (ClickerBank.MarketVolotility >= 0.999f)
        {
            ClickerBank.Gold = Mathf.Clamp(ClickerBank.Gold - 10, 0, int.MaxValue);
            ClickerBank.MarketVolotility = 0f;
            ClickerBank.MarketLosses += 0.01f;
            ClickerBank.MarketGains += 0.001f;
            UpdateUI();
        }
        
        UpdateUI();
    }

    public void OnClickMysteriousButton()
    {
        ClickerBank.MarketValue += ClickerBank.MarketGains;
        if (ClickerBank.MarketValue >= ClickerBank.MarketBuyout)
        {
            ClickerBank.Gold += 10;
            ClickerBank.MarketValue = 0f;
            ClickerBank.MarketLosses += 0.01f;
            ClickerBank.MarketGains += 0.001f;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        marketBar.fillAmount = ClickerBank.MarketValue;
        redLine.fillAmount = ClickerBank.MarketVolotility;
        goldDisplay.text = ClickerBank.Gold.ToString();
    }
}

public static class ClickerBank
{
    public static int Gold { get; set; } = 0;
    public static float MarketLosses { get; set; } = 0.01f;
    public static float MarketGains { get; set; } = 0.1f;
    public static float MarketValue { get; set; } = 0;
    public static float MarketBuyout { get; } = 1f;
    public static float MarketVolotility { get; set; } = 0f;
    public static float MarketVolotilityRate { get; } = 0.02f;
    
}


