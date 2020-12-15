using System;

// После установки пакета NuGet нужно подключить эту библиотеку.
using WindowsInput;

using System.Threading;

namespace ConstantKeyEmu
{
    class Program
    {
        static void Main()
        {
            // Чтобы использовать InputSimulator, нужно скачать одноимённый NuGet пакет.
            InputSimulator inputSimulator = new InputSimulator();
            do
            {
                Console.Write("Введите строчку, которой хотите спамить: ");
                string text = Console.ReadLine();
                Console.Write("Введите режим спама (1. По интервалу отпрвки сообщений, 2. По кол-ву сообщений): ");
                int mode = Convert.ToInt32(Console.ReadLine());
                
                // Первый режим спама по КД.
                if (mode == 1)
                {
                    Console.Write("Введите интервал отправки сообщений (не рекомендуется указывать меньше 50): ");
                    int interval = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Когда будете готовы, нажмите на поле ввода и нажмите END. Чтобы остановить спам, нажмите END.");
                    Console.WriteLine("Чтобы завершить программу, нажмите DELETE.");
                    Console.WriteLine("Чтобы задать новые значения, нажмите INSERT.");
                    while (true)
                    {
                        // Печатаем сообщения по КД, пока пользователь не выключит это.
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
                // Второй режим спама по количеству сообщений.
                else if (mode == 2)
                {
                    Console.Write("Кол-во сообщений для отправки: ");
                    int amount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Когда будете готовы, нажмите на поле ввода и нажмите END.");
                    Console.WriteLine("Чтобы завершить программу, нажмите DELETE.");
                    Console.WriteLine("Чтобы задать новые значения, нажмите INSERT.");
                    while (true)
                    {
                        // Отправляем сообщения без КД, считая только количество.
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
                    // В случае, если данный режим отсутвует, то запускаем программу сначала.
                    Console.WriteLine("Данный режим отсутствует. Нажмите любую кнопку, чтобы продолжить.");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (true) ;
        } 

        // Тот самый спам по КД.
        static void IntervalSpam(string text, int interval, InputSimulator inputSimulator)
        {
            // Если пользователь нажал END, то мы начинаем строчить сообщения по КД.
            if (inputSimulator.InputDeviceState.IsTogglingKeyInEffect(WindowsInput.Native.VirtualKeyCode.END))
            {
                inputSimulator.Keyboard.TextEntry($"{text} ");
                inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                Thread.Sleep(interval);
            }
        }

        // Спам по количеству.
        static void AmountSpam(string text, int amount, InputSimulator inputSimulator)
        {
            // Если пользователь нажал END, то мы за раз отправляем все сообщения.
            if (inputSimulator.InputDeviceState.IsKeyDown(WindowsInput.Native.VirtualKeyCode.END))
            {
                for (int i = 0; i < amount; i++)
                {
                    inputSimulator.Keyboard.TextEntry($"{text} ");
                    inputSimulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
                    Thread.Sleep(70);
                }
            }
        }
    }
}
