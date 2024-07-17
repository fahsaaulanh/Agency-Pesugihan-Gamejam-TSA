using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainAlgorithm : MonoBehaviour
{
    public static event Action OnDayChanged;
    public List<JurigData> jurigs;
    public List<JurigData> workingJurigs;
    public float placeCost = 200f; // Biaya tempat awal
    public float uang = 1000f;     // Uang awal
    public float totalIncome = 0;

    //These variable is related to InGameUI
    [Header("These are related to InGameUI")]
    [Space]
    public InGameUI myInGameUI;

    public float waitDuration, waitToHideIO_Text;

    [Space]
    public int customerCount;
    public List<CustomerData> jobs;
    

    private void Start()
    {
        RemoveDuplicateJurigData();

        Debug.Log("Main Algorithm Dimulai");
        StartCoroutine(DayCycle());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F))
        {
            customerCount ++;
            myInGameUI.increaseCustomerCount();

        }
    }

    private IEnumerator DayCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitDuration); // Tunggu selama 10 detik
            
            // Logika untuk menghitung variabel
            totalIncome = 0;

            DayChanged();
            
            float totalSalary = CalculateTotalSalary();
            float totalOutcome = totalSalary + placeCost;

            float previousUang = uang;

            // Update uang dengan total income, total salary, dan place cost

            uang += totalIncome;
            uang -= totalOutcome;

            Debug.Log($"Total Income: {totalIncome}, Total Salary: {totalSalary}, Place Cost: {placeCost}, Uang: {uang}");


            myInGameUI.UpdateText(totalIncome, totalOutcome, previousUang, uang);
        }
    }

    private void DayChanged()
    {
        OnDayChanged?.Invoke();
    }

    public float CalculateTotalIncome(List<JurigData> localJurigs)
    {
        float totalIncome = 0f;
        foreach (JurigData jurig in localJurigs)
        {
            if (jurig != null)
            {
                totalIncome += jurig.income;
            }
        }
        return totalIncome;
    }

    private float CalculateTotalSalary()
    {
        float totalSalary = 0f;
        foreach (JurigData jurig in jurigs)
        {
            if (jurig != null)
            {
                totalSalary += jurig.passiveSalary;
            }
        }
        return totalSalary;
    }

    private void RemoveDuplicateJurigData()
    {
        HashSet<JurigData> uniqueJurigData = new HashSet<JurigData>();
        for (int i = 0; i < jurigs.Count; i++)
        {
            JurigData jurig = jurigs[i];
            if (jurig != null)
            {
                if (!uniqueJurigData.Add(jurig))
                {
                    jurigs[i] = null; // Hapus duplikat
                    Debug.LogWarning($"Duplicate JurigData removed at index {i}");
                }
            }
        }
    }
}