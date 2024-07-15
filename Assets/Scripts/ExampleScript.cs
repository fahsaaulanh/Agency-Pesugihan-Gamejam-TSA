using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Setiap script harus pake namespace, diawali dengan namespace wajib yaitu AgencyPesugihan,
//kemudian kalau script ada didalam folder Mechanic misalnya, berarti namespace nya jadi AgencyPesugihan.Mechanic
//begitupun seterusnya jika didalam sebuah folder ada folder lagi wajib disesuaikan namespacenya
namespace AgencyPesugihan
{
    public class ExampleScript : MonoBehaviour
    {
        [SerializeField] private string contohVariablePrivate; //penulisan variabel private diawali dengan huruf kecil
        public string ContohVariablePrivate; //penulisan variabel public diawali dengan huruf besar 

        //ini penulisan private function
        private void contohPrivateFunc(string _ContohVariabelDidalamParameter)
        {
            string _contohLokalVariabel = _ContohVariabelDidalamParameter;
        }
    }
}
