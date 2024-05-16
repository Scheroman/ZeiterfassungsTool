using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZeiterfassungsTool.Client.Data
{
    public class ZeiterfassungsTask
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime End { get; set; }

        public ZeiterfassungsTask()
        {
            ID = Guid.NewGuid().ToString();
        }

        public ZeiterfassungsTask(ZeiterfassungsTask task)
        {
            this.ID = task.ID;
            this.Name = task.Name;
            this.Beginn = task.Beginn;
            this.End = task.End;
            this.Link = task.Link;
        }

        public ZeiterfassungsTask Copy()
        {
            ZeiterfassungsTask copy = new ZeiterfassungsTask();
            copy.Name = this.Name;
            copy.Beginn = this.Beginn;
            copy.End = this.End;
            copy.Link = this.Link;
            return copy;
        }

        public void ParseName()
        {
            // http://youtrack.dev.cbt/issue/AB-450/Some-Ticket_Title
            var match = Regex.Match(this.Name, @"issue/(.+)/(.+)");

            if (match.Success)
            {
                string issueId = match.Groups[1].Value;
                string title = match.Groups[2].Value;
                title = title.Replace("-", " ");
                title = title.Replace("_", " ");
                this.Link = this.Name;
                this.Name = issueId + " - " + title;
                
            }
        }
    }
}
