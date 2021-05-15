using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Action<WWW> onSuccess;
    private string imgUrl = "https://www.baidu.com/img/bdlogo.png";
    void Start()
    {
        this.onSuccess += this.SuccessMethod;
        // StartCoroutine("DoSomething");
        // print("Starting"+Time.time);
        // yield return StartCoroutine(Fade());
        // print("Done"+Time.time);
        // yield return new WaitForSeconds(1);
       
        HttpWrapper hw = GetComponent<HttpWrapper>();
        hw.GET(imgUrl,this.onSuccess);
        // StopCoroutine("DoSomething");
    }

    private void SuccessMethod(WWW www){
        if(www == null) return;
        Debug.Log("Success");
        Texture tex = www.texture;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.mainTexture = tex;
    }

    IEnumerator Countdown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
           
        print("WaitAndPrint" + Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s")){
            StartCoroutine(ScreenShotPNG());
        }
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        print("WaitAndPrint" + Time.deltaTime);
    }


    IEnumerator DoSomething(){
        while(true){
            print("DoSometing Loop");
            yield return null;
        }
    }

    IEnumerator Fade()
    {
        print("Fade" + Time.deltaTime);
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = renderer.material.color;
            c.a = f;
            renderer.material.color = c;
            yield return null;
        }
    }

    IEnumerator ScreenShotPNG(){
        yield return new WaitForEndOfFrame();//等待帧结束
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width,height,TextureFormat.RGB24,false);
        tex.ReadPixels(new Rect(0,0,width,height),0,0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);
        File.WriteAllBytes("D:/test.png",bytes);
    }
}
