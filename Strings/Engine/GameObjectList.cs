﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Strings.Engine
{
    public class GameObjectList : GameObject
    {
        public override bool Died =>
            killed && objList.Count == 0;

        public override void OnDraw()
        {
            foreach(var i in objList)
                i.OnDraw();
        }

        public override void Kill()
        {
            foreach (var i in objList)
                i.Kill();
            killed = true;
        }

        public override void OnUpdate(float time)
        {
            foreach (var i in objList)
                i.OnUpdate(time);
            objList.RemoveAll(x => x.Died);
        }

        public override void OnTouched(TouchEvent te)
        {
            if (ListenTouchEvent)
            {
                foreach (var i in objList)
                    i.OnTouched(te);
            }
        }

        public void Attach(GameObject obj)
        {
            obj.OnAttached(obj);
            objList.Add(obj);
        }

        public bool ListenTouchEvent { get; set; }

        List<GameObject> objList = new List<GameObject>();
        bool killed = false;
    }
}