using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
public class CustomerData : MonoBehaviour
{
    kumpulanData myKumpulanData = new kumpulanData();
    public string jobName;
    public string jobArea;
    public string locationName;
    public float incomeTarget;

    [Space]
    public float minTarget;
    public float maxTarget;
    [Space]

    public int maxTime;

    public float sharingPercentage;

    public int customerNumber;
    public List<JurigData> workingJurigs;
    public bool isInProgress;

    private void OnEnable() {
        
        MainAlgorithm.OnDayChanged += Progressing;
    }
    
    void Start()
    {
        
        // ===============================================================================
        // Part jobName
        jobName = myKumpulanData.namaPekerjaan[UnityEngine.Random.Range(0, myKumpulanData.namaPekerjaan.Length)];

        // ===============================================================================
        // Part jobArea
        jobArea = myKumpulanData.namaArea[UnityEngine.Random.Range(0, myKumpulanData.namaArea.Length)];

        // ===============================================================================
        // Part locationName
        locationName = myKumpulanData.namaLokasi[UnityEngine.Random.Range(0, myKumpulanData.namaLokasi.Length)];

        // ===============================================================================
        // Part IncomeTarget
         int step = 5000;// Hitung jumlah kelipatan dalam rentang

        int steps = (int)((maxTarget - minTarget) / step) + 1;

        // Ambil indeks acak menggunakan UnityEngine.Random.Range
        int randomIndex = UnityEngine.Random.Range(0, steps);

        // Hitung nilai acak berdasarkan indeks dan langkah
        incomeTarget = minTarget + randomIndex * step;

        // ===============================================================================
        // Part IncomeTarget
        maxTime = UnityEngine.Random.Range(1, 7);

        // ===============================================================================
        // Part SharingPercentage
        sharingPercentage = UnityEngine.Random.Range(10, 90);
    }

    public void printAllData()
    {
        Debug.Log(jobName + ", " + jobArea.ToString() + ", " + locationName.ToString() + ", " + incomeTarget.ToString() + ", " + maxTime.ToString() + ", " + sharingPercentage.ToString() + "%");
        InGameUI myInGameUI = GameObject.FindGameObjectWithTag("Main").GetComponent<InGameUI>();

        myInGameUI.checkIfJobEmpty();
        myInGameUI.changeDataInJobDetail(jobName, jobArea, incomeTarget, maxTime.ToString(), sharingPercentage.ToString());
        
        myInGameUI.selectedJob = this;

        
    }

    void Progressing()
    {
        if(isInProgress)
        {            
            MainAlgorithm myMainAlgorithm = GameObject.FindGameObjectWithTag("Main").GetComponent<MainAlgorithm>();
            
            myMainAlgorithm.totalIncome += myMainAlgorithm.CalculateTotalIncome(workingJurigs) * sharingPercentage / 100;
        }
    }
}

class kumpulanData{
    public string[] namaArea = new string[]{"Kampung", "Perumahan"};
    public string[] namaPekerjaan = new string[]{"Menambah Uang", "Melariskan Usaha"};

    public string[] namaLokasi = new string[]
{
    "Kampung Sawah",
    "Dusun Baru",
    "Desa Sari",
    "Kampung Tengah",
    "Dusun Timur",
    "Desa Maju",
    "Kampung Utara",
    "Dusun Sejahtera",
    "Desa Harmoni",
    "Kampung Lestari",
    "Dusun Indah",
    "Desa Makmur",
    "Kampung Damai",
    "Dusun Bahagia",
    "Desa Sentosa",
    "Kampung Pertiwi",
    "Dusun Asri",
    "Desa Ceria",
    "Kampung Bersih",
    "Dusun Alam",
    "Desa Nyaman",
    "Kampung Hijau",
    "Dusun Hening",
    "Desa Bening",
    "Kampung Teduh",
    "Dusun Rukun",
    "Desa Aman",
    "Kampung Anggrek",
    "Dusun Sakura",
    "Desa Cemara"
};
}