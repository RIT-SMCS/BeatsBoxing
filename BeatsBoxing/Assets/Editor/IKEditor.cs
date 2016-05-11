using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class IKEditor {

	static IKEditor ()
	{
		SceneView.onSceneGUIDelegate += OnScene;
	}

	const float gizmoSize = 0.2f;
	static Color gizmoColor = new Color (0, 0, 1, 1);
    static Color redColor = new Color(1, 0, 0, 1);
	
	// Update is called once per frame
	static void OnScene (SceneView sceneview) {
		var targets = GameObject.FindObjectsOfType<SimpleIK> ();

		foreach (var target in targets) {
			foreach (var node in target.angleLimitsFacingRight){
				if (node.transform == null){
					continue;
				}

				Transform transform = node.transform;
				Vector3 position = transform.position;

				float handleSize = HandleUtility.GetHandleSize(position);
				float discSize = handleSize * gizmoSize;

				float parentRotation =  transform.parent ? transform.parent.eulerAngles.z : 0;
				Vector3 min = Quaternion.Euler(0,0,node.min + parentRotation)*Vector3.down;
				Vector3 max = Quaternion.Euler(0,0,node.max + parentRotation)*Vector3.down;

				gizmoColor[3] = 0.1f;
				Handles.color = gizmoColor;
				Handles.DrawWireDisc(position, Vector3.back, discSize);
				Handles.DrawSolidArc(position, Vector3.forward, min, node.max - node.min, discSize);

				gizmoColor[3] = 1f;
				Handles.DrawLine(position, position + min * discSize);
				Handles.DrawLine(position, position + max * discSize);

				//Vector3 toChild = FindChildNode(tran
			}

            foreach (var node in target.angleLimitsFacingLeft)
            {
                if (node.transform == null)
                {
                    continue;
                }

                Transform transform = node.transform;
                Vector3 position = transform.position;

                float handleSize = HandleUtility.GetHandleSize(position);
                float discSize = handleSize * gizmoSize;

                float parentRotation = transform.parent ? transform.parent.eulerAngles.z : 0;
                Vector3 min = Quaternion.Euler(0, 0, node.min + parentRotation) * Vector3.down;
                Vector3 max = Quaternion.Euler(0, 0, node.max + parentRotation) * Vector3.down;

                redColor[3] = 0.1f;
                Handles.color = redColor;
                Handles.DrawWireDisc(position, Vector3.back, discSize);
                Handles.DrawSolidArc(position, Vector3.forward, min, node.max - node.min, discSize);

                redColor[3] = 1f;
                Handles.DrawLine(position, position + min * discSize);
                Handles.DrawLine(position, position + max * discSize);

                //Vector3 toChild = FindChildNode(tran
            }
		}
	}
}
