using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

public class FirebaseManager : MonoBehaviour
{
    private FirebaseFirestore db;
    public static FirebaseManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null); 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                try
                {
                    AppOptions options = new AppOptions
                    {
                        ProjectId = "cuestionarios-7b7b2",
                        DatabaseUrl = new System.Uri("https://cuestionarios-7b7b2-default-rtdb.firebaseio.com/"),
                        MessageSenderId = "879493368500",
                        StorageBucket = "cuestionarios-7b7b2.firebasestorage.app",
#if UNITY_IOS
                        ApiKey = "AIzaSyD2xqaDT8OL5n7W_3ZyXFk2SnBzz5MCwUM",
                        AppId = "1:879493368500:ios:d78cd175dd4f63781525e1"
#else
                        ApiKey = "AIzaSyBEudJDHk-lOZ2KChIaxAfGhPKL8A2LhYE",
                        AppId = "1:879493368500:android:6591cef61658ccbb1525e1"
#endif
                    };
                    FirebaseApp app = FirebaseApp.Create(options);
                    db = FirebaseFirestore.GetInstance(app);
                    Debug.Log("Firebase inicializado correctamente con opciones manuales.");
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("No se pudo inicializar Firebase con opciones manuales (tal vez ya existe el DefaultInstance). Intentando usar DefaultInstance. Error: " + ex.Message);
                    db = FirebaseFirestore.DefaultInstance;
                }
            }
            else
            {
                Debug.LogError("No se pudo inicializar Firebase: " + task.Result);
            }
        });
    }

    public void SaveResponses(Dictionary<string, List<int>> respuestas, Action onComplete)
    {
        if (db == null)
        {
            Debug.LogError("Firebase Firestore no está inicializado.");
            onComplete?.Invoke();
            return;
        }

        Dictionary<string, object> respuestasDict = new Dictionary<string, object>();
        foreach (var section in respuestas)
        {
            respuestasDict[section.Key] = section.Value;
        }

        db.Collection("Cuestionarios").Document(Guid.NewGuid().ToString())
            .SetAsync(respuestasDict).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.Exception == null)
                {
                    Debug.Log("Respuestas guardadas correctamente en Firestore.");
                }
                else
                {
                    Debug.LogError("Error al guardar en Firestore: " + task.Exception);
                }
                onComplete?.Invoke();
            });
    }
}

