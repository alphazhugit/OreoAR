using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class Test : MonoBehaviour {

    public GameObject m_Ao;
    public GameObject m_Yu;
    public GameObject m_Li;

    public List<GameObject> Aoliao = new List<GameObject>();
    // Use this for initialization
    void Start ()
    {
        Aoliao.Add(m_Ao);
        Aoliao.Add(m_Yu);
        Aoliao.Add(m_Yu);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Yu);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Li);
        Aoliao.Add(m_Yu);
        Aoliao.Add(m_Ao);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Apply()
    {
        for (int i = 0; i < Aoliao.Count; ++ i)
        {
            Instantiate(Aoliao[i], new Vector3(0,i * 0.05f,0), Quaternion.identity);
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
