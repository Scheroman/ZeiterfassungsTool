using Blazored.LocalStorage;
using System.Runtime.CompilerServices;
using ZeiterfassungsTool.Client.Pages;

namespace ZeiterfassungsTool.Client.Data
{
    public class SaveManager : IServiceManager
    {
        private const string TASKS_KEY = "tasks";

        private readonly ILocalStorageService localStorage;
        private ZeiterfassungsTaskListe zeiterfassungsTaskListe;
        private ZeiterfassungsTaskListe timelineTasks;

        public SaveManager(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<ZeiterfassungsTaskListe> AddZeiterfassungsTask(ZeiterfassungsTask task)
        {
            return await addZeiterfassungsTask(task, DateTime.MinValue, zeiterfassungsTaskListe);
        }

        public async Task<ZeiterfassungsTaskListe> LoadZeiterfassungsTaskListe()
        {
            return await loadZeiterfassungsTaskListe(TASKS_KEY, DateTime.MinValue);
        }

        public async Task<ZeiterfassungsTaskListe> DeleteZeiterfassungsTask(ZeiterfassungsTask task)
        {
            if (zeiterfassungsTaskListe == null)
            {
                zeiterfassungsTaskListe = await loadZeiterfassungsTaskListe(TASKS_KEY, DateTime.MinValue);
            }

            if (task != null && zeiterfassungsTaskListe.Items.Contains(task))
            {
                zeiterfassungsTaskListe.Items.Remove(task);
                await saveTasks(TASKS_KEY, DateTime.MinValue, zeiterfassungsTaskListe);
            }

            return zeiterfassungsTaskListe;
        }

        public async Task<ZeiterfassungsTaskListe> UpdateZeiterfassungsTask(ZeiterfassungsTask task)
        {
            if(zeiterfassungsTaskListe == null)
            {
                zeiterfassungsTaskListe = await loadZeiterfassungsTaskListe(TASKS_KEY, DateTime.MinValue);
            }

            zeiterfassungsTaskListe.Update(task);

            await saveTasks(TASKS_KEY, DateTime.MinValue, zeiterfassungsTaskListe);
            return zeiterfassungsTaskListe;
        }

        public async Task<ZeiterfassungsTaskListe> AddTimelineTask(ZeiterfassungsTask newTask, ZeiterfassungsTask latestTask)
        {
            if (timelineTasks == null)
            {
                timelineTasks = new ZeiterfassungsTaskListe();
            }

            timelineTasks.Update(latestTask);
            timelineTasks =  await addZeiterfassungsTask(newTask, DateTime.Now, timelineTasks);
            return timelineTasks;
        }

        public async Task<ZeiterfassungsTaskListe> LoadTimelineTasks()
        {
            return await loadZeiterfassungsTaskListe(TASKS_KEY, DateTime.Now);
        }

        private async Task<ZeiterfassungsTaskListe> addZeiterfassungsTask(ZeiterfassungsTask task, DateTime dateTime, ZeiterfassungsTaskListe liste)
        {
            if (task == null)
            {
                return liste;
            }

            if (liste == null)
            {
                liste = await loadZeiterfassungsTaskListe(TASKS_KEY, dateTime);
            }

            liste.Items.Add(task);
            await saveTasks(TASKS_KEY, dateTime, liste );
            return liste;
        }

        private async Task<ZeiterfassungsTaskListe> loadZeiterfassungsTaskListe (string key, DateTime dateTime)
        {
            string fullKey = key;

            if ( dateTime != DateTime.MinValue)
            {
                fullKey += dateTime.ToShortDateString();
            }

            ZeiterfassungsTaskListe liste = await localStorage.GetItemAsync<ZeiterfassungsTaskListe>(fullKey);
            
            if (liste == null)
            {
                liste = new ZeiterfassungsTaskListe();
            }

            return liste;
        }

        private async Task saveTasks(string key, DateTime date, ZeiterfassungsTaskListe liste)
        {
            string fullKey = key;

            if (date != DateTime.MinValue)
            {
                fullKey += date.ToShortDateString();
            }

            await localStorage.SetItemAsync(fullKey, liste);
        }
    }
}
