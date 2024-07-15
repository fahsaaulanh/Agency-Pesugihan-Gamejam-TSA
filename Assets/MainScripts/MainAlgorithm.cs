using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAlgorithm : MonoBehaviour
{
    public JurigData[] jurigs = new JurigData[4]; // Array untuk menyimpan 4 karyawan
    public float placeCost = 200f; // Biaya tempat awal
    public float uang = 1000f;     // Uang awal

    public InGameUI myInGameUI;

    public float WaitDuration, WaitToHideIO_Text;

    private void Start()
    {
        RemoveDuplicateJurigData();

        Debug.Log("Main Algorithm Dimulai");
        StartCoroutine(CalculateVariables());
    }

    private IEnumerator CalculateVariables()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitDuration); // Tunggu selama 10 detik

            // Logika untuk menghitung variabel
            float totalIncome = CalculateTotalIncome();
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

    private float CalculateTotalIncome()
    {
        float totalIncome = 0f;
        foreach (JurigData jurig in jurigs)
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
                totalSalary += jurig.salary;
            }
        }
        return totalSalary;
    }

    private void RemoveDuplicateJurigData()
    {
        HashSet<JurigData> uniqueJurigData = new HashSet<JurigData>();
        for (int i = 0; i < jurigs.Length; i++)
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
