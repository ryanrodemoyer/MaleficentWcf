using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Maleficent.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void Test01()
        {
            List<BackgroundWorker> workers = new List<BackgroundWorker>();

            HttpClient client = new HttpClient();

            for (int i = 0; i < 100; i++)
            {
                var worker = new BackgroundWorker();

                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                workers.Add(worker);
            }

            foreach (var worker in workers)
            {
                worker.RunWorkerAsync(client);
            }



            //Task.WhenAll(_tasks);

            //client.Dispose();

            Debug.WriteLine("Finished!");
        }

        [Test]
        public async void Test03()
        {
            List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();

            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 100; i++)
                {
                    tasks.Add(client.GetAsync("http://localhost/MaleficentWeb/api/default"));
                }

                HttpResponseMessage[] responses = await Task.WhenAll(tasks);

                Debug.WriteLine($"Finished! {responses.Length}");
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Task<HttpResponseMessage> task = (Task<HttpResponseMessage>) e.Result;

            Task<string> task = (Task<string>) e.Result;

            string result = task.Result;

            Debug.WriteLine(result);

            ////var r = await task;
            ////var r = task.Result;
            //_tasks.Add(task);
        }

        private ConcurrentBag<Task<HttpResponseMessage>> _tasks = new ConcurrentBag<Task<HttpResponseMessage>>();

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
                HttpClient client = (HttpClient) e.Argument;

                e.Result = client.GetStringAsync("http://localhost/MaleficentWeb/api/default");
        }

        [Test]
        public void Test02()
        {
            Debug.WriteLine("Unit test");
        }
    }
}
