using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GlossaryManager : MonoBehaviour
{
    public GameObject termButtonPrefab; // El prefab del botón del término
    public Transform content; // El objeto Content del Scroll View
    public TMP_Text detailTitleText; // El texto del título del detalle
    public TMP_Text detailDescriptionText; // El texto de la descripción del detalle

    public GameObject PanelDifuminado;
    public GameObject PanelPopup;
    
    private Dictionary<string, string> glossary = new Dictionary<string, string>();

    void Start()
    {
        LoadGlossary(); // Cargar los términos del glosario
        PopulateGlossary(); // Poblar el Scroll View con los términos
    }

    void LoadGlossary()
    {
        // Añadir términos y definiciones al diccionario
        glossary.Add("Cambio espontáneo", "Es un proceso que se realiza de forma natural, sin agregar energía externa.");
        glossary.Add("Cambio exotérmico", "Es un proceso en el cual el sistema libera energía hacia los alrededores.");
        glossary.Add("Concentración en reacción química", "En una reacción química, la concentración de los reactivos disminuye conforme avanza la reacción hasta un valor mínimo.");
        glossary.Add("Energía de formación", "Es la energía necesaria para formar una mol de producto.");
        glossary.Add("Energía de reacción", "Es la diferencia entre la energía de los productos menos la energía de los reactivos.");
        glossary.Add("Estado de transición", "O también llamado complejo activado es la estructura intermedia entre reactivos y productos y su energía corresponde a la energía de activación.");
        glossary.Add("Energía de activación", "Es la energía mínima necesaria para que los reactivos empiecen a reaccionar.");
        glossary.Add("Catálisis", "Es un proceso en el cual una sustancia llamada catalizador (que no es reactivo ni producto de una reacción química) modifica la velocidad de la reacción.");
        glossary.Add("Factores que afectan la rapidez de una reacción", "Algunos factores que afectan la rapidez de una reacción son: la temperatura, la concentración de los reactivos o productos y la presencia de un catalizador.");
        glossary.Add("Energía de adsorción", "Es la energía que se libera en un sistema formado por una molécula o adsorbato que se adhiere a una superficie (sustrato).");
        glossary.Add("Reacción de hidrogenación", "En una reacción de hidrogenación, una molécula recibe uno o más átomos de hidrógeno.");
        glossary.Add("Reacción de deshidrogenación", "Es aquella en la que una molécula pierde uno o más átomos de hidrógeno.");
        glossary.Add("Reacción de decloración", "Es en la cual una molécula pierde uno o más átomos de cloro.");
        glossary.Add("Policlorodioxinas", "Son sustancias químicas persistentes que se desprenden de la quema de plásticos, están formadas por dos ciclos de 6 carbonos unidos por dos átomos de oxígeno, tienen como máximo 8 átomos de cloro.");
        glossary.Add("Número de coordinación", "En estado sólido es el número de átomos con los que forma enlace un átomo en particular.");
    }

    void PopulateGlossary()
    {
        foreach (var term in glossary)
        {
            // Instanciar un nuevo botón a partir del prefab
            GameObject button = Instantiate(termButtonPrefab, content);
            // Configurar el texto del botón
            button.GetComponentInChildren<TMP_Text>().text = term.Key;
            // Añadir un listener al botón para mostrar el detalle al hacer clic
            button.GetComponent<Button>().onClick.AddListener(() => ShowDetail(term.Key));
        }
    }

    void ShowDetail(string term)
    {
        // Actualizar el panel de detalles con el término seleccionado y su descripción
        PanelDifuminado.SetActive(true);
        PanelPopup.SetActive(true);
        detailTitleText.text = term;
        detailDescriptionText.text = glossary[term];
    }
}
