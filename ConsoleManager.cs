﻿using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BlueShadowMon
{
    [SupportedOSPlatform("windows")]
    internal static class ConsoleManager
    {
        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static string GameTitle { get; set; } = "Blue Shadow Mon";
        public static ConsoleColor DefaultFgColor { get; set; } = ConsoleColor.White;
        public static ConsoleColor DefaultBgColor { get; set; } = ConsoleColor.Black;
        public static int FrameRate { get; set; } = 30;
        private static DateTime LastFrame = DateTime.Now;

        /// <summary>
        /// Disable resizing, minimize and maximize buttons.
        /// Change console properties.
        /// </summary>
        public static void WindowSetup()
        {
            // Disable resizing and minimize/maximize buttons
            IntPtr window = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(window, false);
            int MF_BYCOMMAND = 0x00000000;
            int SC_MINIMIZE = 0xF020;
            int SC_MAXIMIZE = 0xF030;
            int SC_SIZE = 0xF000;
            DeleteMenu(sysMenu, SC_MINIMIZE, MF_BYCOMMAND);
            DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
            DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);

            // Set console properties
            Console.Title = GameTitle;
            Console.TreatControlCAsInput = true;
            Console.CursorVisible = false;

            // Set console size to 80% of the largest possible size
            int width = (int)(Console.LargestWindowWidth * 0.8 / Tile.Width) * Tile.Width;
            int height = (int)(Console.LargestWindowHeight * 0.8 / Tile.Height) * Tile.Height;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
        }

        /// <summary>
        /// Writes text to the console at the specified position.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="x">Number of column from the left</param>
        /// <param name="y">Number of lines from the top</param>
        /// <param name="centered">Writes the text centered around the position</param>
        public static void WriteText(string text, int x, int y, bool centered = false)
        {
            WriteText(text, x, y, DefaultFgColor, DefaultBgColor, centered);
        }

        /// <summary>
        /// Writes text to the console at the specified position.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="x">Number of column from the left</param>
        /// <param name="y">Number of lines from the top</param>
        /// <param name="fcolor">Color of the text</param>
        /// <param name="centered">Writes the text centered around the position</param>
        public static void WriteText(string text, int x, int y, ConsoleColor fcolor, bool centered = false)
        {
            WriteText(text, x, y, fcolor, DefaultBgColor, centered);
        }

        /// <summary>
        /// Writes text to the console at the specified position.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="x">Number of column from the left</param>
        /// <param name="y">Number of lines from the top</param>
        /// <param name="fcolor">Color of the text</param>
        /// <param name="bcolor">Color of the background</param>
        /// <param name="centered">Writes the text centered around the position</param>
        public static void WriteText(string text, int x, int y, ConsoleColor fcolor, ConsoleColor bcolor, bool centered = false)
        { 
            Console.ForegroundColor = fcolor;
            Console.BackgroundColor = bcolor;
            if (centered)
            {
                x = x - (text.Length / 2);
                y = y - (text.Split(Environment.NewLine).Length / 2);
            }
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
        
        /// <summary>
        /// Wait for next frame
        /// </summary>
        public static void Update()
        {
            while (DateTime.Now - LastFrame < TimeSpan.FromSeconds(1.0 / FrameRate))
            {
                // Wait until the next frame
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Catch all inputs waiting in the buffer.
        /// </summary>
        /// <returns>List of inputs caught</returns>
        public static List<ConsoleKeyInfo> catchInputs()
        {
            List<ConsoleKeyInfo> inputs = new List<ConsoleKeyInfo>();
            while (Console.KeyAvailable)
            {
                inputs.Add(Console.ReadKey(true));
            }
            return inputs;
        }

        /// <summary>
        /// Print a map in the console, with the given tile centered in the window.
        /// </summary>
        /// <param name="map">The map to print</param>
        /// <param name="centeredX">Tile x coordinates</param>
        /// <param name="centeredY">Number of lines from the top</param>
        public static void PrintMap(Map map, int centeredX, int centeredY)
        {
            // TODO: Print map
        }
    }
}