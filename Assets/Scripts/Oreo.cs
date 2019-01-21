using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class Oreo : MonoBehaviour {

    public GameObject m_Ao;
    public GameObject m_Ao_up;
    public GameObject m_Li;
    public Transform oreoP;


    private List<GameObject> Aoliao = new List<GameObject>();
    // Use this for initialization
    void Start ()
    {
        Aoliao.Add(m_Ao);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Ao);
        Aoliao.Add(m_Ao);
        Aoliao.Add(m_Ao);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Ao);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Apply()
    {
        Quaternion qua = Quaternion.identity;

        for (int i = 0; i < Aoliao.Count; ++ i)
        {
            if (i == Aoliao.Count - 1)
            {
                Aoliao[i] = m_Ao_up;
            }
            Instantiate(Aoliao[i], new Vector3(0,i * 0.03f,0), qua, oreoP);
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Apply"))
        {
            Apply();
        }
    }
}
