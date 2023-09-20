using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeFire : MonoBehaviour
{
    [SerializeField] Transform FireWinParent;
    [SerializeField] Transform EmployeeParent;

    private void Start()
    {
        WinOff();
    }

    void WinOff()
    {
        FireWinParent.gameObject.SetActive(false);

        for (int i = 0; i < FireWinParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void ShowFireWin()
    {
        bool value = true;

        if (FireWinParent.gameObject.activeInHierarchy)
        {
            value = false;
        }
        else
        {
            value = true;
        }

        if (value == true)
        {
            FireWinParent.gameObject.SetActive(true);

            FindEmployeeData();
        }
        else
        {
            FireWinParent.gameObject.SetActive(false);

            for (int i = 0; i < FireWinParent.childCount; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);

                FireWinParent.GetChild(i).GetChild(5).
                   GetComponent<Button>().interactable = true;

                FireWinParent.GetChild(i).GetChild(5).GetChild(0).
                    GetComponent<Text>().text = "해고하기";
            }
        }
    }

    void FindEmployeeData()
    {
        for (int i = 0; i < EmployeeParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(true);

            for (int j = 0; j < 5; j++)
            {
                FireWinParent.GetChild(i).GetChild(j).
                    GetComponent<Text>().text = Stat(i, j);
            }
        }
    }

    string Stat(int Evalue, int Svalue)
    {
        string result = null;

        switch (Svalue)
        {
            case 0:
                result = "손재주 : " + EmployeeParent.GetChild(Evalue).
                    GetComponent<EmployeeStat>().Handy.ToString();
                break;
            case 1:
                result = "순발력 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Agility.ToString();
                break;
            case 2:
                result = "경력 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Career.ToString();
                break;
            case 3:
                result = "창의력 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Creativity.ToString();
                break;
            case 4:
                result = "주급 : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Pay.ToString();
                break;
            default:
                break;
        }

        return result;
    }

    public void FireButtonOn(int value)
    {
        if (EmployeeParent.childCount > 1)
        {
            EmployeeParent.GetComponent<PizzaQuality>().Employees.Remove
                (EmployeeParent.GetChild(value).gameObject);

            Destroy(EmployeeParent.GetChild(value).gameObject);

            FireWinParent.GetChild(value).GetChild(5).
                GetComponent<Button>().interactable = false;

            FireWinParent.GetChild(value).GetChild(5).GetChild(0).
                GetComponent<Text>().text = "해고완료";
        }
        else
        {
            Debug.Log("사장님 나빠요");
        }
    }
}