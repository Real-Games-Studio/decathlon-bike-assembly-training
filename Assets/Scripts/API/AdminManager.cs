using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class AdminManager : MonoBehaviour
{
    [Serializable]
    public class FieldsEmptyEvent : UnityEvent { }
    public FieldsEmptyEvent OnFieldsEmpty;
    public UnityEvent OnAdmin;


    public string adminUser;
    public string adminPass;

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

        if (email == adminUser && senha == adminPass)
        {
            OnAdmin.Invoke();
        }
    }

}
