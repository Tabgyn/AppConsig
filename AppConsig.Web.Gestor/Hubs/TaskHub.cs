using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AppConsig.Web.Gestor.Models;
using Microsoft.AspNet.SignalR;

namespace AppConsig.Web.Gestor.Hubs
{
    public class TaskHub : Hub
    {
        private static ConcurrentDictionary<string, TaskModel> _currentTasks;

        private static ConcurrentDictionary<string, TaskModel> CurrentTasks
            => _currentTasks ?? (_currentTasks = new ConcurrentDictionary<string, TaskModel>());

        private void ReportProgress()
        {
            Clients.All.progressChanged(CurrentTasks.Select(t => t.Value));
            foreach (var task in CurrentTasks.Where(task => task.Value.Percent >= 100))
            {
                TaskModel taskModel;
                CurrentTasks.TryRemove(task.Key, out taskModel);
                ReportProgress();
            }
        }

        public void CancelTask(string taskId)
        {
            if (CurrentTasks.ContainsKey(taskId))
                CurrentTasks[taskId].CancelToken.Cancel();
        }

        public async Task<string> ProcessarFolha(string arquivo)
        {
            var tokenSource = new CancellationTokenSource();

            string taskId = $"task{Guid.NewGuid()}";

            CurrentTasks.TryAdd(taskId, new TaskModel
            {
                Percent = 0,
                Id = taskId,
                Name = $"folha{CurrentTasks.Count}",
                CancelToken = tokenSource
            });

            var task = ProcessarFolha(tokenSource.Token, new Progress<int>(pourcent =>
            {
                if (CurrentTasks.ContainsKey(taskId))
                    CurrentTasks[taskId].Percent = pourcent;

                ReportProgress();
            }));

            await task;

            return "";
        }

        public static async Task ProcessarFolha(CancellationToken token, IProgress<int> progress)
        {
            for (var i = 0; i <= 100; i++)
            {
                if (token.IsCancellationRequested)
                {
                    progress?.Report(100);
                    token.ThrowIfCancellationRequested();
                }

                progress?.Report(i);

                await Task.Delay(1000, token);
            }

            progress?.Report(100);
        }
    }
}