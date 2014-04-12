﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MMD.VMD;

namespace MMD
{
    namespace VMD
    {
        public class VMDFormatter
        {
            GameObject mmd_object;
            VMDFormat format;

            public VMDFormat Format { get { return format; } }

            public VMDFormatter(GameObject target)
            {
                mmd_object = target;
                format = new VMDFormat();
                format.header = new VMDFormat.Header();
                format.header.vmd_model_name = target.name;
                format.header.vmd_header = "Vocaloid Motion Data 0002";
                format.motion_list = new VMDFormat.MotionList();
                format.skin_list = new VMDFormat.SkinList();
                format.camera_list = new VMDFormat.CameraList();
                format.light_list = new VMDFormat.LightList();
                format.self_shadow_list = new VMDFormat.SelfShadowList();
            }

            public VMDFormat InsertMorph(uint insert_frame_no)
            {
                // Expression以下のGameObjectを取り出し
                var expression = mmd_object.transform.FindChild("Expression");
                var expressions = new List<Transform>();
                for (int i = 0; i < expression.childCount; i++)
                    expressions.Add(expression.GetChild(i));

                // GameObjectごとにVMDFormatを構成
                foreach (var exp in expressions)
                {
                    if (exp.name == "base" || exp.localPosition.z == 0f) continue;
                    var skin = new VMDFormat.SkinData();
                    skin.frame_no = insert_frame_no;
                    skin.skin_name = exp.name;
                    skin.weight = exp.localPosition.z;

                    format.skin_list.Insert(skin);
                }
                return format;
            }
        }
    }
}