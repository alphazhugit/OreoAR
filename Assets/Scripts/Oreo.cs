using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lean.Touch
{
    public class Oreo : MonoBehaviour
    {

        public GameObject m_Ao;
        public GameObject m_Ao_up;
        public GameObject m_Li;
        private GameObject oreoP;

        public Canvas settingCanvas;
        public Canvas backCanvas;

        private List<GameObject> Aoliao = new List<GameObject>();

        private string eventO = "Play_O";
        private string eventL = "Play_L";
        private string BankName = "Oreo_SoundBank";


        void Start()
        {
            Init();
        }

        void Init()
        {
            oreoP = new GameObject("oreoP");

            var leanScale = oreoP.AddComponent<LeanScale>();
            //var leanRotate = oreoP.AddComponent<LeanRotateCustomAxis>();

            leanScale.ScaleClamp = true;
            leanScale.ScaleMin = new Vector3(0.5f, 0.5f, 0.5f);
            leanScale.ScaleMax = new Vector3(3.0f, 3.0f, 3.0f);



            oreoP.transform.parent = GameObject.Find("Oreo").transform;
            backCanvas.enabled = true;
            settingCanvas.enabled = true;
            if (Aoliao.Count > 0)
            {
                Aoliao.Clear();
            }

            LoadSoundBank();
        }

        public void Apply()
        {
            if (Aoliao.Count > 0)
            {
                Quaternion qua = Quaternion.identity;

                for (int i = 0; i < Aoliao.Count; ++i)
                {
                    if (i == Aoliao.Count - 1 && Aoliao[i] == m_Ao)
                    {
                        Aoliao[i] = m_Ao_up;
                    }
                    Instantiate(Aoliao[i], new Vector3(0, i * 0.1f, 0), qua, oreoP.transform);
                }

                settingCanvas.enabled = false;
                backCanvas.enabled = true;

                VoidAR.GetInstance().startMarkerlessTracking();
            }
        }

        public void Add_Ao()
        {
            Aoliao.Add(m_Ao);
            PostEvent(eventO);
        }

        public void Add_Li()
        {
            Aoliao.Add(m_Li);
            PostEvent(eventL);

        }
        public void Reset()
        {
            if (Aoliao.Count > 0)
            {
                Destroy(oreoP);
                Init();
                SceneManager.LoadScene(1);
                UnLoadSoundBank();
            }
        }

        void LoadSoundBank()
        {
            AkBankManager.LoadBank(BankName, false, false);
        }

        void UnLoadSoundBank()
        {
            AkBankManager.UnloadBank(BankName);
        }

        void PostEvent(string eventname)
        {
            AkSoundEngine.PostEvent(eventname, gameObject);
        }

        void StopAll()
        {
            AkSoundEngine.StopAll();
        }
    }
}
