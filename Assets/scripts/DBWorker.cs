using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Data;
using UnityEngine;

public class DBWorker : MonoBehaviour
{
    public Button addButton;
    public Button changeButton;
    public Button deleteButton;
    public GameObject content;
    public GameObject prefab;
    public GameObject AddFT;
    public GameObject AddFD;
    public GameObject AddCar;
    public GameObject AddFuel;
    public TMP_Dropdown fuelType;
    public TMP_InputField FDName;
    public TMP_InputField FDSpeed;
    public TMP_InputField FTName;
    public TMP_InputField FTVolume;
    public TMP_InputField carName;
    public TMP_InputField carVolume;
    public TMP_Dropdown carFuelType;
    public TMP_InputField fuelName;
    public TMP_InputField fuelPrice;
    public List<string> fuelTypeList;//= new List<string> { "АИ-92", "АИ-95" };

    string stringtochange;
    bool toChange { set; get; }
    private void Awake()
    {
        addButton = GameObject.Find("AddButton").GetComponent<Button>();
        changeButton = GameObject.Find("ChangeButton").GetComponent<Button>();
        deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();
        if(fuelType != null)
        {
            setDropDown();
        }
    }
    private void Start()
    {
        // Получаем отсортированную таблицу лидеров
        DataTable ftypeTable = DBManager.GetTable("SELECT * FROM Ftype;");
        

        fuelTypeList=new List<string>();
        for (int i=0;i<ftypeTable.Rows.Count;i++)
            fuelTypeList.Add(ftypeTable.Rows[i][1].ToString());
            if(fuelType != null)
        {
            setDropDown();
        }
        if (carFuelType!=null)
            setDropDown();

        



        // Получаем id лучшего игрока
        //int idBestPlayer = int.Parse(scoreboard.Rows[0][1].ToString());
        // Получаем ник лучшего игрока
        //string nickname = MyDataBase.ExecuteQueryWithAnswer($"SELECT nickname FROM Player WHERE id_player = {idBestPlayer};");
        
        //Debug.Log(scoreboard.Rows[0][2].ToString());
    }

    public void setToChange(bool value) {
        toChange = value;
    }
    public void changeAddButton(string item) {
        Debug.Log(item);
        addButton.GetComponentInChildren<TextMeshProUGUI>().text = "Добавить " + item; 
    }
    public void deleteComponent()
    {
        Destroy(prefab);
        changeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Изменить";
        deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = "Удалить";
        addButton.GetComponentInChildren<TextMeshProUGUI>().text = "Добавить";
    }
    public void changeChangeButton(TextMeshProUGUI item)
    {
        changeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Изменить " + item.text;        
        deleteButton.GetComponentInChildren<TextMeshProUGUI>().text = "Удалить " + item.text;        
    }
    public void setDropDown() {
        fuelType.ClearOptions();
        fuelType.AddOptions(fuelTypeList);
        carFuelType.ClearOptions();
        carFuelType.AddOptions(fuelTypeList);
    }

    public void openAddPanel()
    {
        string type = addButton.GetComponentInChildren<TextMeshProUGUI>().text.Split(" ")[1];
        switch (type){
            case "ТБ":
                AddFT.SetActive(true);
                break;
            case "ТРК":
                AddFD.SetActive(true);
                break;
            case "тип":
                AddFuel.SetActive(true);
                break;
            case "автомобиль":
                AddCar.SetActive(true);
                break;
        }
    }

    public void openChangePanel()
    {
        string type = addButton.GetComponentInChildren<TextMeshProUGUI>().text.Split(" ")[1];
        switch (type)
        {
            case "ТБ":
                AddFT.SetActive(true);
                FTName.text = prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text;
                FTVolume.text = prefab.GetComponentsInChildren<TextMeshProUGUI>()[3].text.Split(" ")[0];
                fuelType.value = fuelTypeList.IndexOf((prefab.GetComponentsInChildren<TextMeshProUGUI>()[4].text));
                break;          
            case "ТРК":
                AddFD.SetActive(true);
                FDName.text = prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text;
                FDSpeed.text = prefab.GetComponentsInChildren<TextMeshProUGUI>()[2].text.Split(" ")[0];
                break;
            case "тип":
                AddFuel.SetActive(true);
                fuelName.text = prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text;
                stringtochange = fuelName.text;
                fuelPrice.text = prefab.GetComponentsInChildren<TextMeshProUGUI>()[2].text.Split(" ")[0];
                break;
            case "автомобиль":
                AddCar.SetActive(true);
                carName.text = prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text;
                carVolume.text = prefab.GetComponentsInChildren<TextMeshProUGUI>()[3].text.Split(" ")[0];
                carFuelType.value = fuelTypeList.IndexOf((prefab.GetComponentsInChildren<TextMeshProUGUI>()[4].text));
                break;
        }
    }
    public void addTRK()
    {
        if (toChange) {
            prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = FTName.text;
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[3].text = FTVolume.text + " Л";
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[4].text = fuelTypeList[fuelType.value];
        } else {
            var copy = Instantiate(prefab, content.transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = FTName.text;
            copy.GetComponentsInChildren<TextMeshProUGUI>()[3].text = FTVolume.text + " Л";
            copy.GetComponentsInChildren<TextMeshProUGUI>()[4].text = fuelTypeList[fuelType.value];
            setLinks(copy);
        }
        
        FTName.text = "";
        FTVolume.text = "";
    }
    public void addFD()
    {
        if (toChange) {
            prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = FDName.text;
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[2].text = FDSpeed.text + " Л/С";
        } else {
            var copy = Instantiate(prefab, content.transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = FDName.text;
            copy.GetComponentsInChildren<TextMeshProUGUI>()[2].text = FDSpeed.text + " Л/С";
            setLinks(copy);
        }
        
        FDName.text = "";
        FDSpeed.text = "";
    }
    public void addFuelType()
    {
        if (toChange) {
            fuelTypeList[fuelTypeList.IndexOf(stringtochange)] = fuelName.text;
            prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = fuelName.text;
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[2].text = fuelPrice.text + " руб.";
        } else {
            var copy = Instantiate(prefab, content.transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = fuelName.text;
            copy.GetComponentsInChildren<TextMeshProUGUI>()[2].text = fuelPrice.text + " руб.";
            fuelTypeList.Add(fuelName.text);
            setLinks(copy);
        }
        
        
        setDropDown();
        fuelName.text = "";
        fuelPrice.text = "";
    }
    public void addCar()
    {
        if (toChange) {
            prefab.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = carName.text;
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[3].text = carVolume.text + " Л";
            prefab.GetComponentsInChildren<TextMeshProUGUI>()[4].text = fuelTypeList[carFuelType.value];
        } else {
            var copy = Instantiate(prefab, content.transform);
            copy.GetComponentsInChildren<LayoutElement>()[0].GetComponentInChildren<TextMeshProUGUI>().text = carName.text;
            copy.GetComponentsInChildren<TextMeshProUGUI>()[3].text = carVolume.text + " Л";
            copy.GetComponentsInChildren<TextMeshProUGUI>()[4].text = fuelTypeList[carFuelType.value];
            setLinks(copy);
        }
        
    }
    public void setParent(GameObject child)
    {
        content = child.transform.parent.gameObject;
        GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().content = content;

    }

    public void setPrefab(GameObject prefab) {
        GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().prefab = prefab;
    }
    public void setLinks(GameObject prefab) {
        prefab.GetComponentInChildren<DBWorker>().addButton = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().addButton;
        prefab.GetComponentInChildren<DBWorker>().changeButton = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().changeButton;
        prefab.GetComponentInChildren<DBWorker>().deleteButton = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().deleteButton;
        prefab.GetComponentInChildren<DBWorker>().content = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().content;
        prefab.GetComponentInChildren<DBWorker>().prefab = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().prefab;
        prefab.GetComponentInChildren<DBWorker>().fuelType = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().fuelType;
        prefab.GetComponentInChildren<DBWorker>().carFuelType = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().carFuelType;
        prefab.GetComponentInChildren<DBWorker>().FTName = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().FTName;
        prefab.GetComponentInChildren<DBWorker>().FTVolume = GameObject.Find("DBWorkerMain").GetComponent<DBWorker>().FTVolume;
    }

    public void setContent(GameObject content) {
        this.content = content;
    }

}
