using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HttpWrapper : MonoBehaviour
{
    public void GET(string url,Action<WWW> onSuccess,Action<WWW> onFail = null){
        WWW www = new WWW(url);
        StartCoroutine(WaitForResponse(www,onSuccess,onFail));
    }

    public void POST(string url,Dictionary<string,string> post,Action<WWW> onSuccess,Action<WWW> onFail = null){
        WWWForm form= new WWWForm();
        foreach(KeyValuePair<string,string> post_arg in post){
            form.AddField(post_arg.Key,post_arg.Value);
        }
        WWW www = new WWW(url,form);
        StartCoroutine(WaitForResponse(www,onSuccess,onFail));

    }

    private IEnumerator WaitForResponse(WWW www,Action<WWW> onSuccess,Action<WWW> onFail = null){
        yield return www;
        if(www.error==null){
            onSuccess(www);
        }else{
            Debug.LogError("WWW Error:" + www.error);
            if(onFail != null) onFail(www);
        }
    }
}
