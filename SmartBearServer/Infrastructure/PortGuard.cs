using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SmartBearServer.Infrastructure
{
    public static class PortGuard
    {
        public static void CleanupPorts(params int[] ports)
        {
            foreach (var port in ports)
            {
                try
                {
                    KillProcessOnPort(port);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PortGuard] Failed to cleanup port {port}: {ex.Message}");
                }
            }
        }

        private static void KillProcessOnPort(int port)
        {
            var pids = GetPidsOnPort(port);
            var currentPid = Environment.ProcessId;

            foreach (var pid in pids)
            {
                if (pid == currentPid) continue;

                try
                {
                    var process = Process.GetProcessById(pid);
                    Console.WriteLine($"[PortGuard] Killing process {process.ProcessName} (PID: {pid}) using port {port}...");
                    process.Kill(true);
                    process.WaitForExit(2000);
                    Console.WriteLine($"[PortGuard] Port {port} cleared.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PortGuard] Could not kill PID {pid}: {ex.Message}");
                }
            }
        }

        private static List<int> GetPidsOnPort(int port)
        {
            var pids = new List<int>();
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "netstat.exe",
                        Arguments = $"-ano",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // Regex to find PID at the end of the line for a specific port
                // Matches lines like: TCP    0.0.0.0:7017           0.0.0.0:0              LISTENING       1234
                var pattern = $@"TCP\s+[\d\.]+:({port})\s+[\d\.]+:[\d]+\s+LISTENING\s+(\d+)";
                var matches = Regex.Matches(output, pattern);

                foreach (Match match in matches)
                {
                    if (match.Groups.Count >= 3 && int.TryParse(match.Groups[2].Value, out int pid))
                    {
                        if (!pids.Contains(pid)) pids.Add(pid);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[PortGuard] Error finding PIDs: {ex.Message}");
            }
            return pids;
        }
    }
}
