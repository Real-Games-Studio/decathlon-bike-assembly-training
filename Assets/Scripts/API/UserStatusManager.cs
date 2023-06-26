using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Events;

public class UserStatusManager : MonoBehaviour
{
    
    public UnityEvent OnTutorial;
    public UnityEvent OnGuiado;
    public UnityEvent OnDesafio;



    public string url = "http://seu_servidor.com/status.php";

    public void GetStatusData()
    {
        StartCoroutine(CoroutineGetStatusData());
    }

    IEnumerator CoroutineGetStatusData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Obter os dados JSON da resposta
                string jsonResponse = www.downloadHandler.text;

                // Converter o JSON para um objeto StatusResponse
                StatusResponse statusResponse = JsonUtility.FromJson<StatusResponse>(jsonResponse);

                // Tratar os valores das colunas tutorial, guiado e desafio
                int tutorial = 0;
                int guiado = 0;
                int desafio = 0;

                int.TryParse(statusResponse.tutorial, out tutorial);
                int.TryParse(statusResponse.guiado, out guiado);
                int.TryParse(statusResponse.desafio, out desafio);

                if(tutorial>0) {
                    OnTutorial.Invoke();
                }
                if(guiado>0) {
                    OnGuiado.Invoke();
                }
                if(desafio>0) {
                    OnDesafio.Invoke();
                }

            }
            else
            {
                Debug.LogError("Falha na obtenção dos dados de status: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class StatusResponse
    {
        public string tutorial;
        public string guiado;
        public string desafio;
    }
}
