using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class BankAccount
    {
        public object locked = new object();
        public int Balance { get; set; }
        public double Growth { get; set; }
        

        public async Task Deposit(int Money)
        {
            await Task.Run(() =>
            {
                Balance += Money;
            });                       
        }

        //Con Locked y esperando que en donde se consuma se use Task.Add(Task.Factory.StartNew(() => { EL BLOQUE DE CÓDIGO QUE SE DESEA EJECUTAR })); para realizar una tarea de manera directa sin implementar algún metodo separado.
        public void DepositLocked(int Money)
        {
            lock (locked)
            {
                Balance += Money;
            }
        }

        public async Task Draw(int Money)
        {
            await Task.Run(() =>
            {
                Balance -= Money;
            });
        }
        
        //Con Locked y esperando que en donde se consuma se use Task.Add(Task.Factory.StartNew(() => { EL BLOQUE DE CÓDIGO QUE SE DESEA EJECUTAR })); para realizar una tarea de manera directa sin implementar algún metodo separado.
        public void DrawLocked(int Money)
        {
            lock (locked)
            {
                Balance -= Money;
            }
        }

        public async Task CalculateGrowth(double Percentage)
        {
            await Task.Run(() =>
            {
                Growth = (Balance * Percentage);
            });            
        }

        public async Task<double> CalculateUsingTGrowth(double Percentage)
        {
            return await Task.Run(() =>
            {
                return (Balance * Percentage);
            });
        }

        //Usando TaskCompletionSource para completar una tarea asíncrona manualmente
        public Task<double> CalculateManuallyGrowth(double Percentage)
        {
            var taskCompletion = new TaskCompletionSource<double>();

            Task.Run(() => 
            { 
                double Growth = (Balance * Percentage);
                taskCompletion.SetResult(Growth);
            });
            return taskCompletion.Task;
        }

        //Usando TaskFactory como otra forma de crear tareas asíncronas y usando Task<T>
        public async Task<double> CalculateFactoryGrowth(double Percentage)
        {
            return await Task.Factory.StartNew(() =>
            {
                return Balance * Percentage;
            });
        }
    }
}
