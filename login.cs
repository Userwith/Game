using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System;
using UnityEngine;
using UnityEngine.UI;

public class login : MonoBehaviour {
	public GameObject button;
	public GameObject UserName;
	public GameObject Password;
	public AudioSource Taudio;
	// Use this for initialization
	void Start () {
		
		string constr = "server=127.0.0.1;Database=MVCproject;uid=root;pwd=12345678;charset=utf8";
		MySqlConnection mycon = new MySqlConnection (constr);
		mycon.Open();
		InputField Usn = UserName.GetComponent<InputField>();
		InputField pas = Password.GetComponent<InputField>();
		Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(delegate(){
			Taudio.Play();
			string sel = "select * from userinfo";
			MySqlCommand myselect = new MySqlCommand (sel, mycon);
			MySqlDataAdapter adapter = new MySqlDataAdapter(myselect);
			DataSet ds = new DataSet();
			adapter.Fill(ds);
			bool flag = false;
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				
				string uid = ds.Tables[0].Rows[i][1].ToString();
				string pwd = ds.Tables [0].Rows [i] [2].ToString ();
				if(Usn.text.Equals(uid)){
					if(pas.text.Equals(pwd)){
						flag = true;
						break;
					}
				}
			}
			if(flag){
			mycon.Close();
			Debug.Log("Confirm");
			this.GoNextScene(button);
			}else
			Debug.Log("wrong");
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GoNextScene(GameObject obj){
		Application.LoadLevelAsync("home");

	}
}
