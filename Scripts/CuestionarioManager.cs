using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CuestionarioManager : MonoBehaviour
{
    public GameObject sectionButtonPrefab; // Prefab del botón de sección
    public Transform content; // Content del Scroll View donde se mostrarán las secciones
    public GameObject PanelPopup; // El popup de preguntas
    public GameObject FondoPopup; // El fondo difumiado para el popup de preguntas
    public TMP_Text questionText; // El texto para mostrar la pregunta en el popup
    public TMP_Text sectionHeaderText; // Cabecera con el nombre de la sección
    public List<Button> starButtons; // Lista de botones que representan las estrellas de la escala
    public Button nextButton; // Botón para avanzar a la siguiente pregunta
    public Button previousButton; // Botón para retroceder a la pregunta anterior
    public Button finishButton; // Botón para finalizar el cuestionario
    public Button nextSectionButton; // Botón para pasar a la siguiente sección
    public Sprite estrellaLlena; // Imagen de estrella llena
    public Sprite estrellaVacia; // Imagen de estrella vacía

    public Color incompleteColor; // Color opaco para secciones incompletas
    public Color completeColor; // Color normal para secciones completas


    public Color titleSectionColor1; // Primer color
    public Color titleSectionColor2; // Segundo color
    public GameObject Panel_TitleSection; // Panel que cambiará de color

    private bool isAlternateColor = false;

    
    // Diccionario de secciones y sus preguntas
    private Dictionary<string, List<string>> secciones = new Dictionary<string, List<string>>()
    {
        { "Aspectos técnicos", new List<string> {
            "¿Los elementos de la aplicación se observan adecuadamente?",
            "¿La aplicación reacciona con una velocidad adecuada?",
            "¿El uso de la aplicación es intuitiva?",
            "¿El tamaño de la letra es adecuada?",
            "¿El formato de la pantalla es adecuado?"
        }},
        { "Innovación en el aula", new List<string> {
            "El uso de la aplicación es interesante",
            "La aplicación le ayudó a interactuar mejor con el docente",
            "Existió mayor atención con la implementación de esta tecnología en la clase",
            "Comprendió los contenidos abordados y transmitidos por el docente",
            "Considera usted que la forma en la que se desarrolló la clase es realmente innovadora"
        }},
        { "Metodología de enseñanza", new List<string> {
            "¿Adquirió conocimientos con el uso de la aplicación?",
            "Trabajar con la aplicación ¿le permitió aprender otros conceptos?",
            "Me sentí realmente activo.",
            "Trabajar con la aplicación ¿le permitió reforzar conceptos?"
        }},
        { "Actitudes", new List<string> {
            "¿Cuánto tiempo le dedica a esta asignatura además de las actividades planificadas?",
            "Considera usted que se podría masificar este tipo de iniciativas en otras de sus asignaturas.",
            "Calificaría como positiva esta experiencia con la aplicación",
            "¿Esta experiencia incrementó su interés por la materia?"
        }}
    };

    private Dictionary<string, List<int>> respuestas = new Dictionary<string, List<int>>(); // Respuestas por sección
    private string currentSection; // Sección actual seleccionada
    private int currentQuestionIndex = 0; // Índice de la pregunta actual dentro de la sección

    void Start()
    {
        PopulateSections(); // Mostrar botones de secciones
        UpdateAllSectionButtonColors(); // Revisar el estado de las secciones al inicio
        finishButton.gameObject.SetActive(false); // Ocultar botón de finalizar al inicio
        
    }

    void PopulateSections()
    {
        foreach (var section in secciones)
        {
            GameObject button = Instantiate(sectionButtonPrefab, content);
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            Button buttonComponent = button.GetComponent<Button>();
            Image buttonImage = button.GetComponent<Image>();

            if (buttonText != null)
            {
                buttonText.text = section.Key;
            }

            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => OpenSection(section.Key));
            }

            // Verificar y actualizar el color inicial del botón al crearlo
            UpdateSectionButtonColor(button, section.Key);
        }
    }
    void UpdateSectionButtonColor(GameObject button, string section)
    {
        if (button == null)
        {
            Debug.LogError($"El botón para la sección {section} es nulo.");
            return;
        }

        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError($"El botón {section} no tiene un componente Image asignado.");
            return;
        }

        // Verificar si la sección está completa
        bool isSectionComplete = respuestas.ContainsKey(section) && !respuestas[section].Contains(0);

        // Asignar color basado en el estado de la sección
        buttonImage.color = isSectionComplete ? completeColor : incompleteColor;

    }
    void OpenSection(string section)
    {
        currentSection = section;
        currentQuestionIndex = 0;

        // Inicializar respuestas si la sección no ha sido contestada antes
        if (!respuestas.ContainsKey(section))
        {
            respuestas[section] = new List<int>(new int[secciones[section].Count]);
        }

        sectionHeaderText.text = section; // Mostrar el nombre de la sección
        UpdateTitleSectionPanelColor();
        ShowQuestion();
        PanelPopup.SetActive(true);
        FondoPopup.SetActive(true);
    }

    void UpdateTitleSectionPanelColor()
    {
        if (Panel_TitleSection != null)
        {
            // Alternar entre los dos colores
            Panel_TitleSection.GetComponent<Image>().color = isAlternateColor ? titleSectionColor1 : titleSectionColor2;

            // Cambiar el estado para la próxima alternancia
            isAlternateColor = !isAlternateColor;
        }
        else
        {
            Debug.LogError("Panel_TitleSection no está asignado.");
        }
    }

    void ShowQuestion()
    {
        string preguntaActual = secciones[currentSection][currentQuestionIndex];
        questionText.text = preguntaActual;

        int respuestaActual = respuestas[currentSection][currentQuestionIndex];

        // Actualizar las estrellas según la respuesta guardada
        for (int i = 0; i < starButtons.Count; i++)
        {
            // Cambia el sprite de la estrella según la calificación guardada
            starButtons[i].GetComponent<Image>().sprite = (i < respuestaActual) ? estrellaLlena : estrellaVacia;
        }

        // Actualizar visibilidad de botones de navegación
        previousButton.gameObject.SetActive(currentQuestionIndex > 0);
        nextButton.gameObject.SetActive(currentQuestionIndex < secciones[currentSection].Count - 1);
        nextSectionButton.gameObject.SetActive(currentQuestionIndex == secciones[currentSection].Count - 1); // Mostrar flecha en la última pregunta
    }

    void UpdateAllSectionButtonColors()
    {
        foreach (Transform child in content)
        {
            TMP_Text buttonText = child.GetComponentInChildren<TMP_Text>();
            if (buttonText == null) continue;

            string sectionName = buttonText.text;
            GameObject button = child.gameObject;

            // Actualizar el color del botón según el estado de la sección
            UpdateSectionButtonColor(button, sectionName);
        }
    }


    public void SelectRating(int rating)
    {
        // Guardar la calificación seleccionada para la pregunta actual
        respuestas[currentSection][currentQuestionIndex] = rating;

        // Actualizar los sprites de las estrellas según la calificación seleccionada
        for (int i = 0; i < starButtons.Count; i++)
        {
            starButtons[i].GetComponent<Image>().sprite = (i < rating) ? estrellaLlena : estrellaVacia;
        }

        // Revisar el estado de los botones de sección
        UpdateAllSectionButtonColors();

        CheckAllSectionsCompleted(); // Verificar si todas las secciones están completas
    }

    public void NextQuestion()
    {
        if (currentQuestionIndex < secciones[currentSection].Count - 1)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
        else
        {
            PanelPopup.SetActive(false); // Cerrar el popup si es la última pregunta
            FondoPopup.SetActive(false);
        }
    }

    public void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            ShowQuestion();
        }
    }

    public void NextSection()
    {
        List<string> keys = new List<string>(secciones.Keys);
        int currentIndex = keys.IndexOf(currentSection);

        if (currentIndex < keys.Count - 1)
        {
            OpenSection(keys[currentIndex + 1]);
        }
        else
        {
            PanelPopup.SetActive(false);
            FondoPopup.SetActive(false);
        }
    }

    public void CheckAllSectionsCompleted()
    {
        bool allSectionsCompleted = true;

        foreach (var section in secciones.Keys)
        {
            if (!respuestas.ContainsKey(section) || respuestas[section].Contains(0))
            {
                allSectionsCompleted = false;
                break;
            }
        }

        finishButton.gameObject.SetActive(allSectionsCompleted); // Mostrar el botón si todo está completo
    }

    public void FinalizeQuiz()
    {
        FirebaseManager.Instance.SaveResponses(respuestas, () => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PantallaInicial");
        });
    }

}
