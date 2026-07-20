using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBarController1 : MonoBehaviour
{
    public Slider levelSlider;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public float targetValue; // Valor objetivo para la animación
    public TMP_Text levelText; // Referencia al componente de texto TMP para mostrar el valor

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private bool animationPlayed = false;

    void Start()
    {
        animator1 = object1.GetComponent<Animator>();
        animator2 = object2.GetComponent<Animator>();
        animator3 = object3.GetComponent<Animator>();
        levelSlider.onValueChanged.AddListener(UpdateLevelText); // Agregar listener para actualizar el texto
        UpdateLevelText(levelSlider.value); // Actualizar el texto inicialmente
    }

    void Update()
    {
        // Verificar si se alcanza exactamente el valor objetivo
        if (!animationPlayed && Mathf.Approximately(levelSlider.value, targetValue))
        {
            // Detener la rotación y reproducir la animación de separación
            animator1.SetTrigger("Separate");
            animator2.SetTrigger("Separate");
            animator3.SetTrigger("Separate");
            animationPlayed = true;
            levelSlider.interactable = false; // Deshabilitar el slider
        }
    }

    // Método para actualizar el texto con el valor del slider
    void UpdateLevelText(float value)
    {
        levelText.text = "Energía en " + value.ToString("F2") + " ev";
    }
}