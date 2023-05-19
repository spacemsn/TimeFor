using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
{
    [Header("����������")]
    public GameObject GlobalSettings;
    private moveCharacter move;

    private List<Button> selectButtons;
    private Button oldButton;
    private Button currentButton;
    public Transform buttonParent;
    public int selectedIndex;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
    }
    
    public void Update()
    {

        selectButtons = buttonParent.GetComponentsInChildren<Button>().ToList();

        if (buttonParent.childCount > 0 && selectButtons[selectedIndex] != null)
        {
            SelectObject(selectButtons[selectedIndex]);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // �������� ������ ��������� ������ ����
            selectedIndex = selectButtons.IndexOf(currentButton);

            // ���� ������� ��������� ������, �������� ������
            if (selectedIndex >= selectButtons.Count - 1)
            {
                selectedIndex = 0;
            }
            else
            {
                // �������� ��������� ������
                selectedIndex++;
            }

            // �������� ������, ��������� � ��������� �������
            if (buttonParent.childCount > 0 && selectButtons[selectedIndex] != null)
            {
                SelectObject(selectButtons[selectedIndex]);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // �������� ������ ��������� ������ ����
            selectedIndex = selectButtons.IndexOf(currentButton);

            // ���� ������� ������ ������, �������� ���������
            if (selectedIndex <= 0)
            {
                selectedIndex = selectButtons.Count - 1;
            }
            else
            {
                // �������� ���������� ������
                selectedIndex--;
            }

            // �������� ������, ��������� � ��������� �������
            if (buttonParent.childCount > 0 && selectButtons[selectedIndex] != null)
            {
                SelectObject(selectButtons[selectedIndex]);
            }
        }
    }

    void SelectObject(Button obj)
    {
        // ���������� ��������� ���� ��������
        foreach (Button selectableObj in selectButtons)
        {
            if (currentButton != null)
            {
                // ������ ���� �� �������
                selectableObj.transform.GetComponent<Image>().color = currentButton.colors.normalColor;
            }
        }

        // �������� ����� ������
        if(currentButton != null) { oldButton = currentButton; oldButton.GetComponent<SelectObjectButton>().isSelect(); }
        currentButton = obj; currentButton.GetComponent<SelectObjectButton>().isSelect();

        // �������� ��������� ������
        //currentButton.GetComponent<Image>().color = Color.green;
        currentButton.GetComponent<Image>().color = currentButton.colors.highlightedColor;

    }
}
