using UnityEngine;
using System.Collections;
//using Windows.Kinect;


public class KinectOverlayer : MonoBehaviour 
{
	public GUITexture backgroundImage;
	public KinectInterop.JointType trackedJoint = KinectInterop.JointType.HandRight;
	public GameObject overlayObject;
	public float smoothFactor = 5f;
	
	public GUIText debugText;

	private float distanceToCamera = 10f;

	
	void Start()
	{
		if(overlayObject)
		{
			distanceToCamera = (overlayObject.transform.position - Camera.main.transform.position).magnitude;
		}
	}
	
	void Update () 
	{
		KinectManager manager = KinectManager.Instance;
		
		if(manager && manager.IsInitialized())
		{
			//backgroundImage.renderer.material.mainTexture = manager.GetUsersClrTex();
			if(backgroundImage && (backgroundImage.texture == null))
			{
				backgroundImage.texture = manager.GetUsersClrTex();
			}
			
			int iJointIndex = (int)trackedJoint;

			if(manager.IsUserDetected())
			{
				long userId = manager.GetPrimaryUserID();
				
				if(manager.IsJointTracked(userId, iJointIndex))
				{
					Vector3 posJoint = manager.GetJointKinectPosition(userId, iJointIndex);

					if(posJoint != Vector3.zero)
					{
						// 3d position to depth
						Vector2 posDepth = manager.MapSpacePointToDepthCoords(posJoint);
						ushort depthValue = manager.GetDepthForPixel((int)posDepth.x, (int)posDepth.y);

						if(depthValue > 0)
						{
							// depth pos to color pos
							Vector2 posColor = manager.MapDepthPointToColorCoords(posDepth, depthValue);
							
							float xNorm = (float)posColor.x / manager.GetColorImageWidth();
							float yNorm = 1.0f - (float)posColor.y / manager.GetColorImageHeight();
							
//							Vector3 localPos = new Vector3(xNorm * 10f - 5f, 0f, yNorm * 10f - 5f); // 5f is 1/2 of 10 - size of the plane
//							Vector3 vPosOverlay = backgroundImage.transform.TransformPoint(localPos);
							
							if(debugText)
							{
								debugText.GetComponent<GUIText>().text = string.Format("Normalized coords: ({0:F2}, {1:F2})", xNorm, yNorm);
							}
							
							if(overlayObject)
							{
								Vector3 vPosOverlay = Camera.main.ViewportToWorldPoint(new Vector3(xNorm, yNorm, distanceToCamera));
								overlayObject.transform.position = Vector3.Lerp(overlayObject.transform.position, vPosOverlay, smoothFactor * Time.deltaTime);
							}
						}
					}
				}
				
			}
			
		}
	}
}
