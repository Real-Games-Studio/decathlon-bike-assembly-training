using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class UserPerformanceManager : MonoBehaviour
{
    public string url = "http://seu_servidor.com/performance.php";
    [System.Serializable]
    public class RegisterSuccessEvent : UnityEvent { }

    [System.Serializable]
    public class RegisterErrorEvent : UnityEvent<string> { }

    public RegisterSuccessEvent OnRegisterSuccess;
    public RegisterErrorEvent OnRegisterError;

    public void RegisterPerformance(float tempoTotal, int pontos)
    {
        StartCoroutine(SendRegisterRequest(tempoTotal, pontos));
    }

    IEnumerator SendRegisterRequest(float tempoTotal, int pontos)
    {    
        // Parâmetros de post (email, dataHora, tempoTotal, pontos)
        WWWForm form = new WWWForm();
        form.AddField("tempo", tempoTotal.ToString());
        form.AddField("pontos", pontos.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Sucesso na solicitação

                // Chamar o evento de sucesso no registro
                if (OnRegisterSuccess != null)
                    OnRegisterSuccess.Invoke();
            }
            else
            {
                // Erro na solicitação

                // Chamar o evento de erro no registro
                if (OnRegisterError != null)
                    OnRegisterError.Invoke(www.error);
            }
        }
    }
}
