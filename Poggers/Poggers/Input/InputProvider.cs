using System;
using System.Collections.Generic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Poggers.Input
{
    public class InputProvider
    {
        private static InputProvider instance;

        private readonly List<Action<GameWindow, KeyboardKeyEventArgs>> listenersDown = new List<Action<GameWindow, KeyboardKeyEventArgs>>();
        private readonly List<Action<GameWindow, KeyboardKeyEventArgs>> listenersUp = new List<Action<GameWindow, KeyboardKeyEventArgs>>();

        public InputProvider(GameWindow window)
        {
            window.KeyUp += args => this.UpProxy(window, args);
            window.KeyDown += args => this.DownProxy(window, args);
        }

        public static InputProvider Instance { get => instance; set => instance = value; }

        public void SubscribeDown(Action<GameWindow, KeyboardKeyEventArgs> func)
        {
            this.listenersDown.Add(func);
        }

        public void SubscribeUp(Action<GameWindow, KeyboardKeyEventArgs> func)
        {
            this.listenersUp.Add(func);
        }

        public void UnsubscribeDown(Action<GameWindow, KeyboardKeyEventArgs> func)
        {
            if (this.listenersDown.Contains(func))
            {
                this.listenersDown.Remove(func);
            }
        }

        public void UnsubscribeUp(Action<GameWindow, KeyboardKeyEventArgs> func)
        {
            if (this.listenersUp.Contains(func))
            {
                this.listenersUp.Remove(func);
            }
        }

        private void DownProxy(GameWindow window, KeyboardKeyEventArgs args)
        {
            Array.ForEach(this.listenersDown.ToArray(), listener => listener(window, args));
        }

        private void UpProxy(GameWindow window, KeyboardKeyEventArgs args)
        {
            Array.ForEach(this.listenersUp.ToArray(), listener => listener(window, args));
        }
    }
}
