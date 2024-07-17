using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class InGameUI : MonoBehaviour
{

    public GameObject Money_Text, Income_Text, Outcome_Text;
    [Space]

    public MainAlgorithm myMainAlgorithm;

    [Header("These are for JobList")]
    [Space]
    public GameObject itemPrefab; // Prefab untuk item list
    public Transform container; // Kontainer untuk item list

    [Header("These are for Cheking customerCount")]
    [Space]

    public GameObject jobDetailPanel;
    public GameObject emptyText;

    [Header("These are for JobUI")]
    [Space]

    public TextMeshProUGUI jobName_Text;
    public TextMeshProUGUI jobPlace_Text;
    public TextMeshProUGUI targetIncome_Text;
    public TextMeshProUGUI durasi_Text;
    public TextMeshProUGUI pembagian_Text;


    [Header("These are for Jurig selection")]
    [Space]

    public int selectedJurigIndex;
    public GameObject[] SelectedJurig_Button;

    public GameObject jurigDetailPanel, noAvailJurig_Text;
    
    [Space]
    public int selectedJurig = 1;
    public TextMeshProUGUI jurigName_text, jurigRole_text, jurigGaji_text, jurigProfit_text, countJurig_text;
    public Image jurigImage;

    [Space]
    public CustomerData selectedJob;

    private void Start() {
        changeDataOfJurig();
        checkIfJobEmpty();
    }

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

    public void increaseCustomerCount()
    {
        GameObject newCustomer = Instantiate(itemPrefab, container);
        myMainAlgorithm.jobs.Add(newCustomer.GetComponent<CustomerData>());
    }

    private IEnumerator HideIO_Text()
    {
        yield return new WaitForSeconds(myMainAlgorithm.waitToHideIO_Text);

        Income_Text.SetActive(false);
        Outcome_Text.SetActive(false);
        Money_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
    }

    public void changeDataInJobDetail(string jobName, string jobPlace, float targetIncome, string durasi, string pembagian)
    {
        jobName_Text.text = jobName;
        jobPlace_Text.text = jobPlace;
        targetIncome_Text.text = FormatUang(targetIncome);
        durasi_Text.text = durasi + " hari";
        pembagian_Text.text = pembagian + " %";
    }

    public void changeJurig(bool isNext)
    {
        if(isNext){
            if(selectedJurig<myMainAlgorithm.jurigs.Count)selectedJurig++;
        }else{
            if(selectedJurig > 1) selectedJurig--;
        }

        changeDataOfJurig();
    }

    public void changeDataOfJurig()
    {
        if(myMainAlgorithm.jurigs.Count > 0)
        {
            jurigDetailPanel.SetActive(true);
            noAvailJurig_Text.SetActive(false);
            jurigName_text.text = myMainAlgorithm.jurigs[selectedJurig - 1].jurigName;
            jurigRole_text.text = myMainAlgorithm.jurigs[selectedJurig - 1].jurigRole.ToString();
            jurigGaji_text.text = myMainAlgorithm.jurigs[selectedJurig - 1].salary.ToString();
            jurigProfit_text.text = myMainAlgorithm.jurigs[selectedJurig - 1].income.ToString();
            jurigImage.sprite = myMainAlgorithm.jurigs[selectedJurig - 1].jurigImage;
        }else{
            jurigDetailPanel.SetActive(false);
            noAvailJurig_Text.SetActive(true);
        }

        countJurig_text.text = selectedJurig.ToString() + "/" + myMainAlgorithm.jurigs.Count.ToString();
    }

    public void checkIfJobEmpty()
    {
        if(myMainAlgorithm.customerCount == 0)
        {
            jobDetailPanel.SetActive(false);
            emptyText.SetActive(true);
        }else{
            jobDetailPanel.SetActive(true);
            emptyText.SetActive(false);
        }
    }

    public void pickJurig(int button)
    {
        selectedJurigIndex = button;
    }

    public void ApplyJurigForJob()
    {
        selectedJob.workingJurigs[selectedJurigIndex] = myMainAlgorithm.jurigs[selectedJurig - 1];

        myMainAlgorithm.workingJurigs.Add(myMainAlgorithm.jurigs[selectedJurig - 1]);
        myMainAlgorithm.jurigs.RemoveAt(selectedJurig - 1);

        SelectedJurig_Button[selectedJurigIndex].GetComponent<Image>().sprite = selectedJob.workingJurigs[selectedJurigIndex].jurigImage;
    }

    public void ApplyJob()
    {
        selectedJob.isInProgress = true;
    }
}