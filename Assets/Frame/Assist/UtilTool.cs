//======================================================================
//        Copyright (C) 2015-2020 Winddy He. All rights reserved
//        Email: hgplan@126.com
//======================================================================
using UnityEngine;
using System.Collections;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Linq;

namespace Knight.Core
{
    public static class UtilTool
    {
        public static readonly string SessionSecrect = "pomelo_session_secret_winddy";
        public static readonly uint SessionSeed = 98723;

        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void ExitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public static void SafeExecute(Action rAction)
        {
            if (rAction != null) rAction();
        }

        public static void SafeExecute<T>(Action<T> rAction, T rObj)
        {
            if (rAction != null) rAction(rObj);
        }

        public static void SafeExecute<T1, T2>(Action<T1, T2> rAction, T1 rObj1, T2 rObj2)
        {
            if (rAction != null) rAction(rObj1, rObj2);
        }

        public static void SafeExecute<T1, T2, T3>(Action<T1, T2, T3> rAction, T1 rObj1, T2 rObj2, T3 rObj3)
        {
            if (rAction != null) rAction(rObj1, rObj2, rObj3);
        }

        public static void SafeExecute<T1, T2, T3, T4>(Action<T1, T2, T3, T4> rAction, T1 rObj1, T2 rObj2, T3 rObj3, T4 rObj4)
        {
            if (rAction != null) rAction(rObj1, rObj2, rObj3, rObj4);
        }

        public static TResult SafeExecute<TResult>(Func<TResult> rFunc)
        {
            if (rFunc == null) return default(TResult);
            return rFunc();
        }

        public static TResult SafeExecute<T, TResult>(Func<T, TResult> rFunc, T rObj)
        {
            if (rFunc == null) return default(TResult);
            return rFunc(rObj);
        }

        public static TResult SafeExecute<T1, T2, TResult>(Func<T1, T2, TResult> rFunc, T1 rObj1, T2 rObj2)
        {
            if (rFunc == null) return default(TResult);
            return rFunc(rObj1, rObj2);
        }

        public static TResult SafeExecute<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> rFunc, T1 rObj1, T2 rObj2, T3 rObj3)
        {
            if (rFunc == null) return default(TResult);
            return rFunc(rObj1, rObj2, rObj3);
        }

        public static TResult SafeExecute<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> rFunc, T1 rObj1, T2 rObj2, T3 rObj3, T4 rObj4)
        {
            if (rFunc == null) return default(TResult);
            return rFunc(rObj1, rObj2, rObj3, rObj4);
        }

        public static float WrapAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        public static byte[] GetMD5(string rContentFile)
        {
            return GetMD5(new List<string>() { rContentFile });
        }

        public static byte[] GetMD5(List<string> rContentFiles)
        {
            rContentFiles.Sort((a1, a2) => { return a1.CompareTo(a2); });

            HashAlgorithm rHasAlgo = HashAlgorithm.Create("MD5");
            byte[] rHashValue = new byte[20];

            byte[] rTempBuffer = new byte[4096];
            int rTempCount = 0;
            for (int i = 0; i < rContentFiles.Count; i++)
            {
                if (File.Exists(rContentFiles[i]))
                {
                    FileStream fs = File.OpenRead(rContentFiles[i]);
                    while (fs.Position != fs.Length)
                    {
                        rTempCount += fs.Read(rTempBuffer, 0, 4096 - rTempCount);
                        if (rTempCount == 4096)
                        {
                            if (rHasAlgo.TransformBlock(rTempBuffer, 0, rTempCount, null, 0) != 4096)
                            rTempCount = 0;
                        }
                    }
                    fs.Close();
                }
            }
            rHasAlgo.TransformFinalBlock(rTempBuffer, 0, rTempCount);
            rHashValue = rHasAlgo.Hash;
            return rHashValue;
        }

        public static string ToHEXString(this byte[] rSelf)
        {
            var rText = new StringBuilder();
            for (int nIndex = 0; nIndex < rSelf.Length; ++nIndex)
                rText.AppendFormat("{0:X2}", rSelf[nIndex]);
            return rText.ToString();
        }

        public static string HashAlgorithmByString(string rText, string rHashName, Encoding rEncoding)
        {
            var rHashAlgorithm = HashAlgorithm.Create(rHashName);
            var rTextBytes = rEncoding.GetBytes(rText);
            rHashAlgorithm.TransformFinalBlock(rTextBytes, 0, rTextBytes.Length);
            return rHashAlgorithm.Hash.ToHEXString();
        }

        public static byte[] GetMD5ToBytes(string rText)
        {
            var rHashAlgorithm = HashAlgorithm.Create("MD5");
            var rTextBytes = Encoding.UTF8.GetBytes(rText);
            rHashAlgorithm.TransformFinalBlock(rTextBytes, 0, rTextBytes.Length);
            return rHashAlgorithm.Hash;
        }
        public static string GetMD5(byte[] rDatas)
        {
            var rHashAlgorithm = HashAlgorithm.Create("MD5");
            rHashAlgorithm.TransformFinalBlock(rDatas, 0, rDatas.Length);
            return rHashAlgorithm.Hash.ToHEXString();
        }

        public static string GetFilesMD5(string[] rFiles)
        {

            Array.Sort(rFiles, (a, b) => { return a.CompareTo(b); });

            HashSet<string> rFilesSet = new HashSet<string>();
            foreach (var rItem in rFiles)
            {
                rFilesSet.Add(rItem);
            }

            var rStringBuilder = new StringBuilder();
            var rPreFix = Application.dataPath.Substring(0, Application.dataPath.Length - 6);
            foreach (var rItem in rFiles)
            {
                if (rItem.EndsWith(".meta"))
                {
                    continue;
                }
                var rRelativeName = rItem.Replace(rPreFix, "");
                rStringBuilder.Append(rRelativeName);

                var rMeta = rItem + ".meta";
                if (File.Exists(rMeta))
                {
                    rFilesSet.Add(rMeta);
                }
            }

            rFiles = rFilesSet.ToArray();
            Array.Sort(rFiles, (a, b) => { return a.CompareTo(b); });

            var rHashMD5 = string.Empty;
            var rMemory = new System.IO.MemoryStream();
            foreach (var item in rFiles)
            {
                if (File.Exists(item))
                {
                    var rFileStream = File.OpenRead(item);
                    var rBuffer = new byte[rFileStream.Length];
                    rFileStream.Read(rBuffer, 0, rBuffer.Length);
                    rFileStream.Close();
                    rMemory.Write(rBuffer, 0, rBuffer.Length);
                }
            }

            var rFileNameBuffer = System.Text.UTF8Encoding.UTF8.GetBytes(rStringBuilder.ToString());
            rMemory.Write(rFileNameBuffer, 0, rFileNameBuffer.Length);

            rMemory.Position = 0;
            System.Security.Cryptography.MD5 rCalculator = System.Security.Cryptography.MD5.Create();
            Byte[] rBufferBytes = rCalculator.ComputeHash(rMemory);
            rCalculator.Clear();
            rMemory.Close();
            rStringBuilder = new System.Text.StringBuilder();
            for (int i = 0; i < rBufferBytes.Length; i++)
            {
                rStringBuilder.Append(rBufferBytes[i].ToString("x2"));
            }
            rHashMD5 = rStringBuilder.ToString();
            return rHashMD5;
        }

        public static string HashAlgorithmByDataBytes(byte[] rBytes, string rHashName)
        {
            var rHashAlgorithm = HashAlgorithm.Create(rHashName);
            rHashAlgorithm.TransformFinalBlock(rBytes, 0, rBytes.Length);
            return rHashAlgorithm.Hash.ToHEXString();
        }

        public static string GetMD5String(string rText, Encoding rEncoding)
        {
            return HashAlgorithmByString(rText, "MD5", rEncoding);
        }

        public static string GetMD5String(string rText)
        {
            return GetMD5String(rText, Encoding.Default);
        }

        public static string PathCombine(char rDirectoryChar, params string[] rPaths)
        {
            var rReplaceChar = rDirectoryChar == '\\' ? '/' : '\\';
            if (rPaths.Length == 0)
                return string.Empty;

            var rFirstPath = rPaths[0].Replace(rReplaceChar, rDirectoryChar);
            if (rFirstPath.Length > 0 && rFirstPath[rFirstPath.Length - 1] == rDirectoryChar)
                rFirstPath = rFirstPath.Substring(0, rFirstPath.Length - 1);

            var rBuilder = new StringBuilder(rFirstPath);
            for (int nIndex = 1; nIndex < rPaths.Length; ++nIndex)
            {
                if (string.IsNullOrEmpty(rPaths[nIndex]))
                    continue;

                var rPath = rPaths[nIndex].Replace(rReplaceChar, rDirectoryChar);
                if (rPath[0] != rDirectoryChar)
                    rPath = rDirectoryChar + rPath;
                if (rPath[rPath.Length - 1] == rDirectoryChar)
                    rPath = rPath.Substring(0, rPath.Length - 1);
                rBuilder.Append(rPath);
            }

            return rBuilder.ToString();
        }

        public static string PathCombine(params string[] rPaths)
        {
            return PathCombine('/', rPaths);
        }

        public static string GetParentPath(string rPath)
        {
            return Path.GetDirectoryName(rPath).Replace("\\", "/");
        }

        public static bool PathIsSame(string rPath1, string rPath2)
        {
            string rFullPath1 = Path.GetFullPath(rPath1);
            string rFullPath2 = Path.GetFullPath(rPath2);
            return rFullPath1.Equals(rFullPath2);
        }

        public static GameObject CreateGameObject(string rName, params Type[] rComps)
        {
            GameObject rGo = new GameObject(rName, rComps);

            rGo.transform.localPosition = Vector3.zero;
            rGo.transform.localRotation = Quaternion.identity;
            rGo.transform.localScale = Vector3.one;

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rParentGo, string rName, params Type[] rComps)
        {
            GameObject rGo = new GameObject(rName, rComps);
            rGo.transform.parent = rParentGo.transform;

            rGo.transform.localPosition = Vector3.zero;
            rGo.transform.localRotation = Quaternion.identity;
            rGo.transform.localScale = Vector3.one;

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rTemplateGo)
        {
            GameObject rGo = GameObject.Instantiate(rTemplateGo);

            rGo.name = rTemplateGo.name;
            rGo.transform.localPosition = Vector3.zero;
            rGo.transform.localRotation = Quaternion.identity;
            rGo.transform.localScale = Vector3.one;

            return rGo;
        }

        public static GameObject CreateGameObject(GameObject rTemplateGo, GameObject rParentGo)
        {
            GameObject rGo = GameObject.Instantiate(rTemplateGo);
            rGo.transform.SetParent(rParentGo.transform);

            rGo.name = rTemplateGo.name;
            rGo.transform.localPosition = Vector3.zero;
            rGo.transform.localRotation = Quaternion.identity;
            rGo.transform.localScale = Vector3.one;

            return rGo;
        }

        public static void SafeDestroy(UnityEngine.Object rObj)
        {
            SafeDestroy(rObj, false);
        }
        public static T SafeInstantiate<T>(T rObj) where T : UnityEngine.Object
        {
            if (rObj)
            {
                return UnityEngine.Object.Instantiate<T>(rObj);
            }
            return default;
        }

        public static void SafeDestroy(UnityEngine.Object rObj, bool bAllowDestroyingAssets)
        {
            if (rObj)
                GameObject.DestroyImmediate(rObj, bAllowDestroyingAssets);
            rObj = null;
        }

        public static void SetLayer(GameObject rObj, string rLayerName, bool bIsIncludeChildren = false)
        {
            int nLayer = LayerMask.NameToLayer(rLayerName);
            SetLayer(rObj, nLayer, bIsIncludeChildren);
        }

        public static void SetLayer(GameObject rObj, int nLayer, bool bIsIncludeChildren)
        {
            if (rObj)
            {
                rObj.layer = nLayer;
                if (bIsIncludeChildren)
                {
                    int nChildNum = rObj.transform.childCount;
                    for (int i = 0; i < nChildNum; i++)
                    {
                        var rChildObj = rObj.transform.GetChild(i).gameObject;
                        SetLayer(rChildObj, nLayer, bIsIncludeChildren);
                    }
                }
            }
        }

        public static Transform FindObjectByName(Transform rTrans, string rName)
        {
            if (rTrans == null)
                return null;

            int nChildNum = rTrans.childCount;
            for (int i = 0; i < nChildNum; i++)
            {
                var rChild = rTrans.GetChild(i);
                if (rName.Equals(rChild.name))
                    return rChild;
                else
                {
                    var rChildNode = FindObjectByName(rChild, rName);
                    if (rChildNode != null)
                        return rChildNode;
                }
            }

            return null;
        }

        public static void WriteAllText(string rPath, string rContents)
        {
            string rDir = Path.GetDirectoryName(rPath);
            if (!Directory.Exists(rDir)) Directory.CreateDirectory(rDir);
            File.WriteAllText(rPath, rContents);
        }

        public static void WriteAllText(string rPath, string rContents, Encoding rEncoding)
        {
            string rDir = Path.GetDirectoryName(rPath);
            if (!Directory.Exists(rDir)) Directory.CreateDirectory(rDir);
            File.WriteAllText(rPath, rContents, rEncoding);
        }

        public static void WriteAllBytes(string rPath, byte[] rBytes)
        {
            string rDir = Path.GetDirectoryName(rPath);
            if (!Directory.Exists(rDir)) Directory.CreateDirectory(rDir);
            File.WriteAllBytes(rPath, rBytes);
        }

        public static string GetTransformPathByTrans(Transform rTargetTrans, Transform rTrans)
        {
            string rPath = "";
            GetTransformPathByTrans(rTargetTrans, rTrans, ref rPath);
            return rPath;
        }

        public static void GetTransformPathByTrans(Transform rTargetTrans, Transform rTrans, ref string rPath)
        {
            if (rTrans == null || rTrans.parent == null || rTargetTrans == rTrans) return;
            rPath = rTrans.name + (string.IsNullOrEmpty(rPath) ? rPath : "/" + rPath);
            GetTransformPathByTrans(rTargetTrans, rTrans.parent, ref rPath);
        }

        public static string GetTransformPath(Transform rTrans, bool bIgnoreParentCheck = false)
        {
            string rPath = "";
            GetTransformPath(rTrans, ref rPath, bIgnoreParentCheck);
            return rPath;
        }

        public static float SplitAndRound(float rNum, int nCount)
        {
            rNum = rNum * Mathf.Pow(10, nCount);
            return Mathf.Round(rNum) / Mathf.Pow(10, nCount);
        }

        public static string GetTransformPathWithSelf(Transform rTrans)
        {
            string rPath = "";
            GetTransformPath(rTrans, ref rPath);
            if (rTrans == null) return "";
            if (!string.IsNullOrEmpty(rPath))
                rPath = "/" + rPath;
            return rTrans.root.name + rPath;
        }

        public static void GetTransformPath(Transform rTrans, ref string rPath, bool bIgnoreParentCheck = false)
        {
            if (rTrans == null || (!bIgnoreParentCheck && rTrans.parent == null)) return;

            rPath = rTrans.name + (string.IsNullOrEmpty(rPath) ? rPath : "/" + rPath);
            GetTransformPath(rTrans.parent, ref rPath, bIgnoreParentCheck);
        }

        public static List<T> GetComponentsInChildrenBreak<T>(Component rComp, Type rBreakCompType) where T : Component
        {
            List<T> rCompResults = new List<T>();
            var rOriginComps = rComp.transform.GetComponents<T>();
            if (rOriginComps?.Length > 0)
            {
                rCompResults.AddRange(rOriginComps);
            }
            int nChildCount = rComp.transform.childCount;
            for (int i = 0; i < nChildCount; i++)
            {
                var rChildTrans = rComp.transform.GetChild(i);
                GetComponentsInChildrenBreak(ref rCompResults, rChildTrans, rBreakCompType);
            }
            return rCompResults;
        }

        private static void GetComponentsInChildrenBreak<T>(ref List<T> rComps, Transform rTrans, Type rBreakCompType) where T : Component
        {

            var rOriginComps = rTrans.GetComponents(typeof(T));
            if (rOriginComps?.Length > 0)
            {
                rComps.AddRange(rTrans.GetComponents<T>());
            }
            int nChildCount = rTrans.childCount;
            if (nChildCount == 0) return;
            for (int i = 0; i < nChildCount; i++)
            {
                var rExcludeComps = rTrans.GetComponents(rBreakCompType);
                if (rExcludeComps.Length > 0) continue;
                var rChildTrans = rTrans.GetChild(i);
                GetComponentsInChildrenBreak(ref rComps, rChildTrans, rBreakCompType);
            }
        }

        public static T[] GetComponentsInNearestParent<T>(Transform rTrans) where T : Component
        {
            T[] rComponent = new T[0];
            var rParentTrans = rTrans.transform.parent;
            GetComponentsInNearestParent(ref rComponent, rParentTrans);
            return rComponent;
        }

        private static void GetComponentsInNearestParent<T>(ref T[] rResultComp, Transform rTrans) where T : Component
        {
            var rOrgin = rTrans.GetComponents<T>();
            if (rOrgin.Length > 0)
            {
                rResultComp = rOrgin;
                return;
            }


            var rParentTrans = rTrans.parent;
            if (!rParentTrans)
            {
                return;
            }

            GetComponentsInNearestParent(ref rResultComp, rParentTrans);
        }

        public static Transform GetChildTransformByName(Transform rTransRoot, string rName)
        {
            var rAllTrans = rTransRoot.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < rAllTrans.Length; i++)
            {
                if (rAllTrans[i].name.Trim().Equals(rName))
                {
                    return rAllTrans[i];
                }
            }
            return null;
        }

        public static Color ToColor(int r, int g, int b, int a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        public static Color ToColor(Color32 rColor32)
        {
            return ToColor(rColor32.r, rColor32.g, rColor32.b, rColor32.a);
        }

        /// <summary>
        /// 颜色格式 #00FF00FF
        /// </summary>
        public static Color ToColor(string rColorStr)
        {
            if (rColorStr.Length != 9 || rColorStr[0] != '#')
            {

                return Color.white;
            }

            string rRStr = rColorStr.Substring(1, 2);
            int nR = Get0XValue(rRStr[0]) * 16 + Get0XValue(rRStr[1]);

            string rGStr = rColorStr.Substring(3, 2);
            int nG = Get0XValue(rGStr[0]) * 16 + Get0XValue(rGStr[1]);

            string rBStr = rColorStr.Substring(5, 2);
            int nB = Get0XValue(rBStr[0]) * 16 + Get0XValue(rBStr[1]);

            string rAStr = rColorStr.Substring(7, 2);
            int nA = Get0XValue(rAStr[0]) * 16 + Get0XValue(rAStr[1]);

            return ToColor(nR, nG, nB, nA);
        }

        public static int Get0XValue(char rChar)
        {
            if (rChar >= '0' && rChar <= '9')
            {
                return rChar - '0';
            }
            else if (rChar >= 'A' && rChar <= 'F')
            {
                return rChar - 'A' + 10;
            }
            return 0;
        }

        public static IPEndPoint ToIPEndPoint(string host, int port)
        {
            return new IPEndPoint(IPAddress.Parse(host), port);
        }

        public static IPEndPoint ToIPEndPoint(string address)
        {
            int index = address.LastIndexOf(':');
            string host = address.Substring(0, index);
            string p = address.Substring(index + 1);
            int port = int.Parse(p);
            return ToIPEndPoint(host, port);
        }

        public static ulong GenerateUIntGUID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static float FloatPreci4(float fValue)
        {
            return (Mathf.RoundToInt(fValue * 10000.0f)) / 10000.0f;
        }
        public static float FloatPreci2(float fValue)
        {
            return Mathf.FloorToInt(fValue * 100f) / 100f;
        }
        public static GameObject AddChild(this Transform rParent, GameObject rPrefabGo, string rLayerName = "UI")
        {
            if (rPrefabGo == null) return null;

            GameObject rTargetGo = GameObject.Instantiate(rPrefabGo);
            rTargetGo.name = rPrefabGo.name;
            if (rParent != null) rTargetGo.transform.SetParent(rParent, false);
            rTargetGo.transform.localScale = Vector3.one;
            rTargetGo.SetLayer(rLayerName);

            return rTargetGo;
        }

        public static GameObject AddChildNoScale(this Transform rParent, GameObject rPrefabGo, string rLayerName = "UI")
        {
            if (rParent == null || rPrefabGo == null) return null;

            GameObject rTargetGo = GameObject.Instantiate(rPrefabGo);
            rTargetGo.name = rPrefabGo.name;
            rTargetGo.transform.SetParent(rParent, false);
            rTargetGo.SetLayer(rLayerName);

            return rTargetGo;
        }

        public static GameObject AddChildCopyInUI(this RectTransform rParent, GameObject rPrefabGo, Camera rCamera, string rLayerName = "UI")
        {
            if (rParent == null || rPrefabGo == null) return null;

            GameObject rTargetGo = GameObject.Instantiate(rPrefabGo);
            rTargetGo.name = rPrefabGo.name;
            rTargetGo.transform.SetParent(rParent, false);
            rTargetGo.SetLayer(rLayerName);
            var rPrefabRect = (RectTransform)rPrefabGo.transform;
            var rLocalPos = UtilTool.ScreenPointToLocalPointInRectangle(rParent, rCamera.WorldToScreenPoint(rPrefabRect.position), rCamera);
            ((RectTransform)rTargetGo.transform).localPosition = rLocalPos;

            return rTargetGo;
        }
        public static void SetLayer(this GameObject rGo, string rLayerName)
        {
            if (rGo == null) return;

            SetLayer(rGo.transform, rLayerName);
        }
        public static void SetLayer(Transform rParent, string rLayerName)
        {
            if (rParent == null) return;

            rParent.gameObject.layer = LayerMask.NameToLayer(rLayerName);

            for (int i = 0; i < rParent.transform.childCount; i++)
            {
                var rTrans = rParent.transform.GetChild(i);
                SetLayer(rTrans, rLayerName);
            }
        }
        public static void SetActive_Effective(this GameObject rGo, bool bIsActive)
        {
            if (!rGo) return;
            var rTrans = rGo.GetComponent<RectTransform>();
            var rCanvasGroup = rGo.ReceiveComponent<CanvasGroup>();
            if (bIsActive)
            {
                rCanvasGroup.alpha = 1.0f;
                rTrans.anchoredPosition = new Vector2(0, 0);
            }
            else
            {
                rCanvasGroup.alpha = 0.0f;
                rTrans.anchoredPosition = new Vector2(0, 50000);
            }
            var guidance = rTrans.Find("Guidance");
            if (guidance)
            {
                guidance.gameObject.SetActive(bIsActive);
            }
        }
        public static Vector2 ScreenPointToLocalPointInRectangle(RectTransform rRect, Vector2 rScreenPoint, Camera rCam)
        {
            Vector2 rLocalPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rRect, rScreenPoint, rCam, out rLocalPos);
            return rLocalPos;
        }
        public static Vector3 MyScreenPointToWorldPoint(Vector3 rScreenPoint, Transform rTarget)
        {
            //1 得到物体在主相机的xx方向
            Vector3 rDir = (rTarget.position - Camera.main.transform.position);
            //2 计算投影 (计算单位向量上的法向量)
            Vector3 rnorVec = Vector3.Project(rDir, Camera.main.transform.forward);
            //返回世界空间
            return Camera.main.ViewportToWorldPoint(new Vector3(rScreenPoint.x / Screen.width, rScreenPoint.y / Screen.height, rnorVec.magnitude));
        }
        public static GameObject GetChild(this GameObject rGo, int nIndex)
        {
            if (rGo == null) return null;
            return rGo.transform.GetChild(nIndex).gameObject;
        }
        public static uint GetWeeHoursStamp(uint aStamp)
        {
            //TimeZone 时区偏移，单位：秒
            uint TimeZone = 8 * 3600;

            return aStamp - (aStamp + TimeZone) % 86400;
        }
        public static void DeleteChildren(this Transform rTrans, bool bNeedFilterDeactive = false)
        {
            if (rTrans == null) return;

            for (int i = rTrans.childCount - 1; i >= 0; i--)
            {
                Transform rChildTrans = rTrans.GetChild(i);

                if (bNeedFilterDeactive && !rChildTrans.gameObject.activeSelf)
                    continue;
                GameObject.DestroyImmediate(rChildTrans.gameObject);
            }
        }
        public static bool AnyOne<T>(this T[] rArray, Func<T, bool> rPredicate)
        {
            if (rPredicate == null)
            {
                return true;
            }

            int nCount = rArray.Length;
            for (int i = 0; i < nCount; i++)
            {
                if (rPredicate.Invoke(rArray[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static T Find<T>(this T[] rArray, Func<T, bool> rPredicate)
        {
            if (rPredicate == null)
            {
                return default;
            }

            int nCount = rArray.Length;
            for (int i = 0; i < nCount; i++)
            {
                if (rPredicate.Invoke(rArray[i]))
                {
                    return rArray[i];
                }
            }

            return default;
        }

        public static string TimeSpanFormat(long nTicks, string rFormat = null)
        {
            var rTimeSpan = new TimeSpan(nTicks);
            return TimeSpanFormat(rTimeSpan, rFormat);
        }
        public static string TimeSpanFormat(int nDays = 0, int nHours = 0, int nMinutes = 0, int nSeconds = 0, int nMilliseconds = 0, string rFormat = null)
        {
            var rTimeSpan = new TimeSpan(nDays, nHours, nMinutes, nSeconds, nMilliseconds);
            return TimeSpanFormat(rTimeSpan, rFormat);
        }

        public static long FormatStringToSeconds(string rTimeString, string rFormat)
        {
            var rFormatProvider = new System.Globalization.DateTimeFormatInfo();
            rFormatProvider.ShortTimePattern = rFormat;
            var rDateTime = Convert.ToDateTime(rTimeString, rFormatProvider);
            var rSeconds = (rDateTime.Ticks - TimeAssist.TimeZoneEpoch) / 10000000;
            return rSeconds;
        }
        public static string TimeSpanFormat(TimeSpan rTimeSpan, string rFormat = null)
        {
            if (rTimeSpan == null)
            {
                return "Invalid";
            }
            if (!string.IsNullOrEmpty(rFormat))
            {
                return rTimeSpan.ToString(rFormat);
            }
            if (rTimeSpan.Hours > 0)
            {
                return rTimeSpan.ToString(@"hh\:mm\:ss");
            }
            else
            {
                return rTimeSpan.ToString(@"mm\:ss");
            }
        }

        /// <summary>
        /// 秒转日期
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <param name="rFormat"></param>
        /// <returns></returns>
        public static string UTCDateTimeFormat(long nSeconds, string rFormat = "yyyy/MM/dd HH:mm:ss")
        {
            var rDateTime = new DateTime(TimeAssist.TimeZoneEpoch);
            rDateTime = rDateTime.AddSeconds(nSeconds);
            return rDateTime.ToString(rFormat);
        }

        /// <summary>
        /// 秒转小时(小时：分钟)
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <param name="rFormat"></param>
        /// <returns></returns>
        public static string SecToHourTimeStr(long nSeconds)
        {
            var nHour = nSeconds / 3600;
            var nMinite = nSeconds % 3600 / 60;
            return string.Format("{0:D2} : {1:D2}", nHour, nMinite);
        }

        /// <summary>
        /// 秒转小时(小时：分钟：秒)
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <param name="rFormat"></param>
        /// <returns></returns>
        public static string SecToHMSTimeStr(long nSeconds)
        {
            if (nSeconds <= 0) return "00:00:00";
            var nHour = nSeconds / 3600;
            var nMinite = nSeconds % 3600 / 60;
            var nSec = nSeconds % 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}", nHour, nMinite, nSec);
        }

        /// <summary>
        /// 秒转天
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <returns></returns>
        public static string SecondsToDayStr(long nSeconds)
        {
            var rDay = Mathf.CeilToInt((float)nSeconds / TimeAssist.SecondsPerDay).ToString();
            return rDay;

        }

        /// <summary>
        /// 秒转周
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <returns></returns>
        public static int SecondsToWeek(long nSeconds)
        {
            var nTicks = nSeconds * TimeSpan.TicksPerSecond;
            var rDate = new DateTime(nTicks);
            var nDay = rDate.DayOfYear;
            var nWeek = nDay / 7;
            var nRemainDay = nDay % 7;

            if (nRemainDay > 0)
            {
                nWeek++;
            }
            return nWeek;
        }

        public static long SecondsToEpochDay(long nSeconds)
        {
            var rTicks = nSeconds * TimeSpan.TicksPerSecond;
            var rDiffTicks = rTicks - TimeAssist.TimeZoneEpoch;
            var nDay = rDiffTicks / TimeSpan.TicksPerDay + rDiffTicks % TimeSpan.TicksPerDay;

            return nDay;
        }

        /// <summary>
        /// 两个时间点之间的周数
        /// </summary>
        /// <param name="nSeconds"></param>
        /// <returns></returns>
        public static int WeekThrough(long nBeginSeconds, long nEndSeconds)
        {
            var nBeginDay = UtilTool.SecondsToEpochDay(nBeginSeconds);
            var nEndDay = UtilTool.SecondsToEpochDay(nEndSeconds);
            var nBeginWeek = nBeginDay / 7 + nBeginDay % 7;
            var nEndWeek = nEndDay / 7 + nEndDay % 7;
            var nWeekThrough = nEndWeek - nBeginWeek + 1;

            return (int)nWeekThrough;
        }

        public static int CaculateDayCount(long nTime)
        {
            var rDateTimeTemp = new DateTime(TimeAssist.TimeZoneEpoch);
            var rDateTime = rDateTimeTemp.AddSeconds(nTime);
            var nCurDay = rDateTime.Day;
            rDateTime = rDateTimeTemp.AddSeconds(TimeAssist.ServerNowSeconds());
            var nCurDay2 = rDateTime.Day;
            return nCurDay2 - nCurDay;
        }
        /// <summary>
        /// HttpWebRequest发送文件
        /// </summary>
        /// <param name="rUrl">url</param>
        /// <param name="rFilePath">文件路径</param>
        /// <param name="rParamName">文件参数名</param>
        /// <param name="rContentType">contentType</param>
        /// <param name="rNameValueCollection">其余要附带的参数键值对</param>
        public static bool HttpUploadFile(string rUrl, string rFilePath, string rFileName, string rParamName = "file", string rContentType = "application/octet-stream", NameValueCollection rNameValueCollection = null, Dictionary<string, string> rRequestHeaderInfoDic = null)
        {
            WebResponse webResponse = null;
            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rUrl);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;
                if (rRequestHeaderInfoDic != null)
                {
                    foreach (var rPair in rRequestHeaderInfoDic)
                    {
                        request.Headers.Add(rPair.Key, rPair.Value);
                    }
                }
                Stream requestStream = request.GetRequestStream();
                string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                if (rNameValueCollection != null)
                {
                    foreach (string key in rNameValueCollection.Keys)
                    {
                        requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                        string formitem = string.Format(formdataTemplate, key, rNameValueCollection[key]);
                        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                        requestStream.Write(formitembytes, 0, formitembytes.Length);
                    }
                }
                requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", rParamName, rFileName, rContentType);
                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
                requestStream.Write(headerbytes, 0, headerbytes.Length);

                FileStream fileStream = new FileStream(rFilePath, FileMode.Open, FileAccess.Read);
                Debug.Log($"待上传文件大小 :{fileStream.Length}");
                byte[] buffer = new byte[4096];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(trailer, 0, trailer.Length);
                requestStream.Close();
                webResponse = request.GetResponse();
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                return true;
            }
            catch (Exception rException)
            {
                if (webResponse != null)
                {
                    webResponse.Close();
                }
                Debug.Log($"上传文件失败:{rUrl} Error:{rException.Message} StackTrace:{rException.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// 添加组件到所有子对象
        /// </summary>
        /// <param name="component"></param>
        /// <param name="includeInactive"></param>
        public static void AddComponentInChilds<T>(Component component, Boolean includeInactive = true) where T : Component
        {
            if (component != null)
            {
                var transforms = component.GetComponentsInChildren<Transform>(includeInactive);
                for (var i = 0; i < transforms.Length; i++)
                {
                    _ = transforms[i].gameObject.ReceiveComponent<T>();
                }
            }
        }
        /// <summary>
        /// 创建子对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="childPath"></param>
        /// <returns></returns>
        public static GameObject CreateChild(GameObject go, String childPath)
        {
            if (go != null && !String.IsNullOrEmpty(childPath))
            {
                var childNames = childPath.Split('/');
                if (childNames == null || childNames.Length == 0)
                {
                    return default;
                }
                var cur = go.transform;
                for (var i = 0; i < childNames.Length; i++)
                {
                    var child = new GameObject(childNames[i]);
                    child.transform.SetParent(cur);
                    cur = child.transform;
                }
                return cur.gameObject;
            }
            return default;
        }
        /// <summary>
        /// 获取启动参数
        /// </summary>
        /// <param name="rArgName"></param>
        /// <returns></returns>
        public static string GetStartCommandLineArg(string rArgName)
        {
            var rArg = string.Empty;
            var rCommandLineArgs = Environment.GetCommandLineArgs();
            if (rCommandLineArgs != null && rCommandLineArgs.Length > 1)
            {
                for (int i = 0; i < rCommandLineArgs.Length - 1; i++)
                {
                    if (rCommandLineArgs[i].Equals(rArgName))
                    {
                        rArg = rCommandLineArgs[i + 1];
                    }
                }
            }
            return rArg;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="rFilePath"></param>
        /// <param name="bIsDellOrigin"></param>
        /// <returns></returns>
        public static string GZipFile(string rFilePath, bool bIsDellOrigin = false, string rGZipFilePath = "")
        {
            try
            {
                var rGZipFileBuffer = new byte[1024 * 1024];
                if (string.IsNullOrEmpty(rGZipFilePath))
                {
                    rGZipFilePath = rFilePath + ".gz";
                }
                //1.读取源文件（读取到源文件是未压缩的）
                using (FileStream fsRead = new FileStream(rFilePath, FileMode.Open, FileAccess.Read))
                {
                    //要将读取到的内容压缩，就是要写入到一个新的文件；所以创建一个新的写入文件的文件流
                    using (FileStream fsWrite = new FileStream(rGZipFilePath, FileMode.Create, FileAccess.Write))
                    {
                        //因为在写入的时候要压缩后写入，所以需要创建压缩流来写入(因此在压缩写入前需要先创建写入流)
                        //压缩的时候就是要将压缩好的数据写入到指定流中，通过fsWrite写入到新的路径下
                        using (GZipStream zip = new GZipStream(fsWrite, CompressionMode.Compress))
                        {
                            //循环读取，每次从fsRead读取一部分，压缩就写入一部分
                            //读取流每次读取buffer数组的大小
                            int r = fsRead.Read(rGZipFileBuffer, 0, rGZipFileBuffer.Length);
                            while (r > 0)
                            {
                                //写入，用压缩流来写入，这样写入的才是压缩后的数据
                                //压缩流要从 读取流中读取到的文件数据的数组 中调用Write方法将压缩字节写入基础流(fsWrite)
                                zip.Write(rGZipFileBuffer, 0, r); //从读取到内容的数组中读取0到实际读取到的字节数进行压缩
                                                                  //继续从未压缩的文件中读取数据
                                r = fsRead.Read(rGZipFileBuffer, 0, rGZipFileBuffer.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log($"压缩文件失败 FilePath:{rFilePath}");
                Debug.Log($"Error:{e.Message}\nStackTrace:{e.StackTrace}");
            }
            //压缩完成后删除原文件
            if (bIsDellOrigin)
            {
                File.Delete(rFilePath);
            }
            return rGZipFilePath;
        }
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="rData"></param>
        /// <returns></returns>
        public static byte[] GzipCompress(byte[] rData)
        {
            var rGZipFileBuffer = new byte[1024 * 1024];
            using (var fsRead = new MemoryStream(rData))
            {
                using (var fsWrite = new MemoryStream())
                {
                    using (GZipStream zip = new GZipStream(fsWrite, CompressionMode.Compress))
                    {
                        int r = fsRead.Read(rGZipFileBuffer, 0, rGZipFileBuffer.Length);
                        while (r > 0)
                        {
                            zip.Write(rGZipFileBuffer, 0, r);
                            r = fsRead.Read(rGZipFileBuffer, 0, rGZipFileBuffer.Length);
                        }
                    }
                    return fsWrite.ToArray();
                }
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="rCompressData"></param>
        /// <returns></returns>
        public static byte[] GZipDecompress(byte[] rCompressData)
        {
            var rGZipDecompressBuffer = new byte[1024 * 1024];
            using (MemoryStream rMemoryStream = new MemoryStream(rCompressData))
            {
                using (GZipStream rGZipStream = new GZipStream(rMemoryStream, CompressionMode.Decompress))
                {
                    var rDecompressBuffer = PoolByteBuffer.Alloc();
                    while (true)
                    {
                        var nRead = rGZipStream.Read(rGZipDecompressBuffer, 0, rGZipDecompressBuffer.Length);
                        if (nRead <= 0)
                        {
                            break;
                        }
                        rDecompressBuffer.Append(rGZipDecompressBuffer, 0, nRead);
                    }
                    var rDecompressData = rDecompressBuffer.ToArray();
                    rDecompressBuffer.Free();
                    return rDecompressData;
                }
            }
        }
        /// <summary>
        /// 求向量夹角
        /// </summary>
        /// <param name="rFrom"></param>
        /// <param name="rTo"></param>
        /// <returns></returns>
        public static float VectorAngle(Vector2 rFrom, Vector2 rTo)
        {
            float rAngle;
            Vector3 rCross = Vector3.Cross(rFrom, rTo);
            rAngle = Vector2.Angle(rFrom, rTo);
            return rCross.z > 0 ? -rAngle : rAngle;
        }
        /// <summary>
        /// 世界坐标转 UI坐标
        /// </summary>
        /// <param name="rParentRect"></param>
        /// <param name="rScreenPos"></param>
        /// <param name="rUICamrea"></param>
        /// <returns></returns>
        public static Vector3 WorldPosToUI(RectTransform rParentRect, Vector2 rScreenPos, Camera rUICamrea)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rParentRect, rScreenPos, rUICamrea, out var rResPos);
            return rResPos;
        }

        public static Vector2 IntArrayToVector2(int[] rArray)
        {
            return new Vector2(rArray[0], rArray[1]);
        }

        /// <summary>
        /// 加载文件数据
        /// </summary>
        /// <param name="rPath">路径</param>
        /// <returns></returns>
        public static byte[] LoadFileData(string rPath)
        {
            if (!File.Exists(rPath))
                return null;
            FileStream rFileStream = new FileStream(rPath, FileMode.Open);
            byte[] rData = new byte[rFileStream.Length];
            rFileStream.Read(rData, 0, rData.Length);
            rFileStream.Close();
            return rData;
        }

        public static bool NetworkIsWIFI()
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }

        public static bool IsNumber(string rString)
        {
            if (string.IsNullOrWhiteSpace(rString)) return false;
            Regex rx = new Regex("^[0-9]*$");
            bool RET = rx.IsMatch(rString);
            return rx.IsMatch(rString);
        }
    }
}