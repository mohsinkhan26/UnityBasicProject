using System;
using UnityEngine;

namespace MK.Common
{
    public static class GameLogger
    {
        public static string StackTraceColor = Colors.yellow.ToString();

        private static bool isLogging = true;

        public static bool IsLogging
        {
            get
            {
                return isLogging;
            }
        }

        /// <summary>
        /// Clears the console programmatically at runtime.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ClearConsole<T>(this T sender) where T : class
        {
            // This simply does "LogEntries.Clear()" the long way:
            var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            clearMethod.Invoke(null, null);
        }


        #region LOG

        public static void Log<T>(this T sender, string message) where T : class
        {
            if (IsLogging)
            {
                Debug.Log(message);
            }
        }

        public static void Log<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.Log(string.Format(message, args));
        }

        /// <summary>
        /// Logs the String with TimeStamp.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args));
        }

        /// <summary>
        /// Logs the String with TimeStamp and Stack Traces.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogTT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        /// <summary>
        /// Logs the sender class name.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void LogSender<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.Log("[" + sender.ToString() + "]: " + string.Format(message, args));
        }

        public static void LogAll<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: " + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogWithColor<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.Log("[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogItalicsColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <i><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></i>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogBoldColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.Log(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <b><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></b>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        #endregion LOG

        #region LOG ERROR

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">Message.</param>
        public static void LogError<T>(this T sender, string message) where T : class
        {
            if (IsLogging)
            {
                Debug.LogError(message);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogError<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogError(string.Format(message, args));
        }

        /// <summary>
        /// Logs the error with TimeStamp.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogErrorT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args));
        }

        /// <summary>
        /// Logs the error with TimeStamp and Stack Traces.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogErrorTT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        /// <summary>
        /// Logs the error sender class name.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void LogErrorSender<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogError("[" + sender.ToString() + "]: " + string.Format(message, args));
        }

        public static void LogErrorAll<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: " + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogErrorWithColor<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogError("[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogErrorColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogErrorItalicsColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <i><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></i>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogErrorBoldColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogError(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <b><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></b>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        #endregion LOG ERROR

        #region LOG EXCEPTION

        public static void LogException<T>(this T sender, Exception exception) where T : class
        {
            if (IsLogging)
            {
                Debug.LogException(exception);
            }
        }

        public static void LogException<T>(this T sender, string message) where T : class
        {
            sender.LogException(new Exception(message));
        }

        public static void LogException<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogException(string.Format(message, args));
        }

        /// <summary>
        /// Logs the exception with TimeStamp.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogExceptionT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args));
        }

        /// <summary>
        /// Logs the exception with TimeStamp and Stack Traces.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogExceptionTT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        /// <summary>
        /// Logs the exception sender class name.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void LogExceptionSender<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogException("[" + sender.ToString() + "]: " + string.Format(message, args));
        }

        public static void LogExceptionAll<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: " + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogExceptionWithColor<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogException("[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogExceptionColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogExceptionItalicsColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <i><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></i>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogExceptionBoldColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogException(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <b><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></b>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        #endregion LOG EXCEPTION

        #region LOG WARNING

        public static void LogWarning<T>(this T sender, string message) where T : class
        {
            if (IsLogging)
            {
                Debug.LogWarning(message);
            }
        }

        public static void LogWarning<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogWarning(string.Format(message, args));
        }

        /// <summary>
        /// Logs the warning with TimeStamp.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogWarningT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args));
        }

        /// <summary>
        /// Logs the warning with TimeStamp and Stack Traces.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        public static void LogWarningTT<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff] ") + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        /// <summary>
        /// Logs the warning sender class name.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        /// <param name="args">Arguments.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void LogWarningSender<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogWarning("[" + sender.ToString() + "]: " + string.Format(message, args));
        }

        public static void LogWarningAll<T>(this T sender, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: " + string.Format(message, args) +
                "\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogWarningWithColor<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogWarning("[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogWarningColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogWarningItalicsColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <i><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></i>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        public static void LogWarningBoldColorAll<T>(this T sender, Colors color, string message, params object[] args) where T : class
        {
            sender.LogWarning(DateTime.Now.ToString("[hh:mm:ss.fff]") + "[" + sender.ToString() + "]: <b><color=" + color.ToString() + ">" + string.Format(message, args) +
                "</color></b>\n<color=" + StackTraceColor + ">Stack Traces: " + StackTraceUtility.ExtractStackTrace() + "</color>\n");
        }

        #endregion LOG WARNING

        #region LOG GENERAL

        public static void LogGeneral<T>(this T sender, LogType type, string message) where T : class
        {
            switch (type)
            {
                case LogType.Log:
                    sender.Log(message);
                    break;
                case LogType.Error:
                    sender.LogError(message);
                    break;
                case LogType.Warning:
                    sender.LogWarning(message);
                    break;
                case LogType.Exception:
                    sender.LogException(message);
                    break;
                default:
                    sender.Log(message);
                    break;
            }
        }

        public static void LogGeneral<T>(this T sender, LogType type, string message, params object[] args) where T : class
        {
            sender.LogGeneral(type, string.Format(message, args));
        }

        #endregion LOG GENERAL
    }

    public enum Colors
    {
        aqua,
        black,
        blue,
        brown,
        cyan,
        darkblue,
        fuchsia,
        green,
        grey,
        lightblue,
        lime,
        magenta,
        maroon,
        navy,
        olive,
        purple,
        red,
        silver,
        teal,
        white,
        yellow
    }
}
