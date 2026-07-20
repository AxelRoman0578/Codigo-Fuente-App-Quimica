using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con botones
using System.Collections;
using System;
public class RotateObject : MonoBehaviour
{
    
    [Tooltip("Duración del giro en segundos.")]
    public float rotationDuration = 0.5f; // Duración del giro para hacerlo fluido

    private bool isRotating = false; // Bandera para evitar múltiples rotaciones simultáneas
    private bool isUsingInitialRotation = true; // Bandera para alternar entre las rotaciones
    private Quaternion targetRotation; // Rotación objetivo durante la animación

    public Vector3 initialRotation; // Rotación inicial en x, y, z
    public Vector3 changedRotation; // Rotación de cambio en x, y, z
    public bool isRotated = false; // Bandera para el estado actual de rotación
    public Action<bool> OnRotationChanged;

    void Start()
    {
        // Inicializar la rotación del objeto a la rotación inicial
        transform.rotation = Quaternion.Euler(initialRotation);

        // Inicializar la rotación objetivo como la rotación actual
        targetRotation = transform.rotation;

    }

    public void ToggleRotation()
    {
        if (isRotating) return; // Prevenir múltiples giros simultáneos

        // Alternar entre la rotación inicial y la rotación cambiada
        if (isUsingInitialRotation)
        {
            targetRotation = Quaternion.Euler(changedRotation);
        }
        else
        {
            targetRotation = Quaternion.Euler(initialRotation);
        }

        isUsingInitialRotation = !isUsingInitialRotation; // Cambiar el estado

        isRotated = !isUsingInitialRotation; // Actualizar bandera de estado de rotación

        // Notificar el cambio de rotación
        OnRotationChanged?.Invoke(!isUsingInitialRotation);
        
        StartCoroutine(RotateToTarget());
    }


    public IEnumerator RotateToTarget()
    {
        isRotating = true; // Marcar como en proceso de rotación

        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de alcanzar la rotación final exacta
        transform.rotation = targetRotation;

        isRotating = false; // Marcar que terminó la rotación
    }

}
