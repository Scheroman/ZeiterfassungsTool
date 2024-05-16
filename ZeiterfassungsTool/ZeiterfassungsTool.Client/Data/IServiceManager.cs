namespace ZeiterfassungsTool.Client.Data
{
    public interface IServiceManager
    {
        Task<ZeiterfassungsTaskListe> AddZeiterfassungsTask(ZeiterfassungsTask task);
        Task<ZeiterfassungsTaskListe> LoadZeiterfassungsTaskListe();
        Task<ZeiterfassungsTaskListe> DeleteZeiterfassungsTask(ZeiterfassungsTask task);
        Task<ZeiterfassungsTaskListe> UpdateZeiterfassungsTask(ZeiterfassungsTask task);
        Task<ZeiterfassungsTaskListe> AddTimelineTask(ZeiterfassungsTask newTask, ZeiterfassungsTask latestTask);
        Task<ZeiterfassungsTaskListe> LoadTimelineTasks();
    }
}
