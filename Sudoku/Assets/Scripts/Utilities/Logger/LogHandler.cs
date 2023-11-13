using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Logger
{
    public static class LogHandler
    {
        private static List<LogType> _logTypes = new List<LogType>
        {
            LogType.Default,
            LogType.SudokuSolver
        };
        
        public static void LogInfo(string message, LogType logType = LogType.Default)
        {
            if(!_logTypes.Contains(logType)) return;

            Debug.Log(message);
        }

        public static void LogWarning(string message, LogType logType = LogType.Default)
        {
            if(!_logTypes.Contains(logType)) return;

            Debug.LogWarning(message);
        }

        public static void LogError(string message, LogType logType = LogType.Default)
        {
            if(!_logTypes.Contains(logType)) return;

            Debug.LogError(message);
        }
    }
}