using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator[] animators;
    public GameObject continueButton; // El botón de continuar
    public GameObject questionButton; // El botón de continuar
    public GameObject rotateButton; // El botón de rotar
    public GameObject[] botonfin;

    public void PlayAnimationsContinue()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Continue");
        }
    }

    public void PlayAnimationsBack()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("Back");
        }
        // Reiniciar las animaciones a su estado inicial
        ResetAnimations();
    }

    private void ResetAnimations()
    {
        foreach (Animator animator in animators)
        {
            animator.gameObject.SetActive(false); // Desactivar el objeto del animador
            animator.gameObject.SetActive(true);  // Volver a activar el objeto del animador
        }
    }

    private void ShowContinueButton()
    {
        continueButton.SetActive(true); // Mostrar el botón de continuar
        questionButton.SetActive(true);
        rotateButton.SetActive(false); // Mostrar el botón de rotar
    }

    private void ShowButtonFin()
    {
        for (int i = 0; i < botonfin.Length; i++)
        {
            // Activar el objeto
            botonfin[i].SetActive(true);
        }
        continueButton.SetActive(true);// Mostrar el botón de continuar
        questionButton.SetActive(true); 
        rotateButton.SetActive(false); // Mostrar el botón de rotar
    }
}
