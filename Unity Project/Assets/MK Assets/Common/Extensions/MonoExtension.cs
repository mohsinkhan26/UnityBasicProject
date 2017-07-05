using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MK.Common.Extensions
{
    public static class MonoExtension
    {
        public static T[] AddItemToArray<T>(this T[] original, T itemToAdd)
        {
            T[] finalArray = new T[original.Length + 1];
            for (int i = 0; i < original.Length; ++i)
            {
                finalArray[i] = original[i];
            }
            finalArray[finalArray.Length - 1] = itemToAdd;
            Debug.Log("Original: " + original.Length + "   New: " + finalArray.Length);
            return finalArray;
        }

        /// <summary>
        /// Execute action code only on certain types
        /// <see cref="http://www.extensionmethod.net/csharp/object/iftype"/>
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="action">Action.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void IfType<T>(this object item, Action<T> action) where T : class
        {
            if (item is T)
            {
                action(item as T);
            }
        }

        /// <summary>
        /// allows an action to be taken on an object if it is castable as the given type, with no return value.
        /// if the target does not match the type, does nothing
        /// <see cref="http://www.extensionmethod.net/csharp/object/ifis-t"/>
        /// </summary>
        public static void IfIs<T>(this object target, Action<T> method)
        where T : class
        {
            var cast = target as T;
            if (cast != null)
            {
                method(cast);
            }
        }

        /// <summary>
        /// allows an action to be taken on an object if it is castable as the given type, with a return value.
        /// if the target does not match the type, returns default(T)
        /// <see cref="http://www.extensionmethod.net/csharp/object/ifis-t"/>
        /// </summary>
        public static TResult IfIs<T, TResult>(this object target, Func<T, TResult> method)
        where T : class
        {
            var cast = target as T;
            if (cast != null)
            {
                return method(cast);
            }
            else
            {
                return default(TResult);
            }
        }

        /// <summary>
        /// Determines whether a IEnumerable<T> contains a specific value.
        /// <see cref="http://www.extensionmethod.net/csharp/object/in"/>
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="values">Values.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool In<T>(this T value, IEnumerable<T> values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            return values.Contains(value);
        }

        /// <summary>
        /// Determines whether a collection is null or has no elements without having to enumerate the entire collection to get a count.  Uses LINQ.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="items">The items.</param>
        /// <returns>
        /// <c>true</c> if this list is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IList<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Swaps the list entries of defined indices with in single list.
        /// </summary>
        /// <returns>The list entries.</returns>
        /// <param name="list">List.</param>
        /// <param name="indexA">Index a.</param>
        /// <param name="indexB">Index b.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static IList<T> SwapListEntries<T>(this IList<T> list, int indexA, int indexB)
        {
            if (list.Count > indexA && list.Count > indexB && indexA > -1 && indexB > -1)
            {
                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }
            return list;
        }


        #region Invoke extension method

        /* Sample usage is
    	 * this.InvokeExtension(5f, 
	  						() => { Debug.Log("After delay...!"); });
    	 * this.InvokeExtension(5f, 
		                    () => { Debug.Log("After delay...!"); },
							() => { Debug.Log("This is callBack"); });
    	 */
        /// <summary>
        /// Invoke extension method.
        /// </summary>
        /// <param name="mono">Mono.</param>
        /// <param name="delay">Delay.</param>
        /// <param name="action">Action.</param>
        /// <param name="callBack">Call back.</param>
        public static void InvokeExtension(this MonoBehaviour mono, float delay, Action action, Action callBack = null)
        {
            mono.StartCoroutine(mono.InvokeExtensionCoroutine(delay, action, callBack));
        }

        private static IEnumerator InvokeExtensionCoroutine(this MonoBehaviour mono, float delay, Action action, Action callBack = null)
        {
            yield return new WaitForSeconds(delay);

            action();

            if (callBack != null)
                callBack();
        }

        #endregion Invoke extension method

        #region Generic Parameterized Invoke extension method

        /* Sample usage is
    	 * this.InvokeExtension(5f, 
		                     (parameterValue) => { Debug.Log("After delay: " + parameterValue); }, "This is the parameter");
    	 * this.InvokeExtension(5f, 
		                     (parameterValue) => { Debug.Log("After delay: " + parameterValue); }, "This is the parameter", 
							 () => { Debug.Log("This is callBack"); });
    	 * this.InvokeExtension(5f, 
		                     (parameterValue) => { Debug.Log("After delay: " + parameterValue); }, 786, 
							 () => { Debug.Log("This is callBack"); });
    	 * this.InvokeExtension<int>(5f, 
		                     (parameterValue) => { Debug.Log("After delay: " + parameterValue); }, 786, 
							 () => { Debug.Log("This is callBack"); });
    	 */
        /// <summary>
        /// Generic Parameterized Invoke extension method.
        /// </summary>
        /// <param name="mono">Mono.</param>
        /// <param name="delay">Delay.</param>
        /// <param name="action">Action.</param>
        /// <param name="actionParameter">Action parameter.</param>
        /// <param name="callBack">Call back.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void InvokeExtension<T>(this MonoBehaviour mono, float delay, Action<T> action, T actionParameter, Action callBack = null)
        {
            mono.StartCoroutine(mono.InvokeExtensionCoroutine(delay, action, actionParameter, callBack));
        }

        private static IEnumerator InvokeExtensionCoroutine<T>(this MonoBehaviour mono, float delay, Action<T> action, T actionParameter, Action callBack = null)
        {
            yield return new WaitForSeconds(delay);

            action(actionParameter);

            if (callBack != null)
                callBack();
        }

        #endregion Generic Parameterized Invoke extension method

        #region Generic Parameterized with Parameterized Callback Invoke extension method

        /* Sample usage is
    	 * this.InvokeExtension(5f,
		                    (actionParameterValue) => { Debug.Log("After delay: " + actionParameterValue); }, 786, 
							(callBackParameterValue) => { Debug.Log("This is callBack: " + callBackParameterValue); }, 564478);
    	 * this.InvokeExtension(5f,
		                    (actionParameterValue) => { Debug.Log("After delay: " + actionParameterValue); }, "Hello World", 
							(callBackParameterValue) => { Debug.Log("This is callBack: " + callBackParameterValue); }, "New Year");
    	 * this.InvokeExtension<string>(5f,
		                    (actionParameterValue) => { Debug.Log("After delay: " + actionParameterValue); }, "Hello World", 
							(callBackParameterValue) => { Debug.Log("This is callBack: " + callBackParameterValue); }, "New Year");
    	 */
        /// <summary>
        /// Generic Parameterized with Parameterized Callback Invoke extension method.
        /// </summary>
        /// <param name="mono">Mono.</param>
        /// <param name="delay">Delay.</param>
        /// <param name="action">Action.</param>
        /// <param name="actionParameter">Action parameter.</param>
        /// <param name="callBack">Call back.</param>
        /// <param name="callBackParameter">Call back parameter.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void InvokeExtension<T>(this MonoBehaviour mono, float delay, Action<T> action, T actionParameter, Action<T> callBack, T callBackParameter)
        {
            mono.StartCoroutine(mono.InvokeExtensionCoroutine(delay, action, actionParameter, callBack, callBackParameter));
        }

        private static IEnumerator InvokeExtensionCoroutine<T>(this MonoBehaviour mono, float delay, Action<T> action, T actionParameter, Action<T> callBack, T callBackParameter)
        {
            yield return new WaitForSeconds(delay);

            action(actionParameter);

            callBack(callBackParameter);
        }

        #endregion Generic Parameterized with Parameterized Callback Invoke extension method


        #region UPDATE EXTENSION

        public static void UpdateExtension(this MonoBehaviour mono, Action updateTask, float startDelay = 0f, float updateDelay = 0)
        {
            mono.StartCoroutine(mono.UpdateExtensionRoutine(updateTask, startDelay, updateDelay));
        }

        private static IEnumerator UpdateExtensionRoutine(this MonoBehaviour ienum, Action updateTask, float startDelay = 0f, float updateDelay = 0)
        {
            if (startDelay > 0f)
            {
                yield return new WaitForSeconds(startDelay);
            }

            if (updateDelay > 0f)
            {
                while (true)
                {
                    yield return new WaitForSeconds(updateDelay);
                    updateTask();
                }
            }
            else
            {
                while (true)
                {
                    yield return new WaitForEndOfFrame();
                    updateTask();
                }
            }
        }

        #endregion UPDATE EXTENSION

        #region ACTIVE ALPHA ANIMATION

        public static void SetActiveAnimated(this GameObject gObject, bool yesNo)
        {
            MonoBehaviour mono = gObject.GetComponent<MonoBehaviour>();
            if (mono == null)
                mono = gObject.AddComponent<MonoBehaviour>();

            mono.StopCoroutine("AlphaFade");

            if (mono == null)
                return;

            if (!gObject.activeInHierarchy && !yesNo)
                return;

            if (yesNo && !gObject.activeInHierarchy)
                gObject.SetActive(true);

            mono.StartCoroutine(AlphaFade(mono, yesNo));
        }

        private static IEnumerator AlphaFade(this MonoBehaviour mono, bool setActiveTrue)
        {
            Material meshMaterial = mono.GetComponent<MeshRenderer>().material;
            Color meshColor = meshMaterial.color;

            if (setActiveTrue)
            {
                mono.gameObject.SetActive(true);

                while (meshColor.a < 1)
                {
                    meshColor = new Color(meshColor.r, meshColor.g, meshColor.b, meshColor.a += 0.05f);
                    meshMaterial.color = meshColor;
                    Debug.Log("meshColor > " + meshColor);
                    yield return new WaitForSeconds(0.05f);
                }
            }
            else
            {
                while (meshColor.a > 0)
                {
                    meshColor = new Color(meshColor.r, meshColor.g, meshColor.b, meshColor.a -= 0.05f);
                    meshMaterial.color = meshColor;
                    yield return new WaitForSeconds(0.05f);
                }
                mono.gameObject.SetActive(false);
            }
        }

        #endregion ACTIVE ALPHA ANIMATION
    }
}
