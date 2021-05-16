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
    提前编译（Ahead-of-time,AOT）: 在程序运行前，将exe或dll文件种的CIL的byte code 转译为目标平台的原生码并且存储，但仍有一部分CIL的byte code会在程序运行过程种进行转译，需要JIT编译
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
