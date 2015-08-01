using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class ExperimentStats {

  readonly Dictionary<string, string> stats;

  // ---------------------------------------------------------------------------
  public ExperimentStats() {
    stats = new Dictionary<string, string>();
  }

  // ---------------------------------------------------------------------------
  public string Get(string key, string defaultValue = "") {
    return (stats.ContainsKey(key)) ? stats[key]: defaultValue;
  }

  // ---------------------------------------------------------------------------
  public IEnumerator<KeyValuePair<string, string>> GetEnumerator() {
    return stats.GetEnumerator();
  }

  // ---------------------------------------------------------------------------
  public void Set(string key, string value) {
    stats[key] = value;
  }

  // ---------------------------------------------------------------------------
  public void Clear() {
    stats.Clear();
  }

  // ---------------------------------------------------------------------------
  public void WriteToFile(string filename) {
    string csv = string.Join(
      System.Environment.NewLine,
      stats.Select(d => d.Key + "," + d.Value).ToArray()
    );

    StringBuilder sb = new StringBuilder("Output", 128);
    if (!Directory.Exists(sb.ToString())) {
      Directory.CreateDirectory(sb.ToString());
    }
    sb.Append("/");
    sb.Append(filename);
    File.WriteAllText(sb.ToString(), csv);
  }
}