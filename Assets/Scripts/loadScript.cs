using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadScript : MonoBehaviour
{
    public Transform masukanLoadingbar;
    [SerializeField]
    private float nilaiSekarang;
    [SerializeField]
    private float nilaiKecepatan;

    //update is called once per frame
    private void Update()
    {
        if (nilaiSekarang < 100)
        {
            nilaiSekarang += nilaiKecepatan * Time.deltaTime;
            Debug.Log ((int)nilaiSekarang);

        }else{
            Application.LoadLevel("home");
        }
        masukanLoadingbar.GetComponent<Image> ().fillAmount = nilaiSekarang / 100;
        } 
    }

