using UnityEngine;
using UnityEditor;
using System.Collections;

public class ReplaceObject : EditorWindow {

	GameObject[] objs;
	GameObject objReplacement;
	bool _canReplace;
	
	[MenuItem ("Replace Object/Replace")]
	static void Init () {
		ReplaceObject window = (ReplaceObject)EditorWindow.GetWindow (typeof (ReplaceObject));
	}
	
	void OnGUI () {
		objReplacement = (GameObject)EditorGUILayout.ObjectField("Replacement",objReplacement, typeof(GameObject),true);
		if(GUILayout.Button("Replace Object")){
			if(objs.Length != 0 && objReplacement){
				_canReplace = true;
			}
		}
	}
	
	void Update(){
		objs = Selection.gameObjects;
		if(_canReplace){
			Replace();
		}
	}
	
	void Replace(){
		foreach(GameObject obj in objs){
			GameObject newObj = PrefabUtility.InstantiatePrefab(objReplacement) as GameObject;
			newObj.transform.position = obj.transform.position;
			newObj.transform.parent = obj.transform.parent;
			DestroyImmediate(obj);
		}
		_canReplace = false;
	}
}