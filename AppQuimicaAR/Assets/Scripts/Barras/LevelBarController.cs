using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBarController : MonoBehaviour
{
    public Slider levelSlider;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public float targetValue; // Valor objetivo para la animación
    public RotateObject linkedObject;
    public TMP_Text levelText; // Referencia al componente de texto TMP para mostrar el valor

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private bool animationPlayed = false;
    public AnimationController animationController;
    private float lastValue = float.MinValue;

    void Start()
    {
        if (linkedObject == null)
        {
            Debug.LogError("Linked RotateObject no está asignado.");
            return;
        }

        if (animationController == null)
        {
            Debug.LogError("AnimationController no está asignado.");
            return;
        }

        // Suscribirse al evento de cambio de rotación
        linkedObject.OnRotationChanged += UpdateLevelBar;

        animator1 = object1.GetComponent<Animator>();
        animator2 = object2.GetComponent<Animator>();
        animator3 = object3.GetComponent<Animator>();
        levelSlider.onValueChanged.AddListener(UpdateLevelText); // Agregar listener para actualizar el texto
        UpdateLevelText(levelSlider.value); // Actualizar el texto inicialmente
    }

    private void UpdateLevelBar(bool isChangedRotation)
    {
        // Ajustar el valor del slider según la rotación
        levelSlider.value = isChangedRotation ? -1.5f : 2.26f;

        // Ejecutar animaciones si el valor del slider cambia
        if (lastValue != levelSlider.value)
        {
            TriggerAnimations();
            lastValue = levelSlider.value;
        }
    }

    private void TriggerAnimations()
    {
        if (Mathf.Approximately(levelSlider.value, 2.26f) || Mathf.Approximately(levelSlider.value, -1.5f))
        {
            animationController.PlayAnimationsContinue();
        }
    }

    // Método para actualizar el texto con el valor del slider
    void UpdateLevelText(float value)
    {
        levelText.text = "Energía en " + value.ToString("F2") + " eV";
    }

    

    private void OnDestroy()
    {
        if (linkedObject != null)
        {
            linkedObject.OnRotationChanged -= UpdateLevelBar;
        }
    }


    void Update()
    {
        // Actualizar el targetValue según el estado de isRotated del objeto
        if (linkedObject.isRotated)
        {
            targetValue = -1.5f;
        }
        else
        {
            targetValue = 2.26f;
        }

        // Verificar si se alcanza exactamente el valor objetivo
        if (!animationPlayed && linkedObject.isRotated && Mathf.Approximately(levelSlider.value, targetValue))
        {
            // Detener la rotación y reproducir la animación de separación
            PlayAnimations();
            animationPlayed = true;
            levelSlider.interactable = false; // Deshabilitar el slider
        }
    }

    private void PlayAnimations()
    {
        animator1.SetTrigger("Separate");
        animator2.SetTrigger("Separate");
        animator3.SetTrigger("Separate");
    }



}
