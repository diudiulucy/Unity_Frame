using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Linq;
public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    private Action<WWW> onSuccess;
    private string imgUrl = "https://www.baidu.com/img/bdlogo.png";
    private string result = "hello world";

    /**
        Start 在第一帧更新之前调用
     */
    void Start()
    {
        // print("Start");
        this.onSuccess += this.SuccessMethod;
        // StartCoroutine("DoSomething");
        // print("Starting"+Time.time);
        // yield return StartCoroutine(Fade());
        // print("Done"+Time.time);
        // yield return new WaitForSeconds(1);
       
        // HttpWrapper hw = GetComponent<HttpWrapper>();
        // hw.GET(imgUrl,this.onSuccess);
        // StopCoroutine("DoSomething");
        // LoadXML("Test");
        // StartCoroutine(LoadXMLSPath());
        // LoadXMLAb();
    }

  
    void OnDisable(){
        // print("OnDisable");
    }
    
    
    // Update is called once per frame
    void Update()
    {
        //  print("Update");
        if(Input.GetKeyDown("s")){
            print("Update");
            StartCoroutine(ScreenShotPNG());
        }
    }



    void OnGUI(){
        // print("OnGUI");
        GUIStyle titleStyle = new GUIStyle();
        titleStyle.fontSize = 20;
        titleStyle.normal.textColor = new Color(45f/256f,163f/256f,256f/256f,256f/256f);
        GUI.Label(new Rect(0,0,500,200),result,titleStyle);
    }

    void Reset(){
        // print("Reset");
    }

    /**
        Resources目录 只读，不能动态修改
        主线程加载
        资源读取用Resources.Load()
        打包时压缩和加密
     */
    private void LoadXML(string path){
        result = Resources.Load(path).ToString();
        // XmlDocument doc = new XmlDocument();
        // doc.LoadXml(result); 
    }

    /**
        StreamingAssets目录 只读
        主要用来存放二进制文件
        只能用WWW来读取
        原封不动打进包，不会压缩和加密（不要直接把数据文件放到这个目录打包）
     */
    IEnumerator LoadXMLSPath(){
        string sPath = Application.streamingAssetsPath + "Test.xml";
        WWW www = new WWW(sPath);
        yield return www;
        if(www.error == null){
            print("LoadXML failed");
        }else{
             print("LoadXML success");
        }
        result = www.text;
        print(www.text+"loadXmlspath");
    }

    void LoadXMLAb(){
        AssetBundle ab;
        string str = Application.streamingAssetsPath + "/test.bundle";
        WWW www = new WWW(str);
        www = WWW.LoadFromCacheOrDownload(str,0);
        ab = www.assetBundle;
        TextAsset test = ab.LoadAsset("Test.xml") as TextAsset;
        result = test.ToString();
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
