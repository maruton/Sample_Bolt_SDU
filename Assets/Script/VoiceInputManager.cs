/*
 *  Script Refference：
 *      https://docs.unity3d.com/ja/2018.2/ScriptReference/Windows.Speech.KeywordRecognizer.html
 *      
 *  Bolt連携の基礎：
 *      ソースを変更したら
 *      エディター上部のメニュー Tools > Bolt > Build Unit Options を実行すること。
 *      Boltのノードで候補に出てくるようになる。 
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Ludiq;
using Bolt;
using System.Collections;

public class VoiceInputManager : MonoBehaviour {
//    [SerializeField]
//    private string[] m_Keywords = { "" };

    private KeywordRecognizer m_Recognizer;
    public string[] VoiceKeyword = new string[0];   //!< 音声認識設定用キーワード 
    public Dictionary<string, string> VoiceKeyCmd = new Dictionary<string, string>();   //!< 音声認識キーワードとコマンドの組み合わせ辞書 
    private GameObject playerControlManager = null;
    private GameObject cameraChanger = null;
        

    public void start_KeywordRecognizer(AotDictionary dic) {
        int max = dic.Count;
        string[] aKey = new string[max];
        string[] aValue= new string[max];
        Array.Resize(ref VoiceKeyword, max);

        ICollection k = dic.Keys;
        ICollection v = dic.Values;
        k.CopyTo(VoiceKeyword, 0);
        k.CopyTo(aKey, 0);
        v.CopyTo(aValue, 0);

        for (int i = 0; i < max; i++) {
            VoiceKeyCmd.Add(aKey[i], aValue[i]);
        }

        m_Recognizer = new KeywordRecognizer(VoiceKeyword);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized; // 音声認識時のコールバック設定 
        m_Recognizer.Start();   // 音声認識開始 
    }
    private void end_KeywordRecognizer() {
        if(m_Recognizer != null) {
            m_Recognizer.Stop();
            m_Recognizer.Dispose();
        }
    }

    FlowMachine fm_flowMachine = null; 

    void Start() {
        /*  直接 Graphの変数を取得するテスト。
         *  Start()の呼び出し順管理が面倒そうなので
         *  引数でdicをもらうようにした -> start_KeywordRecognizer()。 
        fm_flowMachine = gameObject.GetComponent<FlowMachine>();
        var graphReference = GraphReference.New(fm_flowMachine, true);
        Variables.GraphInstance(graphReference).Set("DebugStatus", "TEST");
        var a = Variables.GraphInstance(graphReference).Get("DebugStatus");
        AotDictionary dicVoice = (AotDictionary)Variables.GraphInstance(graphReference).Get("dic_Keyword_to_ID");
        */

        playerControlManager = GameObject.Find("PlayerControlManager"); // グラフ Playerinput の付いているオブジェクトを探して格納。 
        cameraChanger = GameObject.Find("CameraChanger");               // グラフ CameraChanger の付いているオブジェクトを探して格納。 

    }

    /*
    void OnDisable() {
        Debug.Log("PrintOnDisable: script was disabled");
    }

    void OnEnable() {
        Debug.Log("PrintOnEnable: script was enabled");
        end_KeywordRecognizer();
    }
    */

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args) {
        Debug.Log(args.text); // 認識した音声コマンドを表示。 

        if (VoiceKeyCmd.ContainsKey(args.text)) {
            var cmd = VoiceKeyCmd[args.text];
            switch (cmd) {
                case "STOP":
                    CustomEvent.Trigger(playerControlManager, "PlayerAction_Stop", 1);  // Boltのカスタムイベント"PlayerAction_Stop"を呼び出す。 
                    break;
                case "WALK":
                    CustomEvent.Trigger(playerControlManager, "PlayerAction_Walk", 1);  // Boltのカスタムイベント"PlayerAction_Walk"を呼び出す。 
                    break;
                case "RUN":
                    CustomEvent.Trigger(playerControlManager, "PlayerAction_Run", 1);   // Boltのカスタムイベント"PlayerAction_Run"を呼び出す。 
                    break;
                case "JUMP":
                    CustomEvent.Trigger(playerControlManager, "PlayerAction_Jump", 1);  // Boltのカスタムイベント"PlayerAction_Jump"を呼び出す。 
                    break;
                case "CAMERA":
                    CustomEvent.Trigger(cameraChanger, "ChangeCamera");              // Boltのカスタムイベント"ChangeCamera"を呼び出す。 
                    break;
            }
        }

    }
}