using System;
using WindowsInput;
using System.Threading;

namespace ConstantKeyEmu
{
    class Program
    {
        static void Main()
        {
            InputSimulator inputSimulator = new InputSimulator();
            do
            {
                Console.Write("Введите строчку, которой хотите спамить: ");
                string text = Console.ReadLine();
                Console.Write("Введите режим спама (1. По интервалу отпрвки сообщений, 2. По кол-ву сообщений): ");
                int mode = Convert.ToInt32(Console.ReadLine());
                if (mode == 1)
                {
                    Console.Write("Введите интервал отправки сообщений (не рекомендуется указывать меньше 50): ");
                    int interval = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Когда будете готовы, нажмите на поле ввода и нажмите END. Чтобы остановить спам, нажмите END.");
                    Console.WriteLine("Чтобы завершить программу, нажмите DELETE.");
                    Console.WriteLine("Чтобы задать новые значения, нажмите INSERT.");
                    while (true)
                    {
                        IntervalSpam(text, interval, inputSimulator);
                        if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.INSERT))
                        {
                            Console.Clear();
                            break;
                        }
                        else if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.DELETE))
                        {
                            Environment.Exit(0);
                        }
                    }
                }
                else if (mode == 2)
                {
                    Console.Write("Кол-во сообщений для отправки: ");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Когда будете готовы, нажмите на поле ввода и нажмите END.");
                    Console.WriteLine("Чтобы завершить программу, нажмите DELETE.");
                    Console.WriteLine("Чтобы задать новые значения, нажмите INSERT.");
                    while (true)
                    {
                        AmountSpam(text, amount, inputSimulator);
                        if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.INSERT))
                        {
                            Console.Clear();
                            break;
                        }
                        else if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.DELETE))
                        {
                            Environment.Exit(0);
                        }
                    }
                } else
                {
                    Console.WriteLine("Данный режим отсутствует. Нажмите любую кнопку, чтобы продолжить.");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true) ;
        } 

        static void IntervalSpam(string text, int interval, InputSimulator inputSimulator)
        {
            if (inputSimulator.InputDeviceState.IsTogglingKeyInEffect(WindowsInput.Native.VirtualKeyCode.END))
            {
                inputSimulator.Keyboard.TextEntry($"{text} ");
                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                Thread.Sleep(interval);
            }
        }

        static void AmountSpam(string text, int amount, InputSimulator inputSimulator)
        {
            if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.END))
            {
                for (int i = 0; i < amount; i++)
                {
                    inputSimulator.Keyboard.TextEntry($"{text} ");
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                    Thread.Sleep(50);
                }
            }
        }
    }
}
