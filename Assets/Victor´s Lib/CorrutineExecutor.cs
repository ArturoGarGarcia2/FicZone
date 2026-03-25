using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


    
    public class CorrutineExecutor : MonoBehaviour
    {

        public static CorrutineExecutor instance;

        public bool ExecuteCorrutine(IEnumerator corrutina)
        {

            bool a = true;
            
            StartCoroutine(corrutina);

            return a;

        }
    
    }



