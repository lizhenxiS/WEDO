﻿using UnityEngine;
using System.Collections;
using Wedo_ClientSide;

public enum SHAPE { CIRCLE, TRIANGLE, RECTANGLE, ROUND_RECTANGLE };

public class ShapeClass : MonoBehaviour
{

    private bool isHover = false;
    private Color originColor;
    private bool shapeLock = false; //防止连续生成
    private float lockTime = 3.0f;
    public SHAPE shape;
    private static int circleInstanceCount = 0;
    private static int rectangleInstanceCount = 0;
    private static int roundRectangleInstanceCount = 0;
    private static int triangleInstanceCount = 0;
    public static Vector3 initPos = new Vector3(0, 0, 23);
    public static Vector3 initScale = new Vector3(1, 1, 1);
    public static Vector3 initRotate = new Vector3(0, 0, 0);
    private static string SHAPEPARETNNAME = "ShapeInstance";
    private Vector3 originScale;
    private Vector3 hoverScale;
    private float scaleRate = 2;
    private float originZ;
    private float hoverZ;

    // Use this for initialization
    void Start()
    {
        originColor = renderer.material.color;
        originScale = transform.localScale;
        hoverScale = scaleRate * originScale;
        originZ = transform.position.z;
        hoverZ = originZ - 1;
    }

    // Update is called once per frame
    void Update()
    {
        checkHover();
        createInstanceManage();
    }

    private void createInstanceManage()
    {
        if (isHover)
        {
            if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed)
                || (RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
            {
                if (shapeLock)
                {
                    return;
                }
                else
                {
                    shapeLock = true;
                    Invoke("shapeLockRelease", lockTime);
                }
                if ((RayHit.LeftHitName.Equals(name) && LeftHandProperty.isClosed && !LeftHandProperty.clickUsed))
                {
                    LeftHandProperty.clickUsed = true;
                }
                else if ((RayHit.RightHitName.Equals(name) && RightHandProperty.isClosed && !RightHandProperty.clickUsed))
                {
                    RightHandProperty.clickUsed = true;
                }
                if (WholeStatic.curRoomInterface == null)
                {
                    Debug.Log("curRoomInterface null when create shape");
                }
                if (WholeStatic.curRoomInterface.RoomLayers == null)
                {
                    Debug.Log("Roomlayers null when create shape");
                }
                switch (shape)
                {
                    case SHAPE.CIRCLE:
                        circleInstanceCount++;
                        Debug.Log("shape queue add a circle");
                        RoomStatic.layerArray[RoomStatic.curLayer].AddInstance(RoomStatic.SHAPE_CIRCLE);
                        WholeStatic.curRoomInterface.AddBoardMaterial(
                            WholeStatic.curRoomInterface.RoomLayers[RoomStatic.curLayer - 1].NowLayer.Guid,
                            initPos.x, initPos.y, initPos.z,
                            initScale.x, initScale.y, initScale.z,
                            initRotate.x, initRotate.y, initRotate.z,
                            "C7", RoomStatic.SHAPE_CIRCLE, "", 0, "");
                        Debug.Log("call add a circle in server");
                        break;
                    case SHAPE.RECTANGLE:
                        rectangleInstanceCount++;
                        Debug.Log("shape queue add a rectangle");
                        RoomStatic.layerArray[RoomStatic.curLayer].AddInstance(RoomStatic.SHAPE_RECTANGLE);
                        WholeStatic.curRoomInterface.AddBoardMaterial(
                            WholeStatic.curRoomInterface.RoomLayers[RoomStatic.curLayer - 1].NowLayer.Guid,
                            initPos.x, initPos.y, initPos.z,
                            initScale.x, initScale.y, initScale.z,
                            initRotate.x, initRotate.y, initRotate.z,
                            "C7", RoomStatic.SHAPE_RECTANGLE, "", 0, "");
                        Debug.Log("call add a rectangle in server");
                        break;
                    case SHAPE.ROUND_RECTANGLE:
                        roundRectangleInstanceCount++;
                        Debug.Log("shapa queue add a round rectangle");
                        RoomStatic.layerArray[RoomStatic.curLayer].AddInstance(RoomStatic.SHAPE_ROUNDRECTANGLE);
                        WholeStatic.curRoomInterface.AddBoardMaterial(
                            WholeStatic.curRoomInterface.RoomLayers[RoomStatic.curLayer - 1].NowLayer.Guid,
                            initPos.x, initPos.y, initPos.z,
                            initScale.x, initScale.y, initScale.z,
                            initRotate.x, initRotate.y, initRotate.z,
                            "C7", RoomStatic.SHAPE_ROUNDRECTANGLE, "", 0, "");
                        Debug.Log("call add a round rectangle in server");
                        break;
                    case SHAPE.TRIANGLE:
                        triangleInstanceCount++;
                        Debug.Log("shape queue add a triangle");
                        RoomStatic.layerArray[RoomStatic.curLayer].AddInstance(RoomStatic.SHAPE_TRIANGLE);
                        WholeStatic.curRoomInterface.AddBoardMaterial(
                            WholeStatic.curRoomInterface.RoomLayers[RoomStatic.curLayer - 1].NowLayer.Guid,
                            initPos.x, initPos.y, initPos.z,
                            initScale.x, initScale.y, initScale.z,
                            initRotate.x, initRotate.y, initRotate.z,
                            "C7", RoomStatic.SHAPE_TRIANGLE, "", 0, "");
                        Debug.Log("call add a triangle in server");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void shapeLockRelease()
    {
        shapeLock = false;
    }

    private void checkHover()
    {
        //Debug.Log(RayHit.hitName + " + " + gameObject.name);
        if (MenuBar.isOut && RayHit.hitName.Equals(gameObject.name))
        {
            isHover = true;
            renderer.material.color = Color.red;
            transform.localScale = hoverScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, hoverZ);
        }
        else
        {
            isHover = false;
            renderer.material.color = originColor;
            transform.localScale = originScale;
            transform.position = new Vector3(transform.position.x,
                transform.position.y, originZ);
        }
    }
}
