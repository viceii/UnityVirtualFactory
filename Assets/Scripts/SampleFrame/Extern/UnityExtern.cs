using System.Diagnostics;
using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SampleFrame
{
    /// <summary>
    /// unity类扩展
    /// </summary>
    public static class UnityExtern
    {
        public static T Get<T>(this Component comp)
        {
            return comp.GetComponent<T>();
        }

        public static bool TryGet<T>(this Component comp, out T t)
        {
            return comp.TryGetComponent<T>(out t);
        }

        public static T[] Gets<T>(this Component comp)
        {
            return comp.GetComponents<T>();
        }

        public static T[] GetsInChild<T>(this Component comp)
        {
            return comp.GetComponentsInChildren<T>();
        }

        public static T Find<T>(this Transform tf, string path)
        {
            return tf.Find(path).Get<T>();
        }

        public static T[] Finds<T>(this Transform tf, string path)
        {
            return tf.Find(path).Gets<T>();
        }

        public static void Add<T>(this GameObject go) where T : Component
        {
            go.AddComponent<T>();
        }

        public static T Get<T>(this GameObject go)
        {
            return go.GetComponent<T>();
        }

        public static bool TryGet<T>(this GameObject go, out T t)
        {
            return go.TryGetComponent<T>(out t);
        }

        public static T[] Gets<T>(this GameObject go)
        {
            return go.GetComponents<T>();
        }

        public static T Find<T>(this GameObject go, string path)
        {
            return go.transform.Find(path).Get<T>();
        }

        public static T[] Finds<T>(this GameObject go, string path)
        {
            return go.transform.Find(path).Gets<T>();
        }
    }
}
