//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ObjectSelection : MonoBehaviour
//{
//    public GameObject selectionMenu; // Ссылка на меню выбора объекта
//    public Text menuTitle; // Ссылка на текст заголовка меню
//    public Button menuOptionPrefab; // Ссылка на шаблон опций меню
//    public Transform menuOptionsPanel; // Ссылка на панель опций меню

//    private RaycastHit[] hits;
//    [SerializeField] private List<GameObject> selectableObjects;
//    [SerializeField] private GameObject selectedObject;
//    private bool isMenuOpen;

//    public int selectedIndex;

//    void Start()
//    {
//        // Ищем все объекты с тегом "Selectable"
//        selectableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));

//        // Создаем кнопки для каждого выбираемого объекта
//        foreach (GameObject obj in selectableObjects)
//        {
//            //SelectObject()


//            Button button = Instantiate(menuOptionPrefab, menuOptionsPanel);
//            button.GetComponentInChildren<Text>().text = obj.name;
//            button.onClick.AddListener(delegate { SelectObject(obj); });
//        }

//        // Скрываем меню выбора объекта
//        selectionMenu.SetActive(false);
//        isMenuOpen = false;
//    }

//    void Update()
//    {
//        // Проверяем, было ли нажатие на кнопку меню выбора объекта
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            if (!isMenuOpen)
//            {
//                // Отображаем меню выбора объекта
//                selectionMenu.SetActive(true);
//                isMenuOpen = true;
//            }
//            else
//            {
//                // Скрываем меню выбора объекта
//                selectionMenu.SetActive(false);
//                isMenuOpen = false;
//            }
//        }

//        // Проверяем, было ли нажатие левой кнопки мыши
//        if (Input.GetMouseButtonDown(0))
//        {
//            // Проверяем, попал ли луч на какой-либо объект
//            hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
//            foreach (RaycastHit hit in hits)
//            {
//                if (selectableObjects.Contains(hit.transform.gameObject))
//                {
//                    SelectObject(hit.transform.gameObject);
//                    break;
//                }
//            }
//        }

//        // Обрабатываем прокрутку колесика мыши
//        if (Input.GetKeyDown(KeyCode.UpArrow)) //(Input.GetAxis("Mouse ScrollWheel") > 0f)
//        {
//            // Получаем индекс выбранной кнопки меню
//            selectedIndex = selectableObjects.IndexOf(selectedObject);

//            // Если выбрана последняя кнопка, выбираем первую
//            if (selectedIndex >= selectableObjects.Count - 1)
//            {
//                selectedIndex = 0;
//            }
//            else
//            {
//                // Выбираем следующую кнопку
//                selectedIndex++;
//            }

//            // Выбираем объект, связанный с выбранной кнопкой
//            SelectObject(selectableObjects[selectedIndex]);
//        }
//        else if (Input.GetKeyDown(KeyCode.DownArrow)) //(Input.GetAxis("Mouse ScrollWheel") < 0f)
//        {
//            // Получаем индекс выбранной кнопки меню
//            selectedIndex = selectableObjects.IndexOf(selectedObject);

//            // Если выбрана первая кнопка, выбираем последнюю
//            if (selectedIndex <= 0)
//            {
//                selectedIndex = selectableObjects.Count - 1;
//            }
//            else
//            {
//                // Выбираем предыдущую кнопку
//                selectedIndex--;
//            }

//            // Выбираем объект, связанный с выбранной кнопкой
//            SelectObject(selectableObjects[selectedIndex]);
//        }

//        // Проверяем, была ли нажата кнопка F
//        if (Input.GetKeyDown(KeyCode.F) && selectedObject != null)
//        {
//            // Нажимаем выбранную кнопку меню
//            selectedObject.GetComponent<Button>().onClick.Invoke();
//        }
//    }

//    void SelectObject(GameObject obj)
//    {
//        // Сбрасываем выделение всех объектов
//        foreach (GameObject selectableObj in selectableObjects)
//        {
//            // Меняем цвет на обычный
//            selectableObj.transform.GetComponent<Image>().color = Color.white;
//        }

//        // Выбираем новый объект
//        selectedObject = obj;

//        // Выделяем выбранный объект
//        selectedObject.GetComponent<Image>().color = Color.green;

//        // Обновляем заголовок меню
//        menuTitle.text = selectedObject.name;

//        // Скрываем меню выбора объекта
//        //selectionMenu.SetActive(false);
//        //isMenuOpen = false;
//    }

//}

////public class ObjectSelectionBase : MonoBehaviour
////{
////    public GameObject selectionMenu; // Ссылка на меню выбора объекта
////    public Text menuTitle; // Ссылка на текст заголовка меню
////    public Button menuOptionPrefab; // Ссылка на шаблон опций меню
////    public Transform menuOptionsPanel; // Ссылка на панель опций меню

////    private RaycastHit[] hits;
////    private List<GameObject> selectableObjects;
////    private GameObject selectedObject;
////    private bool isMenuOpen;

////    void Start()
////    {
////        Ищем все объекты с тегом "Selectable"
////        selectableObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Selectable"));

////        Создаем кнопки для каждого выбираемого объекта
////        foreach (GameObject obj in selectableObjects)
////        {
////            Button button = Instantiate(menuOptionPrefab, menuOptionsPanel);
////            button.GetComponentInChildren<Text>().text = obj.name;
////            button.onClick.AddListener(delegate { SelectObject(obj); });
////        }

////        Скрываем меню выбора объекта
////        selectionMenu.SetActive(false);
////        isMenuOpen = false;
////    }

////    void Update()
////    {
////        Проверяем, было ли нажатие на кнопку меню выбора объекта
////        if (Input.GetKeyDown(KeyCode.H))
////        {
////            if (!isMenuOpen)
////            {
////                Отображаем меню выбора объекта
////                selectionMenu.SetActive(true);
////                isMenuOpen = true;
////            }
////            else
////            {
////                Скрываем меню выбора объекта
////                selectionMenu.SetActive(false);
////                isMenuOpen = false;
////            }
////        }

////        Проверяем, было ли нажатие левой кнопки мыши
////        if (Input.GetMouseButtonDown(0))
////        {
////            Проверяем, попал ли луч на какой - либо объект
////           hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
////            foreach (RaycastHit hit in hits)
////            {
////                if (selectableObjects.Contains(hit.transform.gameObject))
////                {
////                    SelectObject(hit.transform.gameObject);
////                    break;
////                }
////            }
////        }
////    }

////    Функция выбора объекта
////    public void SelectObject(GameObject obj)
////    {
////        Выбираем объект
////        selectedObject = obj;

////        Закрываем меню выбора объекта
////        selectionMenu.SetActive(false);
////        isMenuOpen = false;

////        Обновляем текст заголовка меню
////        menuTitle.text = "Selected: " + selectedObject.name;

////        Выполняем действия с выбранным объектом...
////    }
////}
