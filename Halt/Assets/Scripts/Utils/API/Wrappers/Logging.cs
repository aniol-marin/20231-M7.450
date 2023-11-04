using UnityEngine;

namespace Mole.Halt.Utils
{
    public class Info
    {
        public Info(string message) => MoleLog.Log(message);
    }

    public class Warning
    {
        public Warning(string message) => MoleLog.Warning(message);
    }

    public class Error
    {
        public Error(string message) => MoleLog.Error(message);
    }

    public static class LogSettings
    {
        public static void FilterOut(string pattern)
        {
            MoleLog.SetExclusionPattern(pattern);
        }
    }

    internal static class MoleLog
    {
        private static LogFilter filter;

        public static void Log(string message)
        {
            if (ValidMessage(message)) Debug.Log(message);
        }
        public static void Warning(string message)
        {
            if (ValidMessage(message)) Debug.LogWarning(message);
        }

        public static void Error(string message)
        {
            if (ValidMessage(message)) Debug.LogError(message);
        }

        public static void SetExclusionPattern(string pattern)
        {
            filter.excluded = pattern;
        }

        private static bool ValidMessage(string message)
        {
            return !excluded();

            bool excluded() => !string.IsNullOrEmpty(filter.excluded) && !message.Contains(filter.excluded);
        }
    }

    internal struct LogFilter
    {
        public string excluded;
    }

    internal enum LogType
    {
        info,
        warning,
        error,
    }
}