var certificatesOrder = new Dictionary<int, int[]>
{
    { 1, new int[] { 2 } },
    { 2, new int[] { 3, 4 } }
};
var res = GetCertificateOrder(certificatesOrder);

List<int> GetCertificateOrder(Dictionary<int, int[]> data)
{
    var graph = new Dictionary<int, List<int>>();
    var inDegree = new Dictionary<int, int>();

    foreach (var official in data)
    {
        if (!graph.ContainsKey(official.Key))
        {
            graph[official.Key] = [];
            inDegree[official.Key] = 0;
        }

        foreach (var req in official.Value)
        {
            if (!graph.ContainsKey(req))
            {
                graph[req] = [];
                inDegree[req] = 0;
            }

            graph[req].Add(official.Key);
            inDegree[official.Key]++;
        }
    }

    var queue = new Queue<int>();
    foreach (var kvp in inDegree.Where(kvp => kvp.Value == 0).ToList())
        queue.Enqueue(kvp.Key);

    var result = new List<int>();
    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
        result.Add(current);

        foreach (var official in graph[current])
        {
            inDegree[official]--;
            if (inDegree[official] == 0)
                queue.Enqueue(official);
        }
    }

    return result;
}
Console.WriteLine(CanSum(5, new[] { 1,2,3 }));

bool CanSum(int target, int[] nums)
{
    var dp = new bool[target + 1];
    dp[0] = true;

    foreach (var num in nums)
        for (var i = target; i >= num; i--)
        {
            if (dp[i - num])
            {
                dp[i] = true;
                if (i == target)
                    return true;
            }
        }

    return dp[target];
}