using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interactionCharacter : MonoCache
{
    [Header("EntryPoint")]
    public EntryPoint entryPoint;
    public PlayerEntryPoint playerEntry;
    public UIEntryPoint uIEntry;

    [Header("Компоненты")]
    public GameObject GlobalSettings;
    private moveCharacter move;

    [SerializeField] private List<Button> selectButtons;
    [SerializeField] private Button oldButton;
    [SerializeField] private Button currentButton;
    public Transform buttonParent;
    public int selectedIndex;

    private void Start()
    {
        move = GetComponent<moveCharacter>();
    }

    public void GetUI(PlayerEntryPoint player, UIEntryPoint uI)
    {
        this.playerEntry = player;
        this.uIEntry = uI;

        buttonParent = uI.buttonParent;
    }

    public void Update()
    {
        selectButtons = buttonParent.GetComponentsInChildren<Button>().ToList();

        // Выбираем объект, связанный с выбранной кнопкой
        if (selectedIndex > selectButtons.Count - 1 && selectedIndex < selectButtons.Count - 1 && buttonParent.childCount > 0)
        {
            SelectObject(selectButtons[selectedIndex]);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Получаем индекс выбранной кнопки меню
            selectedIndex = selectButtons.IndexOf(currentButton);

            // Если выбрана последняя кнопка, выбираем первую
            if (selectedIndex >= selectButtons.Count - 1)
            {
                selectedIndex = 0;
            }
            else
            {
                // Выбираем следующую кнопку
                selectedIndex++;
            }

            // Выбираем объект, связанный с выбранной кнопкой
            if (buttonParent.childCount > 0 && selectButtons[selectedIndex] != null)
            {
                SelectObject(selectButtons[selectedIndex]);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // Получаем индекс выбранной кнопки меню
            selectedIndex = selectButtons.IndexOf(currentButton);

            // Если выбрана первая кнопка, выбираем последнюю
            if (selectedIndex <= 0)
            {
                selectedIndex = selectButtons.Count - 1;
            }
            else
            {
                // Выбираем предыдущую кнопку
                selectedIndex--;
            }

            // Выбираем объект, связанный с выбранной кнопкой
            if (buttonParent.childCount > 0 && selectButtons[selectedIndex] != null)
            {
                SelectObject(selectButtons[selectedIndex]);
            }
        }
    }

    void SelectObject(Button obj)
    {
        // Сбрасываем выделение всех объектов
        foreach (Button selectableObj in selectButtons)
        {
            if (currentButton != null)
            {
                // Меняем цвет на обычный
                selectableObj.transform.GetComponent<Image>().color = currentButton.colors.disabledColor;
            }
        }

        // Выбираем новый объект
        if(currentButton != null) { oldButton = currentButton; oldButton.GetComponent<SelectObjectButton>().isSelect(); }
        currentButton = obj; currentButton.GetComponent<SelectObjectButton>().isSelect();

        // Выделяем выбранный объект
        currentButton.GetComponent<Image>().color = currentButton.colors.normalColor;

    }
}
