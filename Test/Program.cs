using Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var bank = new BankAccount();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    //await bank.Deposit(100);

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        bank.DepositLocked(100);
                    }));
                    
                }
                for(int k = 0; k < 1000; k++)
                {
                    //await bank.Draw(50);
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        bank.DrawLocked(50);
                    }));
                }
            }

            //Normal para el rendimiento
            await bank.CalculateGrowth(.10);

            //Aplicando Task<T> para el rendimiento
            double growthT = await bank.CalculateUsingTGrowth(.10);

            //Aplicando TaskCompletionSource para el rendimiento
            Task<double> growthTask = bank.CalculateManuallyGrowth(.10);
            double growthManually = await growthTask;

            //Aplicando TaskFactory para el rendimiento
            double growthFactory = await bank.CalculateFactoryGrowth(.10);                       

            Console.WriteLine($"Final balance is {bank.Balance}");

            Console.WriteLine($"Final growth is {bank.Growth}");
            Console.WriteLine($"Final growthT is {growthT}");
            Console.WriteLine($"Final growthManually is {growthManually}");
            Console.WriteLine($"Final growthFactory is {growthFactory}");



            Console.ReadLine();
        }

        //Investigar cómo usar las tasks, 4 formas aprox
        //Repasar lo de locked
    }
}
