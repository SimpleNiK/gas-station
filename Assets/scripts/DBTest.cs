using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Data;
using UnityEngine;

public class DBTest : MonoBehaviour
{
    public GameObject prefabCar;
    public GameObject prefabTRK;
    public GameObject prefabFT;
    public GameObject prefabFtype;
    private void Start()
    {
        DataTable fTankTable = DBManager.GetTable("SELECT ftank_id, ftank_name,ftank_volume,ftank_ftype_id, ft.ftype_name FROM FuelTank left join Ftype as ft on ftank_ftype_id=ft.Ftype_id;");
        DataTable CarTable = DBManager.GetTable("SELECT car_id, car_name, car_volume, car_ftype_id, ft.Ftype_name FROM Car left join Ftype as ft on car_ftype_id=ft.Ftype_id;");
        DataTable TRKTable = DBManager.GetTable("SELECT * FROM TRK;");
        DataTable ftypeTable = DBManager.GetTable("SELECT * FROM Ftype;");
        for (int i=0;i<CarTable.Rows.Count;i++){
        var copy = Instantiate(prefabCar, GameObject.Find("CarContent").transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = CarTable.Rows[i][1].ToString();
            copy.GetComponentsInChildren<TextMeshProUGUI>()[3].text = CarTable.Rows[i][2].ToString() + " Л";
            copy.GetComponentsInChildren<TextMeshProUGUI>()[4].text = CarTable.Rows[i][4].ToString();
            //setLinks(copy);
        }
        for (int i=0;i<fTankTable.Rows.Count;i++){
        var copy = Instantiate(prefabFT, GameObject.Find("fTContent").transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = fTankTable.Rows[i][1].ToString();
            copy.GetComponentsInChildren<TextMeshProUGUI>()[3].text = fTankTable.Rows[i][2].ToString() + " Л";
            copy.GetComponentsInChildren<TextMeshProUGUI>()[4].text = fTankTable.Rows[i][4].ToString();
            //setLinks(copy);
        }
        for (int i=0;i<TRKTable.Rows.Count;i++){
       var copy = Instantiate(prefabTRK, GameObject.Find("fDContent").transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = TRKTable.Rows[i][1].ToString();
            copy.GetComponentsInChildren<TextMeshProUGUI>()[2].text = TRKTable.Rows[i][2].ToString() + " Л/С";
            //setLinks(copy);
        }
        for (int i=0;i<ftypeTable.Rows.Count;i++){
        var copy = Instantiate(prefabFtype, GameObject.Find("fuelContent").transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = ftypeTable.Rows[i][1].ToString();
            copy.GetComponentsInChildren<TextMeshProUGUI>()[2].text = ftypeTable.Rows[i][2].ToString() + " руб.";
            //setLinks(copy);
        }
        
    }
}
