/**
    ------------Mono的组成-----------------
    1.C#编译器  msc(c#写的编译器) windows上可用Mono运行时也可以用.Net运行时
    2.Mono运行时 JIT(即时编译器)、AOT（提前编译器）、类库加载器、垃圾回收器、线程系统等
    3.基础类库 和微软.Net兼容
    4.Mono类库 eg: Gtk+ 、Zip files、LDAP、OpenGL、Cairo、POSIX
    -----------------------------------------

    ------------Mono运行时----------------
    msc:将C#编译为ECMA CIL的byte code，
    Mono的运行时编译器将CIL的byte code再转译为原生码（Native Code）

    Mono运行时提供2种编译器和3种转译方式：
    （3种转译方式如下）
    即时编译（Just-in-time，JIT）：在程序运行过程种，将CIL的byte code 转译为目标平台的原生码（IOS平台无法使用，所以官方没提供热更新）
    提前编译（Ahead-of-time,AOT）: 在程序运行前，将exe或dll文件中的CIL的byte code 转译为目标平台的原生码并且存储，但仍有一部分CIL的byte code会在程序运行过程种进行转译，需要JIT编译
    完全静态编译（Full-ahead-of-Time,Full-AOT）:出现的目的时完全杜绝运行过程中用JIT编译，即在程序运行之前，所有代码都编译成了目标平台的原生码，Unity开发中，最典型的平台IOS
    
    Mono运行时提供垃圾回收器
    Mono2.8版本前 Boehm-Demers-Weiser garbage collector(贝姆垃圾手机器)  此版本有很大限制，Unity采用的是Mono的早期版本，所以其垃圾回收性能不如.Net
    Mono2.8版本后 generational collector（分代收集）
    -----------------------------

    -----------Mono和脚本----------------------------------------------------------
    开发语言的选择：
                                    -----------------------------
                                            脚本开发语言的应用
       --------------       vs      -----------------------------
       C/C++开发的应用                           脚本引擎
       --------------               -----------------------------
            硬件                                  硬件       
       --------------               -----------------------------
     
    汇编语言
    C/C++ 编译静态不安全语言
    C#、Java编译型安全语言
    Python、Perl、Javascript 解释型动态安全语言

    Mono运行时嵌入应用的步骤：
     1.Unity3D （C++代码写的） 编译C++程序链接Mono运行时（pkg-config工具）
     2.初始化Mono运行时
     3.C/C++和C#/CIL的交互

     -------------------------------------------------
      C/C++代码   =>    Mono运行时 =>    托管代码（CIL）
     -------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoStudy : MonoBehaviour
{

    private Vector3 a;
    private Vector3 b;


    /**
        Awake脚本第一次加载的阶段，Start之前调用
     */
    void Awake()
    {
        print("Awake");
    }
    /**
        OnEnable在对象可用之后调用
     */
    void OnEnable()
    {
        print("OnEnable");
    }

    void OnDisable(){
        print("OnDisable");
    }
    // Start is called before the first frame update

    /**
        Start 在第一帧更新之前调用
     */
    void Start()
    {
        print("Start");
        
        a = new Vector3(1,2,1);
        b = new Vector3(5,6,0);

    }

    // Update is called once per frame
    void Update()
    {
        // print("Update");
    }

    /**
        FixedUpdate在帧率低的时候，每帧可能调用多次
        帧率高的时候，可能不会每帧都调用
        主要用于处理物理计算相关的逻辑，如处理刚体
        基于可靠的定时器不受帧率影响
     */
    void FixedUpate()
    {
        // print("FixedUpdate");
    }

    /**
        LateUpdate每帧调用,Update中的任何计算都会在LateUpdate开始之前完成
        常见的第三人称控制器的相机跟随（人物的旋转移动在Update，相机的旋转和移动在LateUpdate）
        用于处理Update方法之后，相机渲染之前的逻辑    
     */
    void LateUpdate()
    {
        // print("LateUpdate");
    }

    /**
        所有帧更新之后被调用（对象存在的最后一帧）

     */
    void OnDestory(){
        print("OnDestory");
    }

    void OnApplicationQuit(){
        print("OnApplicationQuit");
    }

    //================================渲染的部分===================================

    /**
        相机提出场景前被调用，剔除取决于物体在相机是否可见，仅在剔除执行之前被调用
     */
    void OnPreCull()
    {
        print("OnPreCull");
    }

    /**
        物体在任何相机中可见时被调用
     */
    void OnBecameVisible(){
        print("OnBecameVisible");
    }
    /**
        物体在任何相机中不可见时被调用
     */
    void OnBecameInvisible(){
        print("OnBecameInvisible");
    }

    /**
        如果物体可见将为每个摄像机调用一次
     */
    void OnWillRenderObject(){
        // print("OnWillRenderObject");
    }

    /**
        相机渲染场景之前被调用
     */
    void OnPreRender(){
        print("OnPreRender");
    }

     /**
        在所有固定的场景渲染之后被调用，此时可以用GL类或者Graphics.DrawMeshNow 来画自定义的几何体
     */
    void OnRenderObject(){
        // print("OnRenderObject");
    }

     /**
        相机完成场景渲染后被调用
     */
    void OnPostRender(){
        print("OnPostRender");
    }

    /**
        仅专业版可用，场景渲染完成后被调用，用来对屏幕的图像进行处理
     */
    // void OnRenderImage(){
    //     print("OnRenderImage");
    // }

    /*
        每帧被调用，多次用来回应GUI事件，布局和重绘事件先被执行，接下来是为每一次的输入事件执行布局和键盘、鼠标事件
     */
    void OnGUI(){
        // print("OnGUI");

        float c = Vector3.Dot(a,b); //点乘积的几何意义夹角 a·b = |a|·|b|cos<a,b>   值
        //向量a，b的夹角得到的值为弧度，转换为角度
        float angle = Mathf.Acos(Vector3.Dot(a.normalized,b.normalized))*Mathf.Rad2Deg;

        GUILayout.Label("向量a，b的点积为："+ c);
        GUILayout.Label("向量a，b的夹角为："+ angle);

        Vector3 e = Vector3.Cross(a,b);//两个向量的交叉乘积还是向量 c = a * b  c ⊥ a， c ⊥ b，|c| = |a||b|sin<a,b>
        Vector3 d = Vector3.Cross(b,a);//a*b ≠ b*a    a*b = -b*a  可以用正负判断ab的相对位置，顺时针还是逆时针

        angle = Mathf.Asin(Vector3.Distance(Vector3.zero,Vector3.Cross(a.normalized,b.normalized)))*Mathf.Rad2Deg;

        GUILayout.Label("向量a*b为："+ e);
        GUILayout.Label("向量b*a为："+ d);
        GUILayout.Label("向量a，b的夹角为："+ angle);

    }

    /*
        为了可视化的目的，在场景视图中绘制小图标
     */
    void OnDrawGizmos(){
        // print("OnDrawGizmos");
    }

}
