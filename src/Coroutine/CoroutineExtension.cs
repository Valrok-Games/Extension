using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valrok.Extension.Coroutine
{
    public static class CoroutineExtension
    {
        /// <summary>
        /// Executes the action after the set time
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator ExecuteInTime(WaitForSeconds time, System.Action action = null)
        {
            yield return time;
            action?.Invoke();
        }

        /// <summary>
        /// Fades in an Image
        /// </summary>
        /// <param name="spriteRenderer"></param>
        /// <param name="updateInterval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator FadeIn(Image image, WaitForSeconds updateInterval, float alphaUpdateValue = 0.02f, System.Action action = null)
        {
            float alphaVal = image.color.a;
            Color tmp = image.color;

            while (image.color.a < 1)
            {
                alphaVal += alphaUpdateValue;
                tmp.a = alphaVal;
                image.color = tmp;

                yield return updateInterval;
            }

            action?.Invoke();
        }

        /// <summary>
        /// Fades out an Image
        /// </summary>
        /// <param name="spriteRenderer"></param>
        /// <param name="updateInterval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator FadeOut(Image image, WaitForSeconds updateInterval, float alphaUpdateValue = 0.02f, System.Action action = null)
        {
            float alphaVal = image.color.a;
            Color tmp = image.color;

            while (image.color.a > 0)
            {
                alphaVal -= alphaUpdateValue;
                tmp.a = alphaVal;
                image.color = tmp;

                yield return updateInterval;
            }

            action?.Invoke();
        }
    }
}
