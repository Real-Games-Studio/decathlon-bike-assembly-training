using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Serializable]
    public class LoginSuccessEvent : UnityEvent<string> { }

    [Serializable]
    public class LoginErrorEvent : UnityEvent<string> { }

    [Serializable]
    public class FieldsEmptyEvent : UnityEvent { }

    public LoginSuccessEvent OnLoginSuccess;
    public LoginErrorEvent OnLoginError;
    public FieldsEmptyEvent OnFieldsEmpty;

    public string url = "http://seu_servidor.com/login.php";
    public TMP_InputField emailInput;
    public TMP_InputField senhaInput;

    public void Login()
    {
        string email = emailInput.text;
        string senha = senhaInput.text;

        // Verificar se os campos estão preenchidos
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
        {
            Debug.Log("campos não preenchidos");
            // Chamar o evento de campos não preenchidos
            if (OnFieldsEmpty != null)
                OnFieldsEmpty.Invoke();
            return;
        }

        StartCoroutine(SendLoginRequest(email, senha));
    }

    IEnumerator SendLoginRequest(string email, string senha)
    {
        // Parâmetros de post (email e senha)
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("senha", senha);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Sucesso na solicitação

                // Ler o JSON de resposta
                string jsonResponse = www.downloadHandler.text;
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(jsonResponse);

                if (response.success)
                {
                    Debug.Log(response.message);
                    // Chamar o evento de sucesso de login
                    if (OnLoginSuccess != null)
                        OnLoginSuccess.Invoke(response.message);
                }
                else
                {
                    Debug.Log(response.message);
                    // Chamar o evento de erro de login
                    if (OnLoginError != null)
                        OnLoginError.Invoke(response.message);
                }
            }
            else
            {
                // Erro na solicitação

                // Chamar o evento de erro de login
                if (OnLoginError != null)
                    OnLoginError.Invoke(www.error);
            }
        }
    }

    [Serializable]
    public class LoginResponse
    {
        public bool success;
        public string message;
    }
}
