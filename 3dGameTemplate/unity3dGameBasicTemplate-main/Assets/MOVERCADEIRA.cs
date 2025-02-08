using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MOVERCADEIRA : MonoBehaviour
{
    public void GirarCadeira(string[] acc){
        transform.eulerAngles = new Vector3(0, 180.0f, (float)Math.Atan2(float.Parse(acc[2]), float.Parse(acc[1]))*180);
        foreach (var message in acc)
        {
            Debug.Log($"Player interagiu! Mensagem: {message}");
        }
    }
}
