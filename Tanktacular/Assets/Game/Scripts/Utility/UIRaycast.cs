using System;

namespace DLS.Utility
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine.EventSystems;
    /// <summary>
    /// This is used to Raycast to the UI interface. Methods used to return Components and Gameobjects and other types.
    /// </summary>
    public static class UIRaycast
    {
//        /// <summary>
//        /// Returns a list of RaycastResult that contains all the raycast data information at the targetposition.
//        /// </summary>
//        /// <returns></returns>
//        public static List<RaycastResult> CastAll(Vector3 targetposition)
//        {
//            PointerEventData pointerData = new PointerEventData(EventSystem.current);
//            pointerData.position = targetposition;
//            List<RaycastResult> Result = new List<RaycastResult>();
//            EventSystem.current.RaycastAll(pointerData, Result);
//            return Result;
//        }

        /// <summary>
        /// Returns a list of RaycastResult that contains all the raycast data information at the targetposition and by default will not ignore selected gameobjects and their parents.
        /// </summary>
        /// <returns></returns>
        public static List<RaycastResult> CastAll(Vector3 targetposition, bool ignoreChildren, params GameObject[] ignoredGameObjects)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = targetposition;
            List<RaycastResult> Result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, Result);



            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].gameObject != null)
                {
                    if (Result[i].gameObject == ignoredGameObjects.Any(x => x))
                    {
                        Result.RemoveAt(i);
                    }

                    if (ignoreChildren)
                    {
                        for (int j = 0; j < ignoredGameObjects.Length; j++)
                        {
                            if (ignoredGameObjects[j].transform.childCount > 0)
                            {
                                if (Result[i].gameObject.transform.IsChildOf(ignoredGameObjects[j].transform))
                                {
                                    Result.RemoveAt(i);
                                }
                            }
                        }
                    }
                }
            }
            return Result;

        }

        //                    if (Result[i].gameObject == ignoredGameObjects.Any(x => x))
        //                    {
        //                        Result.RemoveAt(i);
        //                        if (ignoreChildren)
        //                        {
        //                            var parents = ignoredGameObjects
        //                        }
        //                    }
        //                    if (ignoreChildren)
        //                    {
        //Debug.Log(i + ": ingore parents? " + ignoreChildren);
        //                        if (Result[i].gameObject == ignoredGameObjects.Any(x => x.gameObject.transform.parent) && ignoreChildren)
        //                        {
        //                            Debug.Log("Removed Because of Parent");
        //                            Result.RemoveAt(i);
        //                        }
        //                    }




        #region GameObject Casting
        /// <summary>
        /// Returns the first Gameobject using the targetposition as reference point for where to search from.
        /// </summary>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static GameObject GetFirstGameobject(Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            return Result[0].gameObject;
        }

        /// <summary>
        /// Returns the first Gameobject using the sourceGameObject to exclude children in results and as a reference point for where to search from.
        /// </summary>
        /// <param name="sourceGameObject"></param>
        /// <returns></returns>
        public static GameObject GetFirstGameobject(GameObject sourceGameObject)
        {
            List<RaycastResult> Result = CastAll(sourceGameObject.transform.position, true);
            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].gameObject.transform.parent.gameObject == sourceGameObject)
                {
                    continue;
                }

                return Result[i].gameObject;
            }

            return null;
        }

        /// <summary>
        /// Returns the first Gameobject using the sourcegameobject as object children to exclude and the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="sourceGameObject"></param>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static GameObject GetFirstGameobject(GameObject sourceGameObject, Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].gameObject.transform.parent.gameObject == sourceGameObject)
                {
                    continue;
                }

                return Result[i].gameObject;
            }

            return null;
        }

        /// <summary>
        /// Returns a list of Gameobjects using the targetposition as reference point for where to search from.
        /// </summary>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static List<GameObject> GetListGameobjects(Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            List<GameObject> gameobjects = new List<GameObject>();

            for (int i = 0; i < Result.Count; i++)
            {
                gameobjects.Add(Result[i].gameObject);
            }
            return gameobjects;
        }

        /// <summary>
        /// Returns a list of Gameobjects using the sourceGameObject to exclude children in results and as a reference point for where to search from.
        /// </summary>
        /// <param name="sourceGameObject"></param>
        /// <returns></returns>
        public static List<GameObject> GetListGameobjects(GameObject sourceGameObject)
        {
            List<RaycastResult> Result = CastAll(sourceGameObject.transform.position, true);
            List<GameObject> gameobjects = new List<GameObject>();

            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].gameObject.transform.parent.gameObject == sourceGameObject)
                {
                    continue;
                }
                gameobjects.Add(Result[i].gameObject);

            }
            return gameobjects;
        }

        /// <summary>
        /// Returns a list of Gameobjects using the sourcegameobject as object children to exclude and the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="sourceGameObject"></param>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static List<GameObject> GetListGameobjects(GameObject sourceGameObject, Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            List<GameObject> gameobjects = new List<GameObject>();

            for (int i = 0; i < Result.Count; i++)
            {
                if (Result[i].gameObject.transform.parent.gameObject == sourceGameObject)
                {
                    continue;
                }
                gameobjects.Add(Result[i].gameObject);
            }
            return gameobjects;
        }


        #endregion

        #region Component Casting

        /// <summary>
        /// Returns the first component of the specified type using the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="sourceGameObject"></param>
        /// <returns></returns>
        public static Component GetFirstComponent(System.Type _type, Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var resultlist = Result.Find(x => x.gameObject.GetComponent(_type));
                return resultlist.gameObject.GetComponent(_type);
            }

            return null;
        }

        /// <summary>
        /// Returns the first component of the specified type using the sourceGameObject to exclude children in results and as a reference point for where to search from.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="sourceGameObject"></param>
        /// <returns></returns>
        public static Component GetFirstComponent(System.Type _type, GameObject sourceGameObject)
        {
            List<RaycastResult> Result = CastAll(sourceGameObject.transform.position, true);
            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_type));

                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i].gameObject.transform.parent.gameObject == sourceGameObject)
                    {
                        continue;
                    }

                    return resultlist[i].gameObject.GetComponent(_type);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the first component of the specified type using the sourceGameObject as object children to exclude and the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="sourceGameObject"></param>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static Component GetFirstComponent(System.Type _type, GameObject sourceGameObject, Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_type));

                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i].gameObject.transform.parent.gameObject == sourceGameObject)
                    {
                        continue;
                    }

                    return resultlist[i].gameObject.GetComponent(_type);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a list of components of the specified type using the sourceGameObject to exclude children in results and as a reference point for where to search from.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_sourceGameobject"></param>
        /// <returns></returns>
        public static List<Component> GetListComponents(GameObject _sourceGameobject, System.Type _type)
        {
            List<RaycastResult> Result = CastAll(_sourceGameobject.transform.position, true);
            List<Component> componentlist = new List<Component>();
            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_type));

                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i].gameObject.transform.parent.gameObject == _sourceGameobject)
                    {
                        continue;
                    }
                    componentlist.Add(resultlist[i].gameObject.GetComponent(_type));
                }
                return componentlist;
            }

            return null;
        }

        /// <summary>
        /// Returns a list of components for all of the type/s provided using the sourceGameObject to exclude children in results and as a reference point for where to search from.
        /// </summary>
        /// <param name="_types"></param>
        /// <param name="_sourceGameobject"></param>
        /// <returns></returns>
        public static List<Component> GetListComponents(GameObject _sourceGameobject, params System.Type[] _types)
        {
            List<RaycastResult> Result = CastAll(_sourceGameobject.transform.position, true);
            List<Component> componentlist = new List<Component>();
            for (int i = 0; i < _types.Length; i++)
            {
                if (Result.Any(x => x.gameObject.GetComponent(_types[i])))
                {
                    var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_types[i]));
                    for (int j = 0; j < resultlist.Count; j++)
                    {
                        if (resultlist[j].gameObject.transform.parent.gameObject == _sourceGameobject)
                        {
                            continue;
                        }
                        componentlist.Add(resultlist[j].gameObject.GetComponent(_types[i]));
                    }
                }
            }
            return componentlist;
        }

        /// <summary>
        /// Returns a list of components of the specified type using the sourceGameObject as object children to exclude and the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_sourceGameobject"></param>
        /// <param name="targetposition"></param>
        /// <returns></returns>
        public static List<Component> GetListComponents(System.Type _type, GameObject _sourceGameobject, Vector3 targetposition)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            List<Component> componentlist = new List<Component>();
            if (Result.Any(x => x.gameObject.GetComponent(_type)))
            {
                var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_type));

                for (int i = 0; i < resultlist.Count; i++)
                {
                    if (resultlist[i].gameObject.transform.parent.gameObject == _sourceGameobject)
                    {
                        continue;
                    }
                    componentlist.Add(resultlist[i].gameObject.GetComponent(_type));
                }
                return componentlist;
            }

            return null;
        }


        /// <summary>
        /// Returns a list of components of the specified type/s using the sourceGameObject as object children to exclude and the targetposition as a reference point for where to search from.
        /// </summary>
        /// <param name="_types"></param>
        /// <param name="_sourceGameobject"></param>
        /// <returns></returns>
        public static List<Component> GetListComponents(GameObject _sourceGameobject, Vector3 targetposition, params System.Type[] _types)
        {
            List<RaycastResult> Result = CastAll(targetposition, true);
            List<Component> componentlist = new List<Component>();
            for (int i = 0; i < _types.Length; i++)
            {
                if (Result.Any(x => x.gameObject.GetComponent(_types[i])))
                {
                    var resultlist = Result.FindAll(x => x.gameObject.GetComponent(_types[i]));
                    for (int j = 0; j < resultlist.Count; j++)
                    {
                        if (resultlist[j].gameObject.transform.parent.gameObject == _sourceGameobject)
                        {
                            continue;
                        }
                        componentlist.Add(resultlist[j].gameObject.GetComponent(_types[i]));
                    }
                }
            }
            return componentlist;
        }

        #endregion 

    }
}


