namespace ZeiterfassungsTool.Client.Data
{
    public class ZeiterfassungsTaskListe
    {
        public List<ZeiterfassungsTask> Items { get; set; }

        public ZeiterfassungsTaskListe()
        {
            Items = new List<ZeiterfassungsTask>();
        }

        public bool Contains(ZeiterfassungsTask task)
        {
            return Items.Select(t => t.ID.Equals(task.ID)).Any();
        }

        public bool Remove(ZeiterfassungsTask task)
        {
            if (!Items.Contains(task))
            {
                return false;
            }

            ZeiterfassungsTask existingTask = Items.Where(t => t.ID.Equals(task.ID)).First();
            if (existingTask != null)
            {
                Items.Remove(existingTask);
            }

            return true;
        }

        public void Update(ZeiterfassungsTask task)
        {
            if (Remove(task))
            {
                Items.Add(task);
            }
        }

        public ZeiterfassungsTask Last()
        {
            if (Items == null || Items.Count == 0)
            {
                return null;
            }

            return Items.Last();
        }
    }
}
