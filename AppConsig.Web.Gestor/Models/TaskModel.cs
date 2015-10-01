using System.Threading;
using Newtonsoft.Json;

namespace AppConsig.Web.Gestor.Models
{
    public class TaskModel
    {
        [JsonProperty("taskId")]
        public string Id { get; set; }
        [JsonProperty("taskName")]
        public string Name { get; set; }
        [JsonProperty("taskPercent")]
        public int Percent { get; set; }
        [JsonIgnore]
        public CancellationTokenSource CancelToken { get; set; }
    }
}