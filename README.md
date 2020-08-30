# Boltサンプル
SD Unityちゃんを使用したサンプル。

・SD Unityちゃんがアニメーションする  
・カメラが切り替えられる。  

Unity2019.4.3f1  
Windows専用。  
※音声認識にWindowsの機能を使用している為。  

基本的にBoltのノードで作成。  
一部C#連携。  


## 操作方法
キーボード操作でアニメーションとカメラ視点が切り替えられます。
音声でも操作できます。

### キー操作方法 
停止：[1]キー  
歩く：[2]キー  
走る：[3]キー  
ジャンプ：[SPACE]キー  
カメラ切り替え：[C]キー　※クリックするごとに、回転視点 > Front視点 > TPS視点 > 回転視点に戻るように順に切り替え。  


### 音声操作方法
停止：発音「とまる」「ストップ」  
歩く：発音「あるく」「ウォーク」  
走る：発音「はしる」「ラン」  
ジャンプ：発音「とぶ」「ジャンプ」  
カメラ切り替え：「カメラ」  

※マイクが必要です。  
※他にマイクを使うアプリは全て終了してからUnityを起動して下さい。  

/Assets/Macros/KeywordFile に発音とコマンドの対応定義があります。  
書き換えれば任意の音声で操作できます。  



-----
# 使用している外部Assetについて

## 【ユニティちゃんトゥーンシェーダー Ver.2.0.7】
### Download
[UnityChanToonShaderVer2_Project (Zip)](https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/archive/master.zip)  

### License
「ユニティちゃんトゥーンシェーダーVer.2.0」は、UCL2.0（ユニティちゃんライセンス2.0）で提供されます。  
ユニティちゃんライセンスについては、以下を参照してください。  
http://unity-chan.com/contents/guideline/

Unity-Chan Toon Shader 2.0 is provided under the Unity-Chan License 2.0 terms.  
Please refer to the following link for information regarding the Unity-Chan License.  
http://unity-chan.com/contents/guideline_en/


## SimpleAnimation
### Download
https://github.com/Unity-Technologies/SimpleAnimation  
  
### License
MIT License
詳細は付属のLICENSE ファイル(/Assets/SimpleAnimation/README.md)を参照。  
See LICENSE file /Assets/SimpleAnimation/README.md.  



ソース一部改変にて使用。  

```
----- SimpleAnimation.cs -----末尾に追加
    // Begin: Add
    public bool isPlayEnd(string stateName) {
        float t = GetState(stateName).normalizedTime;
        if (t >= 1.0) {
            return (true);
        }
        return (false);
    }
    public float GetNormalizeTime(string stateName) {
        float t = GetState(stateName).normalizedTime;
        return (t);
    }
    // End: Add
----------
```


