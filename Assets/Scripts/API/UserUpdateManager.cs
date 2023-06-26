using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class UserUpdateManager : MonoBehaviour
{
    [System.Serializable]
    public class UpdateSuccessEvent : UnityEvent { }

    [System.Serializable]
    public class UpdateErrorEvent : UnityEvent<string> { }

    public UpdateSuccessEvent OnUpdateSuccess;
    public UpdateErrorEvent OnUpdateError;

    public string endpoint = "http://seu_servidor.com/update.php?tipo=";

    public void UpdateData(string tipo)
    {
        StartCoroutine(SendUpdateRequest(tipo));
    }

    IEnumerator SendUpdateRequest(string tipo)
    {
        // URL do arquivo PHP de atualização
        string url = endpoint + tipo;

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Sucesso na solicitação

                // Chamar o evento de sucesso na atualização
                if (OnUpdateSuccess != null)
                    OnUpdateSuccess.Invoke();
            }
            else
            {
                // Erro na solicitação

                // Chamar o evento de erro na atualização
                if (OnUpdateError != null)
                    OnUpdateError.Invoke(www.error);
            }
        }
    }
}
