using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{

    public GameObject Money_Text, Income_Text, Outcome_Text;

    public MainAlgorithm myMainAlgorithm;
    void Update(){
        Money_Text.GetComponent<TextMeshProUGUI>().text = "Money : Rp " + FormatUang(myMainAlgorithm.uang);
    }

    private string FormatUang(float uang)
    {
        if (uang >= 1000)
        {
            return (uang / 1000f).ToString("0.##") + "K";
        }
        return uang.ToString();
    }

    public void UpdateText(float totalIncome, float totalOutcome, float previousUang, float currentUang)
    {
        Income_Text.SetActive(true);
        Outcome_Text.SetActive(true);

        Income_Text.GetComponent<TextMeshProUGUI>().text = "+" + FormatUang(totalIncome);
        Outcome_Text.GetComponent<TextMeshProUGUI>().text = "-" + FormatUang(totalOutcome);

        if(currentUang > previousUang){
            Money_Text.GetComponent<TextMeshProUGUI>().color = Color.green;
        }else if(currentUang < previousUang){
            Money_Text.GetComponent<TextMeshProUGUI>().color = Color.red;
        }else{
            Money_Text.GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
        
        StartCoroutine(HideIO_Text());
    }

    private IEnumerator HideIO_Text()
    {
        yield return new WaitForSeconds(myMainAlgorithm.WaitToHideIO_Text);

        Income_Text.SetActive(false);
        Outcome_Text.SetActive(false);
        Money_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
}
