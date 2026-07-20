using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public MenuFade[] menus; // Lista de todos los menús que usan MenuFade
    private int currentMenuIndex;

    void Start()
    {
        // Recuperar el menú activo guardado al iniciar la escena
        currentMenuIndex = PlayerPrefs.GetInt("CurrentMenu", 0); // Menú predeterminado es el índice 0
        StartCoroutine(ActivateMenu(currentMenuIndex));
    }

    public IEnumerator ActivateMenu(int menuIndex)
    {
        // Si ya hay un menú activo, hacer FadeOut
        if (menus[currentMenuIndex].gameObject.activeSelf)
        {
            yield return StartCoroutine(menus[currentMenuIndex].FadeOut());
        }

        // Hacer FadeIn del nuevo menú
        currentMenuIndex = menuIndex;
        yield return StartCoroutine(menus[menuIndex].FadeIn());

        // Guardar el menú activo actual
        PlayerPrefs.SetInt("CurrentMenu", currentMenuIndex);
    }

    public int GetCurrentMenuIndex()
    {
        return currentMenuIndex; // Retornar el índice del menú actual
    }
}
