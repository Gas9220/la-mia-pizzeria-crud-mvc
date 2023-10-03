﻿namespace la_mia_pizzeria_crud_mvc.CustomLoggers
{
    public class CustomFileLogger
    {
        public void WriteLog(string message, string operation)
        {
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "//log.txt", $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} LOG: {message} OPERATION: {operation}\n");
        }
    }
}
